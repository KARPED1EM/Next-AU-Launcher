using Microsoft.UI.Xaml.Controls;
using NAUL.Manager;
using NAUL.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace NAUL;

public sealed partial class Page_Plugin : Page
{
    public static Page_Plugin Current { get; private set; }

    private ObservableCollection<VersionItem> VersionItemsSource
        => VersionManager.AllVersions.ToObservableCollection();
    private ObservableCollection<PluginItem> SinglePluginItemsSource
        => PluginManager.AllSinglePlugins.ToObservableCollection();
    private ObservableCollection<PluginItem> AdditionalPluginItemsSource
        => PluginManager.AllAdditionalPlugins.ToObservableCollection();

    public Page_Plugin()
    {
        Current = this;

        this.InitializeComponent();
    }

    private void SinglePluginsRadioButtons_Loaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        => (sender as RadioButtons).SelectedItem = PluginManager.AllSinglePlugins.ToList().Find(p => p.IsEnabledForThisVersion);

    private void SinglePluginsRadioButtons_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        e.RemovedItems.Where(i => i != null).ToList().ForEach(i => VersionManager.SelectedVersion.SetPluginStatus((i as PluginItem), false));
        e.AddedItems.Where(i => i != null).ToList().ForEach(i => VersionManager.SelectedVersion.SetPluginStatus((i as PluginItem), true));
        VersionManager.SelectedVersion.EnabledSinglePlugin = e.AddedItems.FirstOrDefault() as PluginItem;
    }

    private void AdditionalPluginItemsCheckBox_Checked(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        var plugin = PluginManager.AllPlugins.Find(p => p.MD5 == (sender as CheckBox).Tag.ToString());
        if (plugin == null) return;
        VersionManager.SelectedVersion.SetPluginStatus(plugin, true);
    }

    private void AdditionalPluginItemsCheckBox_Unchecked(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        var plugin = PluginManager.AllPlugins.Find(p => p.MD5 == (sender as CheckBox).Tag.ToString());
        if (plugin == null) return;
        VersionManager.SelectedVersion.SetPluginStatus(plugin, false);
    }

    private void SelectVersionComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var comboBox = sender as ComboBox;
        var addedItem = (VersionItem)e.AddedItems.FirstOrDefault();
        var removedItem = (VersionItem)e.RemovedItems.FirstOrDefault();

        if (!e.RemovedItems.Any()
            || !e.AddedItems.Any()
            || addedItem == removedItem
            || addedItem == VersionManager.SelectedVersion) return;

        if (!addedItem.HasBepInExInstalled)
        {
            _ = Page_Dialog.Create("该版本未安装BepInEx", "游戏必须安装BepInEx以使用模组").ShowAsync();
            comboBox.SelectedItem = removedItem;
            return;
        }

        VersionManager.SelectedVersion = (e.AddedItems.FirstOrDefault() as VersionItem);
        Bindings.Update();
        comboBox.SelectedItem = VersionManager.SelectedVersion;
    }

    private void SelectVersionComboBox_Loaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        => (sender as ComboBox).SelectedItem = VersionManager.SelectedVersion;
}
