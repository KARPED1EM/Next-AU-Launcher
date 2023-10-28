using System;
using System.Collections.Generic;
using NAUL.Services;

namespace NAUL.Services;

internal class CloudService
{
    public static List<AssemblyMD5Info> RequestAssemblyMODInfo()
    {
        // Wait for compelete
        var list = new List<AssemblyMD5Info>
        {
            // Steam
            new AssemblyMD5Info("076e8c8e0ec61642f4d276f23fc759a8", GamePlatform.Steam, new("2023.10.24")),
            new AssemblyMD5Info("4c159725b4872eda509dfecfef3d0293", GamePlatform.Steam, new("2023.7.12")),
            new AssemblyMD5Info("718d801c5049a6acf2a1cc132c48aed8", GamePlatform.Steam, new("2023.6.13")),
            new AssemblyMD5Info("1bf15d0a96942368f47fb453b4a8d037", GamePlatform.Steam, new("2023.3.28")),
            // Epic
            new AssemblyMD5Info("8a3ae7e799e506aea5de1e72a846c87d", GamePlatform.Epic, new("2023.10.24")),
            new AssemblyMD5Info("499bf5c2fc6aeb335e380f8156b6569d", GamePlatform.Epic, new("2023.7.12"))
        };
        return list;
    }
}
