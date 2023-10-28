using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using NAUL.Services;
using System;
using System.Collections.ObjectModel;
using Windows.Storage;
using Windows.Storage.Pickers;

namespace NAUL;

public sealed partial class Page_CreateVersion : Page
{
    private bool isSelectedPathValid => GamePathService.IsValidAmongUsFolder(SelectGameFolderCombo.Text);
    private ObservableCollection<string> gamePaths => GamePathService.GetCollectionOfGamePaths();

    public Page_CreateVersion()
    {
        this.InitializeComponent();
    }

    private void BackButton_Click(object sender, RoutedEventArgs e)
    {
        Page_Main.Current.NavigateTo(typeof(Page_Version));
    }

    private void SelectGameFolderCombo_Loaded(object sender, RoutedEventArgs e)
    {
        if (SelectGameFolderCombo.Items.Count >= 1)
            SelectGameFolderCombo.SelectedIndex = 0;
    }

    private async void PickGameFolderButton_Click(object sender, RoutedEventArgs e)
    {

        FolderPicker openPicker = new FolderPicker();
        WinRT.Interop.InitializeWithWindow.Initialize(openPicker, MainWindow.Current.hWnd);

        openPicker.SuggestedStartLocation = PickerLocationId.Desktop;
        openPicker.FileTypeFilter.Add("*");

        StorageFolder folder = await openPicker.PickSingleFolderAsync();
        if (folder != null)
        {
            //StorageApplicationPermissions.FutureAccessList.AddOrReplace("PickedFolderToken", folder);
            SelectGameFolderCombo.Text = folder.Path;
            CheckForPathAndUpdateUI(folder.Path);
        }
    }

    private void SelectGameFolderCombo_TextSubmitted(ComboBox sender, ComboBoxTextSubmittedEventArgs args)
        => CheckForPathAndUpdateUI(args.Text);

    private void CheckForPathAndUpdateUI(string path)
    {
        if (GamePathService.IsValidAmongUsFolder(path))
        {
            GamePathService.AddGamePath(path);
            NotificationForGameFolder.Visibility = Visibility.Collapsed;
        }
        else
        {
            NotificationForGameFolder.Visibility = Visibility.Visible;
        }
    }


}
