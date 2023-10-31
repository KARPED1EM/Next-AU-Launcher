using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using NAUL.Manager;
using NAUL.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

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

    private void SinglePluginsRadioButtons_Unloaded(object sender, RoutedEventArgs e)
    {
        
    }
}
