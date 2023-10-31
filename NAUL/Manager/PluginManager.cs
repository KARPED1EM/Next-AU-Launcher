using NAUL.Models;
using NAUL.Services;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text.Json;

namespace NAUL.Manager;

internal class PluginManager
{
    public static List<PluginItem> Plugins = new();

    public static void Init()
    {
        ReadPluginsFromConfig();
        FindPluginsFromAllGamePaths();
    }

    public static void FindPluginsFromAllGamePaths(bool saveToConfig = true)
    {
        foreach(var version in VersionManager.Versions)
        {
            string path = version.Path = "/BepInEx/plugins";
            if (!Directory.Exists(path)) continue;
            foreach (var pluginPath in Directory.EnumerateDirectories(path).Where(path => !Plugins.Any(p => p.MD5 == Utils.GetMD5HashFromFile(path))))
            {
                var plugin = new PluginItem();
                plugin.MD5 = Utils.GetMD5HashFromFile(path);
                plugin.FileName = Path.GetFileNameWithoutExtension(pluginPath);
                plugin.DisplayName = plugin.AssemblyTitle = TryGetPluginName(pluginPath);
                
                System.IO.File.Copy(pluginPath, DataPaths.SAVE_PLUGIN_PATH + plugin.MD5);
                Plugins.Add(plugin);
            }
        }
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

    public static string TryGetPluginName(string path)
    {
        Assembly assembly = Assembly.LoadFrom(path);
        var titleList = new List<string>()
        {
            assembly.GetCustomAttribute<AssemblyTitleAttribute>().Title,
            assembly.GetCustomAttribute<AssemblyProductAttribute>().Product,
            assembly.GetCustomAttribute<AssemblyCompanyAttribute>().Company,
        }.Where(i => !string.IsNullOrWhiteSpace(i));
        return titleList.FirstOrDefault() ?? "unknown";
    }

    public static string TryGetPluginVersion(string path)
    {
        // Uncompleted (mod such as Nebula's version does not shows in file version info)
        var verList = new List<string>()
        {
            // Sort by priority
            FileVersionInfo.GetVersionInfo(path).FileVersion,
            FileVersionInfo.GetVersionInfo(path).ProductVersion,
        }.Where(v => Version.TryParse(v, out var result) && result != new Version("1.0.0"));

        return (verList?.Any() ?? false)
            ? verList.First()
            : "unknown";
    }

}
