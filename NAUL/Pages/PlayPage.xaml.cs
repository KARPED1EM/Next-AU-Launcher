using Microsoft.UI.Xaml.Controls;
using NAUL.Manager;
using System.Diagnostics;
using System.Threading;

namespace NAUL;

public sealed partial class Page_Play : Page
{
    public static Page_Play Current { get; private set; }

    private string TitleText => VersionManager.SelectedVersion?.EnabledSinglePlugin?.DisplayName ?? "Among Us";
    private string DescriptionText => VersionManager.SelectedVersion?.GetDescriptionText() ?? string.Empty;

    private static Thread UpdateThread { get; set; } 

    public Page_Play()
    {
        Current = this;

        this.InitializeComponent();

        UpdateThread = new(() =>
        {
            while (true)
            {
                Thread.Sleep(1500);
                if (Current == null || !PageControl.GetPageByInstance(Current).Showing) continue;
                UpdateComponents();
            }
        });
        UpdateThread.Start();
    }

    private void Page_Loaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        UpdateComponents();
        Bindings.Update();
    }

    private void JumpToVersionButton_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        => PageControl.NavigateTo(typeof(Page_Version));

    private void Button_StartGame_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        VersionManager.SelectedVersion?.Run();
        UpdateComponents();
    }

    private static void UpdateComponents()
    {
        Current.DispatcherQueue.TryEnqueue(() =>
        {
            if (VersionManager.SelectedVersion != null)
            {
                Current.Button_StartGame.IsEnabled = true;
                if (VersionManager.SelectedVersion.IsRunning())
                {
                    Current.TextBlock_StartGame.Text = "ֹͣ����";
                    Current.FontIcon_StartGame.Glyph = "\uE711";
                }
                else
                {
                    Current.TextBlock_StartGame.Text = "������Ϸ";
                    Current.FontIcon_StartGame.Glyph = "\uE768";
                }
            }
            else
            {
                Current.Button_StartGame.IsEnabled = false;
                Current.TextBlock_StartGame.Text = "δ��װ��Ϸ";
            }
        });
    }
}
