using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using NAUL.Manager;
using System.Collections.ObjectModel;
using System.Linq;

namespace NAUL;

public sealed partial class Page_Version : Page
{
    private ObservableCollection<VersionItem> versions => VersionManager.GetCollectionOfVersions();
    private Visibility versionSettingsGridVisibility => VersionsList.SelectedIndex != -1 ? Visibility.Visible : Visibility.Collapsed;

    public Page_Version()
    {
        this.InitializeComponent();
    }

    private void VersionsList_Loaded(object sender, RoutedEventArgs e)
    {
        VersionsList.SelectedIndex = VersionManager.versions.FindIndex(v => v.Name == VersionManager.currentVersion.Name);
    }

    private void VersionsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var version = (VersionItem)e.AddedItems.FirstOrDefault();

        VersionManager.currentVersion = version;

        GameVersionTextBlock.Text = version.GameVersion.ToString();
        GamePlatformTextBlock.Text = version.Platform.ToString();
        ModTextBlock.Text = version.IsVanilla ? "нч" : $"{version.Mod} v{version.ModVersion}";
        BepInExTextBlock.Text = version.BepInExVersion == "None" || version.IsVanilla ? "нч" : version.BepInExVersion;

        GamePathTextBox.Text = version.FolderLocation;

        VersionSettingsGrid.Visibility = Visibility.Visible;
    }

    private void MenuFlyoutItem_Click(object sender, RoutedEventArgs e)
    {

    }

    private void OepnLocation_Click(object sender, RoutedEventArgs e)
    {

    }

    private void CreateVersionButton_Click(object sender, RoutedEventArgs e)
        => Page_Main.Current.NavigateTo(typeof(Page_CreateVersion));

    private void JumpToPlayButton_Click(object sender, RoutedEventArgs e)
        => Page_Main.Current.NavigateTo(typeof(Page_Play));
}
