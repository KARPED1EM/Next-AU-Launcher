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
    private ObservableCollection<PluginItem> PluginsListItemsSource
        => PluginManager.Plugins.ToObservableCollection();

    public Page_Plugin()
    {
        this.InitializeComponent();
    }

}
