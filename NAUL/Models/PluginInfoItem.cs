using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NAUL.Models;

public class PluginInfoItem
{
    public string AssemblyTitle { get; set; }

    public bool IsSingleMod { get; set; }

    public string IconUrl { get; set; }

    public string Author { get; set; }

    public string URL { get; set; }

    public string License { get; set; }

    public string Description { get; set; }
}
