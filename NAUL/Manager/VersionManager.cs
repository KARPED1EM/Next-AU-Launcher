using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using NAUL.Services;

namespace NAUL.Manager;

internal class VersionManager
{
    public static List<AssemblyMD5Info> assemblyMD5Info = CloudService.RequestAssemblyMODInfo();
    public static List<VersionItem> versions = new();
    public static VersionItem currentVersion;

    public static ObservableCollection<VersionItem> GetCollectionOfVersions()
    {
        var collection = new ObservableCollection<VersionItem>();
        foreach (var path in versions)
            collection.Add(path);
        return collection;
    }

    public static void SearchAllVersion()
    {
        foreach (var gamePath in GamePathService.GamePaths)
        {
            Version gameVer = GetGameVersion(gamePath);
            string bepInExVer = GetBepInExVersion(gamePath);
            GamePlatform gamePlatform = GetGamePlatformByPath(gamePath);

            // Add vanilla first
            string vanillaName = gamePlatform switch
            {
                GamePlatform.Steam => "Steam",
                GamePlatform.Epic => "Epic",
                _ => "本地"
            } + " 原版";
            vanillaName = FormatNameToPreventDuplicate(vanillaName);
            versions.Add(new(vanillaName, "原版", new(), gameVer, bepInExVer, gamePlatform, gamePath));

            // Jump to next game path if BepInEx not installed
            if (!HasBepInExInstalled(gamePath)) continue;

            // Find mods
            string pluginPath = gamePath + "/BepInEx/plugins";

            var dllPaths = Directory.EnumerateFiles(pluginPath);

            foreach (var dllPath in dllPaths)
            {
                if (!dllPath.EndsWith(".disabled") && !dllPath.EndsWith(".dll")) continue;
                string dllName = Path.GetFileNameWithoutExtension(Path.GetFileNameWithoutExtension(dllPath));
                dllName = Regex.Replace(dllName, @"\-.*|v.+|\s", string.Empty);

                versions.Add(new(FormatNameToPreventDuplicate(dllName), dllName, GetModVersion(dllPath), gameVer, bepInExVer, gamePlatform, gamePath));
            }

        }
    }

    private static string FormatNameToPreventDuplicate(string name)
    {
        string formatedName;
        int index = 0;
        do
        {
            formatedName = index == 0 ? name : $"{name} ({index})";
            index++;
        } while (versions.Any(v => v.Name == formatedName));
        return formatedName;
    }

    public static Version GetGameVersion(string path)
    {
        string md5 = Utils.GetMD5HashFromFile(path + "/GameAssembly.dll");
        return assemblyMD5Info.Find(info => info.MD5 == md5)?.Version;
    }

    public static bool HasBepInExInstalled(string path)
    {
        return !(
            !Directory.Exists(path)
            || !Directory.Exists(path + "/BepInEx")
            || !Directory.Exists(path + "/dotnet")
            || !File.Exists(path + "/winhttp.dll")
            );
    }

    public static string GetBepInExVersion(string path)
    {
        string fileName = path + "/BepInEx/core/BepInEx.Core.dll";
        if (!File.Exists(fileName)) return "None";
        //AssemblyInformationalVersion
        //var assembly = Assembly.LoadFrom(fileName);
        string version = FileVersionInfo.GetVersionInfo(fileName).ProductVersion;
        version = Regex.Replace(version, @"\+.+", string.Empty);
        return version;
    }

    public static GamePlatform GetGamePlatformByPath(string path)
    {
        if (Directory.Exists(path + "/.egstore")) return GamePlatform.Epic;
        else if (Path.GetDirectoryName(Path.GetDirectoryName(path)).EndsWith("steamapps")) return GamePlatform.Steam;
        else return GamePlatform.Local;
    }

    public static Version GetModVersion(string path)
    {
        var version = FileVersionInfo.GetVersionInfo(path).FileVersion;
        return Version.Parse(version);
    }

}

public class VersionItem
{
    public string Name { get; set; }
    public string Mod { get; set; }
    public Version ModVersion { get; set; }
    public Version GameVersion { get; set; }
    public string BepInExVersion { get; set; }
    public GamePlatform Platform { get; set; }
    public string FolderLocation { get; set; }
    public string FontGlyph { get; set; }

    public string DisplayDescriptionForUI => IsVanilla
        ? $"{GameVersion} {Platform}"
        : $"{ModVersion} {Platform}";

    public VersionItem(string name, string mod, Version modVersion, Version gameVersion, string bepInExVersion, GamePlatform platform, string folderLocation, string fontGlyph = "\uE7FC")
    {
        bool broken = !Directory.Exists(folderLocation);

        Name = (broken ? "(无效) " : string.Empty) + name;
        Mod = mod;
        ModVersion = modVersion;
        GameVersion = gameVersion;
        BepInExVersion = bepInExVersion;
        Platform = platform;
        FolderLocation = folderLocation;
        FontGlyph = broken ? "\uE729" : fontGlyph;
    }

    public bool IsVanilla => ModVersion == new Version();
}

public class AssemblyMD5Info
{
    public string MD5 { get; set; }
    public GamePlatform Platform { get; set; }
    public Version Version { get; set; }

    public AssemblyMD5Info(string md5, GamePlatform platform, Version version)
    {
        MD5 = md5;
        Platform = platform;
        Version = version;
    }
}

public enum GamePlatform
{
    Local,
    Steam,
    Epic
}
