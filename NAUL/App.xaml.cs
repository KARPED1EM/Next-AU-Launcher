using Microsoft.UI.Xaml;
using System.Globalization;

namespace NAUL;

public partial class App : Application
{
    public App()
    {
        this.InitializeComponent();
#if DEBUG
        CultureInfo.CurrentUICulture = new CultureInfo("zh-CN");
#endif
    }

    private Window m_window;
    protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
    {
        m_window = new MainWindow();
        m_window.Activate();
    }
}
