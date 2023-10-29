using System;

namespace NAUL.Models;

public class AssemblyInfoItem
{
    public string MD5 { get; set; }

    public GamePlatforms Platform { get; set; }

    public Version Version { get; set; }

    public AssemblyInfoItem(string md5, GamePlatforms platform, Version version)
    {
        this.MD5 = md5;
        this.Platform = platform;
        this.Version = version;
    }

}