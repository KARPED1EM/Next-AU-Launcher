using Microsoft.UI.Xaml.Controls;
using NAUL.Manager;
using System.Diagnostics;

namespace NAUL;

public sealed partial class Page_Play : Page
{
    private string TitleText => VersionManager.SelectedVersion?.EnabledSinglePlugin?.DisplayName ?? "Among Us";
    private string DescriptionText => VersionManager.SelectedVersion?.GetDescriptionText() ?? string.Empty;

    public Page_Play()
    {
        this.InitializeComponent();
    }

    private void Page_Loaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        if (VersionManager.SelectedVersion != null)
        {
            Button_StartGame.IsEnabled = true;
            if (VersionManager.SelectedVersion.IsRunning())
            {
                TextBlock_StartGame.Text = "停止运行";
                FontIcon_StartGame.Glyph = "\uE711";
            }
            else
            {
                TextBlock_StartGame.Text = "启动游戏";
                FontIcon_StartGame.Glyph = "\uE768";
            }
        }
        else
        {
            Button_StartGame.IsEnabled = false;
            TextBlock_StartGame.Text = "未安装游戏";
        }
        
    }

    private void JumpToVersionButton_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        => PageControl.NavigateTo(typeof(Page_Version));
}
