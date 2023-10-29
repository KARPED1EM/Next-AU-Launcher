using System;

namespace NAUL.Models;

public class AssemblyInfoItem
{
    public string MD5 { get; set; }

    public GamePlatforms Platform { get; set; }

    public Version Version { get; set; }

}