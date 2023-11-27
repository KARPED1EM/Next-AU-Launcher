using NAUL.Models;
using NAUL.Services;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace NAUL.Manager;

public static class VersionManager
{
    public static List<AssemblyInfoItem> AssemblyMD5Infos = CloudService.RequestAssemblyMODInfo();
    public static List<VersionItem> Versions = new();

    private static VersionItem _SelectedVersion;
    public static VersionItem SelectedVersion { get { return _SelectedVersion ?? Versions.FirstOrDefault(); } set { _SelectedVersion = value; } }

    public static void Init()
    {
        ReadVersionsFromConfig();
        LoadVersionsFromGameFinder();
    }

    public static void LoadVersionsFromGameFinder(bool saveToConfig = true)
    {
        FindGameService.SearchAllByRegistry();
        bool needSave = false;
        foreach (var path in FindGameService.FoundGamePaths.Where(p => !Versions.Any(v => v.Path == p)))
        {
            VersionItem item = new()
            {
                DisplayGlyph = "\uE7FC",
                Path = path
            };
            item.DisplayName = item.GamePlatform switch
            {
                GamePlatforms.Steam => "Steam",
                GamePlatforms.Epic => "Epic",
                _ => "本地",
            };

            Versions.Add(item);
            needSave = true;
        }
        if (needSave && saveToConfig) SaveVersionsToConfig();
    }

    public static void ReadVersionsFromConfig()
    {
        if (!File.Exists(DataPaths.CONFIG_VERSION_FILE)) return;
        string jsonString = System.IO.File.ReadAllText(DataPaths.CONFIG_VERSION_FILE);

        JArray jarray = JArray.Parse(jsonString);
        foreach (var js in jarray.ToList())
        {
            VersionItem item = JsonSerializer.Deserialize<VersionItem>(js.ToString());
            if (item == null) continue;
            if (!Versions.Any(v => v.Path == item.Path))
                Versions.Add(item);
        }
    }

    public static void SaveVersionsToConfig()
    {
        string jsonString = JsonSerializer.Serialize(Versions);
        if (!File.Exists(DataPaths.CONFIG_VERSION_FILE))
            System.IO.File.Create(DataPaths.CONFIG_VERSION_FILE).Close();
        System.IO.File.WriteAllText(DataPaths.CONFIG_VERSION_FILE, jsonString);
    }
}
