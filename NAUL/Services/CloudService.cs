using NAUL.Models;
using System.Collections.Generic;

namespace NAUL.Services;

internal class CloudService
{
    public static List<AssemblyInfoItem> RequestAssemblyMODInfo()
    {
        // Wait for compelete
        var list = new List<AssemblyInfoItem>()
        {
            // Steam
            new ("07b382a782ea344a5eae9f57d8ea4054", GamePlatforms.Steam, new("2023.11.28")),
            new ("076e8c8e0ec61642f4d276f23fc759a8", GamePlatforms.Steam, new("2023.10.24")),
            new ("4c159725b4872eda509dfecfef3d0293", GamePlatforms.Steam, new("2023.7.12")),
            new ("718d801c5049a6acf2a1cc132c48aed8", GamePlatforms.Steam, new("2023.6.13")),
            new ("1bf15d0a96942368f47fb453b4a8d037", GamePlatforms.Steam, new("2023.3.28")),
            // Epic
            new ("2c504162c16af930a7176361a3558d71", GamePlatforms.Epic, new("2023.11.28")),
            new ("8a3ae7e799e506aea5de1e72a846c87d", GamePlatforms.Epic, new("2023.10.24")),
            new ("499bf5c2fc6aeb335e380f8156b6569d", GamePlatforms.Epic, new("2023.7.12"))
        };
        return list;
    }

    public static List<PluginInfoItem> RequestPluginInfos()
    {
        // Wait for compelete
        var list = new List<PluginInfoItem>()
        {
            new()
            {
                PluginName = "TONX",
                PluginType = PluginTypes.Single,
                IconUrl = null,
                Author = "KARPED1EM",
                URL = "https://tonx.cc",
                License = "GPL-3.0",
                Description = "A TOH branch mod",
            }
        };
        return list;
    }
}
