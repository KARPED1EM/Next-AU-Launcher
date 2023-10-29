using System.Collections.Generic;
using NAUL.Manager;
using NAUL.Models;

namespace NAUL.Services;

internal class CloudService
{
    public static List<AssemblyInfoItem> RequestAssemblyMODInfo()
    {
        // Wait for compelete
        var list = new List<AssemblyInfoItem>
        {
            // Steam
            new AssemblyInfoItem("076e8c8e0ec61642f4d276f23fc759a8", GamePlatforms.Steam, new("2023.10.24")),
            new AssemblyInfoItem("4c159725b4872eda509dfecfef3d0293", GamePlatforms.Steam, new("2023.7.12")),
            new AssemblyInfoItem("718d801c5049a6acf2a1cc132c48aed8", GamePlatforms.Steam, new("2023.6.13")),
            new AssemblyInfoItem("1bf15d0a96942368f47fb453b4a8d037", GamePlatforms.Steam, new("2023.3.28")),
            // Epic
            new AssemblyInfoItem("8a3ae7e799e506aea5de1e72a846c87d", GamePlatforms.Epic, new("2023.10.24")),
            new AssemblyInfoItem("499bf5c2fc6aeb335e380f8156b6569d", GamePlatforms.Epic, new("2023.7.12"))
        };
        return list;
    }
}
