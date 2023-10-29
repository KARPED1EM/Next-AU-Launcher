using System;
using NAUL.Manager;

namespace NAUL.Models;

public class PluginItem
{
    public string FileName { get; set; }

    public string DisplayName { get; set; }

    public Version PluginVersion { get; set; }

    public bool IsSingleMod { get; set; }

    public string MD5 { get; set; }

    public string IconUrl { get; set; }

    public string Author { get; set; }

    public string URL { get; set; }

    public string License { get; set; }

    public string Description { get; set; }

    public bool IsValid() => File.Exists(PluginManager.PLUGIN_SAVE_PATH + "/" + MD5);

}
