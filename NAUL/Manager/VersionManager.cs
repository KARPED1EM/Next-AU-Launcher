using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using NAUL.Models;
using NAUL.Services;

namespace NAUL.Manager;

internal class VersionManager
{
    public static List<AssemblyInfoItem> AssemblyMD5Infos = CloudService.RequestAssemblyMODInfo();
    public static List<VersionItem> Versions = new();

    private static VersionItem _SelectedVersion;
    public static VersionItem SelectedVersion { get { return _SelectedVersion ?? Versions.FirstOrDefault(); } set { _SelectedVersion = value; } }

    public static Version GetModVersion(string path)
    {
        var version = FileVersionInfo.GetVersionInfo(path).FileVersion;
        return Version.Parse(version);
    }

}
