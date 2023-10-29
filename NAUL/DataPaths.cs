using System;

namespace NAUL;

/// <summary>
/// All folder paths end with "/"
/// </summary>
public static class DataPaths
{
    // Setted data save folder
    public static string SAVE_DATA_PATH => Environment.GetFolderPath(0) + @"/NAUL_DATA/";

    // Different categories of data storage locations
    public static string SAVE_CONFIG_PATH => SAVE_DATA_PATH + @"Configs/";
    public static string SAVE_PLUGIN_PATH => SAVE_DATA_PATH + @"Plugins/";

    // Text data file name
    public static string CONFIG_APP_PATH => SAVE_CONFIG_PATH + @"App.ini";
    public static string CONFIG_PLUGIN_PATH => SAVE_CONFIG_PATH + @"Plugin.json";

}