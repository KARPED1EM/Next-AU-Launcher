using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using NAUL.Manager;
using System;
using System.IO;
using Vanara.PInvoke;
using Windows.Graphics;
using Windows.UI;
using WinRT.Interop;

namespace NAUL;

public sealed partial class MainWindow : Window
{
    public static new MainWindow Current { get; private set; }
    public IntPtr hWnd { get; private set; }
    public double UIScale => User32.GetDpiForWindow(hWnd) / 96d;

    public MainWindow()
    {
        Current = this;
        this.InitializeComponent();

        DataPaths.CreateAllFolders();
        VersionManager.Init();
        PluginManager.Init();

        InitializeMainWindow();
    }

    private void InitializeMainWindow()
    {
        hWnd = WindowNative.GetWindowHandle(this);
        var titleBar = AppWindow.TitleBar;
        titleBar.IconShowOptions = IconShowOptions.HideIconAndSystemMenu;
        var len = (int)(48 * UIScale);
        titleBar.ExtendsContentIntoTitleBar = true;
        SetDragRectangles(new RectInt32(0, 0, 100000, len));
        ChangeTitleBarButtonColor();

        Title = "Next Among Us Launcher";
        ResizeToCertainSize();
        AppWindow.SetIcon(Path.Combine(AppContext.BaseDirectory, @"Assets\LOGO.ico"));

        MainWindow_Frame.Content = new Page_Main();
    }

    public void SetDragRectangles(params RectInt32[] value)
    {
        AppWindow.TitleBar.SetDragRectangles(value);
    }

    public void ResizeToCertainSize(int width = 0, int height = 0)
    {
        var display = DisplayArea.GetFromWindowId(AppWindow.Id, DisplayAreaFallback.Primary);
        var scale = UIScale;
        if (width * height == 0)
        {
            width = (int)(1200 * scale);
            height = (int)(700 * scale);
        }
        else
        {
            width = (int)(width * scale);
            height = (int)(height * scale);
        }
        var x = (display.WorkArea.Width - width) / 2;
        var y = (display.WorkArea.Height - height) / 2;
        AppWindow.MoveAndResize(new RectInt32(x, y, width, height));
        if (AppWindow.Presenter is OverlappedPresenter presenter)
        {
            presenter.IsMaximizable = false;
            presenter.IsResizable = false;
        }
    }

    private void ChangeTitleBarButtonColor()
    {
        if (AppWindowTitleBar.IsCustomizationSupported())
        {
            var titleBar = AppWindow.TitleBar;
            titleBar.ButtonBackgroundColor = Colors.Transparent;
            titleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
            switch (RootGrid.ActualTheme)
            {
                case ElementTheme.Default:
                    break;
                case ElementTheme.Light:
                    titleBar.ButtonForegroundColor = Colors.Black;
                    titleBar.ButtonHoverForegroundColor = Colors.Black;
                    titleBar.ButtonHoverBackgroundColor = Color.FromArgb(0x20, 0x00, 0x00, 0x00);
                    break;
                case ElementTheme.Dark:
                    titleBar.ButtonForegroundColor = Colors.White;
                    titleBar.ButtonHoverForegroundColor = Colors.White;
                    titleBar.ButtonHoverBackgroundColor = Color.FromArgb(0x20, 0xFF, 0xFF, 0xFF);
                    titleBar.ButtonInactiveForegroundColor = Color.FromArgb(0xFF, 0x99, 0x99, 0x99);
                    break;
                default:
                    break;
            }
        }
    }
}
