using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NAUL;

internal class Appconfig
{
    public static string CONFIG_SAVE_PATH => Environment.GetFolderPath(0) + @"/NAUL_Config";

    public static string PLUGIN_SAVE_PATH => CONFIG_SAVE_PATH + @"/Plugins/";
}
