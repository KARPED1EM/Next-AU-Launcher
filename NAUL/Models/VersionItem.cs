using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;
using NAUL.Manager;
using NAUL.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NAUL.Models;

public class VersionItem
{
    public string UID { get; private set; }
    public string DisplayName { get; set; }
    public string DisplayGlyph { get; set; }
    public Version GameVersion { get => GetGameVersionByAssemblyFile(); }
    public GamePlatforms GamePlatform { get => GetGamePlatformByPath(); }
    public bool HasBepInExInstalled { get => CheckIntegrityOfBepInEx(); }
    public string BepInExVersion { get => GetBepInExVersion(); }

    private string _Path;
    public string Path
    {
        get => _Path;
        set => FormatAndSetPath(value);
    }

    public bool IsValid => GamePathService.IsValidAmongUsFolder(Path);
    public bool IsBepInExEnabled => File.IsEnabled(Path + "/winhttp.dll");
    
    private void FormatAndSetPath(string path)
    {
        _Path = path.Replace("\\", "/").TrimEnd('/');
    }
    private Version GetGameVersionByAssemblyFile()
    {
        string md5 = Utils.GetMD5HashFromFile(Path + "/GameAssembly.dll");
        return VersionManager.AssemblyMD5Infos.Find(info => info.MD5 == md5)?.Version;
    }
    private bool CheckIntegrityOfBepInEx() // Incomplete
    {
        return !(
            !Directory.Exists(Path)
            || !Directory.Exists(Path + "/BepInEx")
            || !Directory.Exists(Path + "/dotnet")
            || (!File.Exists(Path + "/winhttp.dll"))
            );
    }
    private string GetBepInExVersion()
    {
        string fileName = Path + "/BepInEx/core/BepInEx.Core.dll";
        if (!HasBepInExInstalled || !File.Exists(fileName)) return null;
        string version = FileVersionInfo.GetVersionInfo(fileName).ProductVersion;
        version = Regex.Replace(version, @"\+.+", string.Empty);
        return version;
    }
    private GamePlatforms GetGamePlatformByPath()
    {
        if (Directory.Exists(Path + "/.egstore")) return GamePlatforms.Epic;
        else if (System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(Path)).EndsWith("steamapps")) return GamePlatforms.Steam;
        else return GamePlatforms.Local;
    }

    public void SetBepInExStatus(bool enable)
    {
        File.SetStatus(Path + "/winhttp.dll", enable);
    }


}

public enum GamePlatforms
{
    Local,
    Steam,
    Epic,
}