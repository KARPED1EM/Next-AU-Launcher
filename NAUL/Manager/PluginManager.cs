using NAUL.Models;
using NAUL.Services;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;

namespace NAUL.Manager;

internal class PluginManager
{
    public static List<PluginItem> Plugins = new();
    public static List<PluginInfoItem> PluginInfos = CloudService.RequestPluginInfos();

    public static void Init()
    {
        ReadPluginsFromConfig();
        DeleteInvalidPlugins();
        FindPluginsFromAllGamePaths(false);
        Plugins.ForEach(p => p.TryGetInfoFromCloud());
        SavePluginsToConfig();
    }

    public static void FindPluginsFromAllGamePaths(bool saveToConfig = true)
    {
        bool needSave = false;
        foreach(var version in VersionManager.Versions)
        {
            string folderPath = version.Path + "/BepInEx/plugins";
            if (!Directory.Exists(folderPath)) continue;
            foreach (var filePath in Directory.EnumerateFiles(folderPath).Where(path => path.EndsWith(".dll") && !Plugins.Any(p => p.MD5 == Utils.GetMD5HashFromFile(path))))
            {
                string pluginPath = filePath.Replace("\\", "/");
                var plugin = new PluginItem();
                plugin.MD5 = Utils.GetMD5HashFromFile(pluginPath);
                plugin.FileName = Path.GetFileName(pluginPath);
                plugin.DisplayName = plugin.PluginName = TryGetPluginName(pluginPath);
                plugin.PluginVersion = Version.Parse(TryGetPluginVersion(pluginPath));
                plugin.Author = TryGetPluginAuthor(pluginPath, plugin.PluginName);
                plugin.IsSingleMod = true;

                string destFileName = DataPaths.SAVE_PLUGIN_PATH + plugin.MD5;
                if (!File.Exists(destFileName))
                    System.IO.File.Copy(pluginPath, destFileName, false);

                Plugins.Add(plugin);
                needSave = true;
            }
        }
        if (needSave && saveToConfig) SavePluginsToConfig();
    }

    public static void DeleteInvalidPlugins(bool saveToConfig = true)
    {
        int originalNum = Plugins.Count;
        Plugins = Plugins.Where(p => p.IsValid).ToList();
        if (originalNum != Plugins.Count && saveToConfig)
            SavePluginsToConfig();
    }

    public static void ReadPluginsFromConfig()
    {
        if (!File.Exists(DataPaths.CONFIG_PLUGIN_FILE)) return;
        string jsonString = System.IO.File.ReadAllText(DataPaths.CONFIG_PLUGIN_FILE);

        JArray jarray = JArray.Parse(jsonString);
        foreach (var js in jarray.ToList())
        {
            PluginItem item = JsonSerializer.Deserialize<PluginItem>(js.ToString());
            if (item == null) continue;
            if (!Plugins.Any(p => p.MD5 == item.MD5))
                Plugins.Add(item);
        }
    }

    public static void SavePluginsToConfig()
    {
        string jsonString = JsonSerializer.Serialize(Plugins);
        if (!File.Exists(DataPaths.CONFIG_PLUGIN_FILE))
            System.IO.File.Create(DataPaths.CONFIG_PLUGIN_FILE).Close();
        System.IO.File.WriteAllText(DataPaths.CONFIG_PLUGIN_FILE, jsonString);
    }

    public static string TryGetPluginAuthor(string path, string pluginName)
    {
        string author = FileVersionInfo.GetVersionInfo(path).CompanyName;
        return string.IsNullOrWhiteSpace(author) || author == pluginName ? null : author;
    }

    public static string TryGetPluginName(string path)
    {
        var info = FileVersionInfo.GetVersionInfo(path);
        var titleList = new List<string>()
        {
            info.ProductName,
            info.CompanyName,
        }.Where(i => !string.IsNullOrWhiteSpace(i));
        return titleList.FirstOrDefault() ?? "unknown";
    }

    public static string TryGetPluginVersion(string path)
    {
        // Uncompleted (mod such as Nebula's version does not shows in file version info)
        var info = FileVersionInfo.GetVersionInfo(path);
        var verList = new List<string>()
        {
            // Sort by priority
            info.FileVersion,
            info.ProductVersion,
        }.Where(v => Version.TryParse(v, out var result) && result != new Version("1.0.0"));

        return (verList?.Any() ?? false)
            ? verList.First()
            : "unknown";
    }

}
