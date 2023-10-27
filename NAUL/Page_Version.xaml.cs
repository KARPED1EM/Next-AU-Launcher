using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using System.Collections.ObjectModel;
using Microsoft.UI.Xaml.Documents;
using System.Security.AccessControl;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace NAUL;
/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class Page_Version : Page
{
    private ObservableCollection<VersionItem> versions = new();
    public Page_Version()
    {
        this.InitializeComponent();
        versions = SetData();
    }
    private ObservableCollection<VersionItem> SetData()
    {
        var data = new ObservableCollection<VersionItem>
        {
            new VersionItem("TONX", "Beta", "2023.10.24s", VersionPlatform.Steam, "F:/Game/Platform/Steam/steamapps/common/Among Us/"),
            new VersionItem("TOHE", "2.3.6", "2023.10.24e", VersionPlatform.Epic, ""),
            new VersionItem("TOH", "5.1.1", "2023.10.24e", VersionPlatform.Epic, ""),
            new VersionItem("Extream Roles", "9.0.0.0", "2023.10.24s", VersionPlatform.Steam, ""),
        };
        return data;
    }

    private void VersionsList_ItemClick(object sender, ItemClickEventArgs e)
    {
        
    }

    private void MenuFlyoutItem_Click(object sender, RoutedEventArgs e)
    {

    }

    private void OepnLocation_Click(object sender, RoutedEventArgs e)
    {

    }

    private void CreateVersionButton_Click(object sender, RoutedEventArgs e)
    {
        Main.mainWindow.NavigateTo(typeof(Page_CreateVersion));
    }
}

public class VersionItem
{
    public string Name { get; set; }
    public string ModVersion { get; set; }
    public string GameVersion { get; set; }
    public VersionPlatform Platform { get; set; }
    public string FolderLocation { get; set; }
    public string FontGlyph { get; set; }

    public VersionItem(string name, string modVersion, string gameVersion, VersionPlatform platform, string folderLocation, string fontGlyph = "\uE7FC")
    {
        bool broken = !Directory.Exists(folderLocation);
        this.Name = (broken ? "(��Ч) " : string.Empty) + name;
        this.ModVersion = modVersion;
        this.GameVersion = gameVersion;
        this.Platform = platform;
        this.FolderLocation = folderLocation;
        this.FontGlyph = broken ? "\uE729" : fontGlyph;
    }
}

public enum VersionPlatform
{
    Steam,
    Epic
}
