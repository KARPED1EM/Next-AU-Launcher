﻿using NAUL.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Management;
using System.Threading;

namespace NAUL.Manager;

public static class GameStatesManager
{
    public static Dictionary<VersionItem, int> PID { get; private set; } = new();

    public static bool IsRunning(this VersionItem version)
    {
        int pid = QueryProcIdByPath(version.ExecutablePath);
        if (pid == -1) return false;

        PID.Remove(version);
        PID.Add(version, pid);
        return true;
    }
    public static void Run(this VersionItem version)
    {
        switch (version.GamePlatform)
        {
            case GamePlatforms.Local:
                var proc = Process.Start(version.ExecutablePath);
                if (proc == null) return;
                PID.Remove(version);
                PID.Add(version, proc.Id);
                break;
            case GamePlatforms.Steam:
                RunBySteam();
                Thread.Sleep(1000);
                QueryProcIdByPath(version.ExecutablePath);
                break;
            case GamePlatforms.Epic:
                RunByEpic();
                Thread.Sleep(1000);
                QueryProcIdByPath(version.ExecutablePath);
                break;
        }
    }
    public static void Terminate(this VersionItem version)
    {
        int pid = PID.TryGetValue(version, out pid) ? pid : QueryProcIdByPath(version.ExecutablePath);
        if (pid != -1)
        {
            var proc = Process.GetProcessById(pid);
            if (proc != null)
            {
                if (!proc.CloseMainWindow())
                    proc.Kill();
            }
        }
    }

    private const string SteamCommand = "steam://rungameid/945360";
    private const string EpicCommand = "com.epicgames.launcher://apps/33956bcb55d4452d8c47e16b94e294bd%3A729a86a5146640a2ace9e8c595414c56%3A963137e4c29d4c79a81323b8fab03a40?action=launch&silent=true";
    public static void RunBySteam() => RunCommandByCmd(SteamCommand);
    public static void RunByEpic() => RunCommandByCmd(EpicCommand);
    public static void RunCommandByCmd(string cmd)
    {
        var psi = new ProcessStartInfo
        {
            FileName = "cmd.exe",
            Arguments = "/c start /B " + cmd,
            CreateNoWindow = true,
            UseShellExecute = false,
            RedirectStandardOutput = true
        };
        Process.Start(psi);
    }

    private static int QueryProcIdByPath(string executablePath)
    {
        executablePath = executablePath.Replace("/", "\\\\");
        string query = $"SELECT ProcessID FROM Win32_Process WHERE ExecutablePath = '{executablePath}'";
        var searcher = new ManagementObjectSearcher(query);
        var list = searcher.Get();

        foreach(var item in list)
            return int.Parse(item["ProcessID"].ToString());
        return -1;
    }
}
