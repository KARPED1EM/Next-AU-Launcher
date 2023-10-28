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
using NAUL.Services;
using System.Diagnostics;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace NAUL;
/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class Page_Version : Page
{
    private ObservableCollection<VersionItem> versions => VersionService.GetCollectionOfVersions();
    private Visibility versionSettingsPanelVisibility => VersionsList.SelectedIndex != -1 ? Visibility.Visible : Visibility.Collapsed;

    public Page_Version()
    {
        this.InitializeComponent();
    }

    private void VersionsList_Loaded(object sender, RoutedEventArgs e)
    {
        VersionsList.SelectedIndex = VersionService.versions.FindIndex(v => v.Name == VersionService.currentVersion.Name);
    }

    private void VersionsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var version = (VersionItem)e.AddedItems.FirstOrDefault();

        VersionService.currentVersion = version;

        GameVersionTextBlock.Text = version.GameVersion.ToString();
        GamePlatformTextBlock.Text = version.Platform.ToString();
        ModTextBlock.Text = version.IsVanilla ? "нч" : $"{version.Mod} v{version.ModVersion}";
        BepInExTextBlock.Text = version.BepInExVersion == "None" || version.IsVanilla ? "нч" : version.BepInExVersion;

        GamePathTextBox.Text = version.FolderLocation;

        VersionSettingsPanel.Visibility = Visibility.Visible;
    }

    private void MenuFlyoutItem_Click(object sender, RoutedEventArgs e)
    {

    }

    private void OepnLocation_Click(object sender, RoutedEventArgs e)
    {

    }

    private void CreateVersionButton_Click(object sender, RoutedEventArgs e)
    {
        Page_Main.Current.NavigateTo(typeof(Page_CreateVersion));
    }

}
