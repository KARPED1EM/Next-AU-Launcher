using NAUL.Models;
using NAUL.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NAUL.Manager;

internal class PluginManager
{
    public static string PLUGIN_SAVE_PATH => Appconfig.CONFIG_SAVE_PATH + @"/Plugins/";

    /*
    public static void SearchAllVersion()
    {
        foreach (var gamePath in FindGameService.FoundGamePaths)
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
    } */
}
