using Microsoft.UI.Xaml.Controls;
using NAUL.Manager;
using System.Diagnostics;

namespace NAUL;

public sealed partial class Page_Play : Page
{
    private string TitleText => "Among Us";
    private string DescriptionText => VersionManager.SelectedVersion?.GetDescriptionText() ?? string.Empty;

    public Page_Play()
    {
        this.InitializeComponent();
    }

    private void Page_Loaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        Button_StartGame.IsEnabled = VersionManager.SelectedVersion != null;
        Debug.WriteLine(Button_StartGame.IsEnabled);
    }

    private void JumpToVersionButton_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        => PageControl.NavigateTo(typeof(Page_Version));
}
