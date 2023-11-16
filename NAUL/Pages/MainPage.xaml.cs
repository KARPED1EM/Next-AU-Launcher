using Microsoft.UI.Composition;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Hosting;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using NAUL.Manager;
using System;
using System.Collections.ObjectModel;
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

    private ObservableCollection<PageControl> MenuItemsSource
        => PageControl.AllPages.Where(p => p.MenuType is MenuTypes.MainMenu).ToObservableCollection();
    private ObservableCollection<PageControl> FooterMenuItemsSource
        => PageControl.AllPages.Where(p => p.MenuType is MenuTypes.FooterMenu).ToObservableCollection();

    public Page_Main()
    {
        Current = this;

        this.InitializeComponent();

        PageControl.Init();

        compositor = ElementCompositionPreview.GetElementVisual(this).Compositor;
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

    private void GlobalNavigation_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        => NavigateTo((args.SelectedItem as PageControl)?.PageClass, false);

    private void GlobalNavigation_Loaded(object sender, RoutedEventArgs e)
        => (sender as NavigationView).SelectedItem = PageControl.AllPages.First();

#nullable enable
    public void NavigateTo(object page, bool needChangeNaviSelection = true)
    {
        Current.DispatcherQueue.TryEnqueue(() =>
        {
            if (Content_Frame.Content == page)
                return;

            Border_ContentBackground.Visibility = Visibility.Visible;

            if (needChangeNaviSelection)
                GlobalNavigation.SelectedItem = page;

            Content_Frame.Content = page;

            Border_ContentBackground.Opacity = page == PageControl.AllPages.First().PageClass ? 0 : 1;

            IsPaneToggleButtonVisible = true;
        });
    }
}
