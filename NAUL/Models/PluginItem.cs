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

}
