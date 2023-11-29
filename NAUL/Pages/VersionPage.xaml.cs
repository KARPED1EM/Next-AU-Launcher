using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using NAUL.Manager;
using NAUL.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;

namespace NAUL;

public sealed partial class Page_Version : Page
{
    private ObservableCollection<VersionItem> VersionsListItemsSource
        => VersionManager.AllVersions.ToObservableCollection();
    private bool VersionSettingsGridVisibility => VersionsList.SelectedIndex != -1;

    public Page_Version()
    {
        this.InitializeComponent();
    }

    private void VersionsList_Loaded(object sender, RoutedEventArgs e)
    {
        VersionsList.SelectedIndex = VersionManager.AllVersions.FindIndex(v => v.Path == VersionManager.SelectedVersion.Path);
    }

    private void VersionsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var version = (VersionItem)e.AddedItems.FirstOrDefault();
        if (version == null)
        {
            VersionManager.SelectedVersion = null;
            VersionSettingsGrid.Visibility = Visibility.Collapsed;
            return;
        }

        VersionManager.SelectedVersion = version;
        VersionSettingsGrid.Visibility = Visibility.Visible;

        string versionText = version.GameVersion?.ToString();
        GameVersionTextBlock.Text = string.IsNullOrEmpty(versionText) ? "未知版本" : versionText;
        GamePlatformTextBlock.Text = version.GamePlatform.ToString();
        BepInExTextBlock.Text = version.HasBepInExInstalled ? version.BepInExVersion : "无";

        GamePathTextBox.Text = version.Path;

        VersionSettingsGrid.Visibility = Visibility.Visible;
    }

    private void CreateVersionButton_Click(object sender, RoutedEventArgs e)
        => PageControl.NavigateTo(typeof(Page_CreateVersion));

    private void JumpToPlayButton_Click(object sender, RoutedEventArgs e)
        => PageControl.NavigateTo(typeof(Page_Play));

    private async void DeleteVersionButton_Click(object sender, RoutedEventArgs e)
    {
        var act = await Page_Dialog.Create("确定要删除吗？", "此操作无法撤回，该版本的全部游戏文件将被删除。\n插件与模组不受影响。", new()
        {
            PrimaryButtonText = "确认删除",
            CloseButtonText = "取消",
        }).ShowAsync();
        if (act != ContentDialogResult.Primary) return;

        VersionManager.SelectedVersion.Terminate();
        int waited = 1;
        while(waited < 3 || VersionManager.SelectedVersion.IsRunning())
        {
            Thread.Sleep(300);
            waited++;
        }

        if (VersionManager.SelectedVersion.Delete(out var reason))
        {
            Bindings.Update();
            VersionsList.SelectedIndex = VersionManager.AllVersions.FindIndex(v => v.Path == VersionManager.SelectedVersion.Path);
        }
        else
        {
            _ = Page_Dialog.Create("删除版本失败", reason).ShowAsync();
        }
    }

    private void StartVersionButton_Click(object sender, RoutedEventArgs e)
        => VersionManager.SelectedVersion?.Run();

    private void OepnLocationButton_Click(object sender, RoutedEventArgs e)
        => VersionManager.SelectedVersion?.OpenInExplorer();
}
