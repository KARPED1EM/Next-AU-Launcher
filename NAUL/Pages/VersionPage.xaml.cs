using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using NAUL.Manager;
using NAUL.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace NAUL;

public sealed partial class Page_Version : Page
{
    private ObservableCollection<VersionItem> VersionsListItemsSource
        => VersionManager.Versions.ToObservableCollection();
    private bool VersionSettingsGridVisibility => VersionsList.SelectedIndex != -1;

    public Page_Version()
    {
        this.InitializeComponent();
    }

    private void VersionsList_Loaded(object sender, RoutedEventArgs e)
    {
        VersionsList.SelectedIndex = VersionManager.Versions.FindIndex(v => v.Path == VersionManager.SelectedVersion.Path);
    }

    private void VersionsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var version = (VersionItem)e.AddedItems.FirstOrDefault();

        VersionManager.SelectedVersion = version;

        GameVersionTextBlock.Text = version.GameVersion.ToString();
        GamePlatformTextBlock.Text = version.GamePlatform.ToString();
        BepInExTextBlock.Text = version.HasBepInExInstalled ? version.BepInExVersion : "нч";

        GamePathTextBox.Text = version.Path;

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

    private void DeleteVersionButton_Click(object sender, RoutedEventArgs e)
    {

    }

    private void StartVersionButton_Click(object sender, RoutedEventArgs e)
    {

    }
}
