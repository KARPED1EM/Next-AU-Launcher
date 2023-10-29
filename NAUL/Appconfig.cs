using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NAUL;

internal class Appconfig
{
    public static string CONFIG_SAVE_PATH => Environment.GetFolderPath(0) + "/NAUL_Config";
}
