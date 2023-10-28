﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace NAUL.Services;

internal class GamePathService
{
    public static IReadOnlyList<string> GamePaths => gamePaths;
    private static List<string> gamePaths = new();

    protected static readonly List<(RegistryKey, string)> registryKeysToSearch = new()
    {
        // Config of TONX
        (Registry.CurrentUser, @"Software\AU-TONX\"),

        // Install path of AmongUs
        (Registry.CurrentUser, @"Software\Microsoft\Windows\CurrentVersion\Explorer\FeatureUsage\AppSwitched\"),
        (Registry.CurrentUser, @"Software\Microsoft\Windows\CurrentVersion\Explorer\FeatureUsage\ShowJumpView\"),
        (Registry.CurrentUser, @"Software\Microsoft\Windows NT\CurrentVersion\AppCompatFlags\Compatibility Assistant\Store\"),
        (Registry.CurrentUser, @"Software\Classes\Local Settings\Software\Microsoft\Windows\Shell\MuiCache\"),

        // Install path of Steam
        (Registry.LocalMachine, @"SOFTWARE\WOW6432Node\Valve\Steam"),
        (Registry.LocalMachine, @"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall\Steam"),
    };

    public static ObservableCollection<string> GetCollectionOfGamePaths()
    {
        var collection = new ObservableCollection<string>();
        foreach (var path in GamePaths)
            collection.Add(path);
        return collection;
    }
    public static void AddGamePath(string path)
    {
        path = path.TrimEnd('\\');
        if (GamePaths.Contains(path)) return;
        gamePaths.Add(path);
    }

    public static void SearchAllByRegistry()
    {
        foreach (var kvp in registryKeysToSearch)
        {
            var (valid, path) = FindValidPathInKey(kvp.Item1, kvp.Item2);
            if (valid) gamePaths.Add(path);
        }
    }
    private static (bool, string) FindValidPathInKey(RegistryKey registerRoot, string inputKey)
    {
        var notFound = (false, string.Empty);
        var keyTree = registerRoot.OpenSubKey(inputKey);
        if (keyTree == null) return notFound;
        var keys = keyTree.GetValueNames().Where(k => k.Contains("Among Us.exe")).ToList();
        if (keys == null || keys.Count < 1) return notFound;

        foreach (var key in keys)
        {
            string path = key;

            // Remove .FriendlyAppName suffix when searching in MuiCache
            if (path.EndsWith(".FriendlyAppName"))
                path = path.Remove(path.IndexOf(".FriendlyAppName"));
            // Get superior directory when key is point to a executable file
            if (path.EndsWith(".exe"))
                path = Path.GetDirectoryName(path);
            // Point to AmongUs folder when key is Steam Folder
            if (path.EndsWith("Steam"))
                path += @"\steamapps\common\Among Us\";
            else if (path.EndsWith("Steam\\"))
                path += @"steamapps\common\Among Us\";

            path = TrimGamePath(path, false);
            if (!IsValidAmongUsFolder(path)) continue;
            if (GamePaths.Contains(path)) continue;
            return (true, path);
        }

        return notFound;
    }
    public static bool IsValidAmongUsFolder(string path)
    {
        if (string.IsNullOrWhiteSpace(path)) return false;
        path = TrimGamePath(path);
        return !(
            !Directory.Exists(path)
            || !Directory.Exists(path + "Among Us_Data")
            || !File.Exists(path + "Among Us.exe")
            || !File.Exists(path + "GameAssembly.dll")
            || !File.Exists(path + "UnityCrashHandler32.exe")
            );
    }
    private static string TrimGamePath(string path, bool endWithSolidus = true)
    {
        if (path.EndsWith("Among Us.exe")) path = Path.GetDirectoryName(path) ?? "";
        path = path.Replace("\\", "/").TrimEnd('/');
        if (endWithSolidus) path += "/";
        return path;
    }
}
