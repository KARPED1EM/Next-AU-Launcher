using NAUL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NAUL;

public static class Main
{
    // Global using variables here
    public static MainWindow mainWindow;

    public static void Init()
    {
        GamePathService.SearchAllByRegistry();

    }
}
