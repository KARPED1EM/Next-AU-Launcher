using Microsoft.UI.Xaml.Shapes;
using NAUL.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Vanara.PInvoke;

namespace NAUL.Manager;

public static class GameStatesManager
{
    public static bool IsRunning(this VersionItem version)
    {
        var procList = Process.GetProcesses();
        return QueryProcIdByPath(version.Path + "/Among Us.exe") != null; 
    }
    private static string QueryProcIdByPath(string executablePath)
    {
        executablePath = executablePath.Replace("/", "\\\\");
        string query = $"SELECT ProcessID FROM Win32_Process WHERE ExecutablePath = '{executablePath}'";
        var searcher = new ManagementObjectSearcher(query);
        var list = searcher.Get();

        foreach(var item in list)
        {
            Debug.WriteLine(item["ProcessID"].ToString());
            return item["ProcessID"].ToString();
        }

        return null;
    }
}
