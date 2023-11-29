using NAUL.Manager;
using NAUL.Services;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace NAUL.Models;

public class VersionItem
{
    private string _DisplayName;
    public string DisplayName { get => _DisplayName; set => FormatAndSetName(value); }

    public string DisplayGlyph { get; set; }

    public Version GameVersion { get => GetGameVersionByAssemblyFile(); }

    public GamePlatforms GamePlatform { get => GetGamePlatformByPath(); }

    public bool HasBepInExInstalled { get => CheckIntegrityOfBepInEx(); }

    public string BepInExVersion { get => GetBepInExVersion(); }

    private string _Path;
    public string Path { get => _Path; set { _Path = value.Replace("\\", "/").TrimEnd('/'); } }

    public string ExecutablePath { get => Path + "/Among Us.exe"; }

    public PluginItem EnabledSinglePlugin { get; set; } 

    public string Description => $"{(GameVersion == null ? "未知版本" : GameVersion)}, {GamePlatform}, {(HasBepInExInstalled ? "BepInEx: " + BepInExVersion : string.Empty)}".Trim().TrimEnd(',');
    public bool IsValid => FindGameService.IsValidAmongUsFolder(Path);
    public bool IsBepInExEnabled => File.IsEnabled(Path + "/winhttp.dll");
    public string PluginFolderPath => Path + "/BepInEx/plugins";

    public string GetDescriptionText()
        => (EnabledSinglePlugin?.IsValid ?? false)
        ? $"{EnabledSinglePlugin.PluginVersion} | {GamePlatform}"
        : $"{(GameVersion == null ? "未知版本" : GameVersion)} | {GamePlatform}";

    private void FormatAndSetName(string name)
    {
        string formatedName;
        int index = 0;
        do
        {
            // eg. TownOfHost (3)
            formatedName = index == 0 ? name : $"{name} ({index})";
            index++;
        } while (VersionManager.AllVersions.Any(v => v.DisplayName == formatedName));
        _DisplayName = formatedName;
    }
    private Version GetGameVersionByAssemblyFile()
    {
        string md5 = Utils.GetMD5HashFromFile(Path + "/GameAssembly.dll");
        return VersionManager.AssemblyMD5Infos.Find(info => info.MD5 == md5)?.Version;
    }
    private bool CheckIntegrityOfBepInEx() // Incomplete
    {
        return !(
            !Directory.Exists(Path)
            || !Directory.Exists(Path + "/BepInEx/core")
            || !Directory.Exists(Path + "/dotnet")
            || (!File.Exists(Path + "/winhttp.dll"))
            );
    }
    private string GetBepInExVersion()
    {
        string fileName = Path + "/BepInEx/core/BepInEx.Core.dll";
        if (!HasBepInExInstalled || !File.Exists(fileName)) return null;
        string version = FileVersionInfo.GetVersionInfo(fileName).ProductVersion;
        version = Regex.Replace(version, @"\+.+", string.Empty);
        return version;
    }
    private GamePlatforms GetGamePlatformByPath()
    {
        if (Path.EndsWith("/AmongUs") && Directory.Exists(Path + "/.egstore")) return GamePlatforms.Epic;
        else if (Path.EndsWith("/Among Us") && System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(Path)).EndsWith("steamapps")) return GamePlatforms.Steam;
        else return GamePlatforms.Local;
    }

    public void SetBepInExStatus(bool enable)
    {
        File.SetStatus(Path + "/winhttp.dll", enable);
    }

    private string GetPluginFullPath(PluginItem plugin)
    {
        string path = PluginFolderPath + "/" + plugin.FileName;
        return File.Exists(path)
            ? File.GetTruePath(path)
            : Directory.Exists(PluginFolderPath)
            ? GetPluginFullPathByMD5(plugin.MD5)
            : null;
    }
    private string GetPluginFullPathByMD5(string md5)
    {
        return Directory.EnumerateFiles(PluginFolderPath).ToList().Find(p => Utils.GetMD5HashFromFile(p) == md5);
    }

    public bool IsPluginEnabled(PluginItem plugin)
    {
        var pluginPath = GetPluginFullPath(plugin);
        if (string.IsNullOrEmpty(pluginPath) || !File.Exists(pluginPath) || !plugin.Is(pluginPath)) return false;
        return File.IsEnabled(pluginPath);
    }

    public void SetPluginStatus(PluginItem plugin, bool enable)
    {
        var pluginPath = GetPluginFullPath(plugin);
        if (string.IsNullOrEmpty(pluginPath) || !File.Exists(pluginPath))
        {
            if (!Directory.Exists(PluginFolderPath))
                Directory.CreateDirectory(PluginFolderPath);
            pluginPath = PluginFolderPath + "/" + plugin.FileName;
            System.IO.File.Copy(plugin.Path, pluginPath, true);
        }
        File.SetStatus(pluginPath, enable);
    }

    public void OpenInExplorer()
    {
        ProcessStartInfo psi = new ProcessStartInfo("Explorer.exe")
        { Arguments = "/e,/select," + Path.Replace("/", "\\") };
        Process.Start(psi);
    }

    public bool Delete(out string reason)
    {
        reason = null;
        try { Directory.Delete(Path, true); }
        catch (Exception ex) { reason = ex.Message; }
        if (Directory.Exists(Path)) return false;
        else
        {
            VersionManager.AllVersions.Remove(this);
            VersionManager.SaveVersionsToConfig();
            if (VersionManager.AllVersions.Count > 0)
                VersionManager.SelectedVersion = VersionManager.AllVersions.First();
            else
                VersionManager.SelectedVersion = null;
            return true;
        }
    }
}

public enum GamePlatforms
{
    Local,
    Steam,
    Epic,
}