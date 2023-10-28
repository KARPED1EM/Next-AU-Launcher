using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using NAUL.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.AccessCache;
using Windows.Storage.Pickers;
using Windows.Storage;
using Windows.UI.Popups;
using System.Threading.Tasks;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace NAUL;
/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
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
