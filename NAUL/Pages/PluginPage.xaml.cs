using Microsoft.UI.Xaml.Controls;
using NAUL.Manager;
using NAUL.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace NAUL;

public sealed partial class Page_Plugin : Page
{
    private ObservableCollection<VersionItem> VersionItemsSource
        => VersionManager.Versions.ToObservableCollection();
    private ObservableCollection<PluginItem> SinglePluginItemsSource
        => PluginManager.Plugins.ToObservableCollection();
    private object SelectedVersion = VersionManager.SelectedVersion;

    public Page_Plugin()
    {
        this.InitializeComponent();
    }

    private void SinglePluginsRadioButtons_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        e.RemovedItems.Where(i => i != null).ToList().ForEach(i => VersionManager.SelectedVersion.SetPluginStatus((i as PluginItem), false));
        e.AddedItems.Where(i => i != null).ToList().ForEach(i => VersionManager.SelectedVersion.SetPluginStatus((i as PluginItem), true));
        VersionManager.SelectedVersion.EnabledSinglePlugin = e.AddedItems.FirstOrDefault() as PluginItem;
    }
}
