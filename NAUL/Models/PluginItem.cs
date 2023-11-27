﻿using NAUL.Manager;
using System;

namespace NAUL.Models;

public class PluginItem : PluginInfoItem
{
    public string FileName { get; set; }

    public string DisplayName { get; set; }

    public Version PluginVersion { get; set; }

    public string MD5 { get; set; }

    public bool IsValid => File.Exists(DataPaths.SAVE_PLUGIN_PATH + "/" + MD5);
    public string Path => DataPaths.SAVE_PLUGIN_PATH + MD5;
    public bool HasURL => !string.IsNullOrWhiteSpace(URL);
    public bool IsEnabledForThisVersion => VersionManager.SelectedVersion?.IsPluginEnabled(this) ?? false;
    public bool IsEnabledSinglePluginForThisVersion => VersionManager.SelectedVersion?.EnabledSinglePlugin == this;

    public void TryGetInfoFromCloud()
    {
        var info = PluginManager.PluginInfos.Find(i => i.PluginName == this.PluginName);
        if (info == null) return;

        PluginType = info.PluginType;
        IconUrl = info.IconUrl;
        Author = info.Author;
        URL = info.URL;
        License = info.License;
        Description = info.Description;
    }
}