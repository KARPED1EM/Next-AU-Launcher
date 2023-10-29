using Microsoft.UI.Xaml.Controls;
using NAUL.Manager;

namespace NAUL;

public sealed partial class Page_Play : Page
{
    private string titleText => "Among Us";
    private string descriptionText => $"{VersionManager.SelectedVersion.GameVersion} | {VersionManager.SelectedVersion.GamePlatform}";

    public Page_Play()
    {
        this.InitializeComponent();
    }

    private void JumpToVersionButton_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        => Page_Main.Current.NavigateTo(typeof(Page_Version));
}
