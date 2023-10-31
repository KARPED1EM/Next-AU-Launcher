using Microsoft.UI.Composition;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Hosting;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Linq;
using System.Numerics;
using Windows.Foundation;
using Windows.Graphics;

namespace NAUL;

public sealed partial class Page_Main : Page
{
    public static Page_Main Current { get; private set; }
    private readonly Compositor compositor;
    private ImageSource backgroundImage = new BitmapImage(new Uri("file:///D:/Desktop/NAUL/NAUL/Assets/BG.png"));

    public Page_Main()
    {
        Current = this;
        this.InitializeComponent();
        compositor = ElementCompositionPreview.GetElementVisual(this).Compositor;
        NavigateTo(typeof(Page_Play));
    }

    private async void Page_Loaded(object sender, RoutedEventArgs e)
    {
        //InitializeSystemTray();
        //await UpdateBackgroundImageAsync(true);
        //await CheckUpdateAsync();
    }

    private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        InitializeTitleBarBackground();
        UpdateDragRectangles();
    }

    public bool IsPaneToggleButtonVisible
    {
        get => GlobalNavigation.IsPaneToggleButtonVisible;
        set
        {
            GlobalNavigation.IsPaneToggleButtonVisible = value;
            Border_TitleText.Visibility = value ? Visibility.Visible : Visibility.Collapsed;
        }
    }

    private void InitializeTitleBarBackground()
    {
        var surface = compositor.CreateVisualSurface();
        surface.SourceOffset = Vector2.Zero;
        surface.SourceVisual = ElementCompositionPreview.GetElementVisual(Border_ContentImage);
        surface.SourceSize = new Vector2((float)Border_TitleBar.ActualWidth, 12);
        var visual = compositor.CreateSpriteVisual();
        visual.Size = new Vector2((float)Border_TitleBar.ActualWidth, (float)Border_TitleBar.ActualHeight);
        var brush = compositor.CreateSurfaceBrush(surface);
        brush.Stretch = CompositionStretch.Fill;
        visual.Brush = brush;
        ElementCompositionPreview.SetElementChildVisual(Border_TitleBar, visual);
    }

    public void UpdateDragRectangles()
    {
        try
        {
            var scale = MainWindow.Current.UIScale;
            var point = Grid_TitleBar.TransformToVisual(this).TransformPoint(new Point());
            var width = Grid_TitleBar.ActualWidth;
            var height = Grid_TitleBar.ActualHeight;
            int len = (int)(48 * scale);
            var rect1 = new RectInt32(len, 0, (int)((point.X - 48) * scale), len);
            var rect2 = new RectInt32((int)((point.X + width) * scale), 0, 100000, len);
            MainWindow.Current.SetDragRectangles(rect1, rect2);
        }
        catch { }
    }

    private async void GlobalNavigation_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
    {
        if (args.InvokedItemContainer?.IsSelected ?? false || args.IsSettingsInvoked) return;
        var item = args.InvokedItemContainer as NavigationViewItem;
        if (item == null) return;
        Type pageType = item.Tag switch
        {
            "Page_Play" => typeof(Page_Play),
            "Page_Version" => typeof(Page_Version),
            "Page_Plugin" => typeof(Page_Plugin),
            "Page_About" => typeof(Page_About),
            "Page_Setting" => typeof(Page_Setting),
            _ => typeof(Page_Play),
        };
        NavigateTo(pageType);
    }

#nullable enable
    public void NavigateTo(Type? page, object? param = null)
    {
        Border_ContentBackground.Visibility = Visibility.Visible;
        string? sourcePage = Content_Frame.CurrentSourcePageType?.Name, destPage = page?.Name;
        if (page is null)
        {
            page = typeof(Page_Play);
            destPage = nameof(Page_Play);
        }

        GlobalNavigation.SelectedItem = GlobalNavigation.MenuItems.ToList().Find(item => (item as NavigationViewItem)?.Tag.ToString() == page?.Name);

        Content_Frame.Navigate(page, param);

        Border_ContentBackground.Opacity = destPage is nameof(Page_Play) ? 0 : 1;

        IsPaneToggleButtonVisible = true;
    }
#nullable disable

}
