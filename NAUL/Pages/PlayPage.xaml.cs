using Microsoft.UI.Xaml.Controls;
using NAUL.Manager;

namespace NAUL;

public sealed partial class Page_Play : Page
{
    private string titleText => (VersionManager.currentVersion?.IsVanilla ?? true)
        ? "Among Us"
        : VersionManager.currentVersion?.Name;
    private string descriptionText => (VersionManager.currentVersion?.IsVanilla ?? true)
        ? (VersionManager.currentVersion?.GameVersion.ToString() + " | " + VersionManager.currentVersion?.Platform)
        : (VersionManager.currentVersion?.ModVersion.ToString() + " | " + VersionManager.currentVersion?.Platform);

    public Page_Play()
    {
        this.InitializeComponent();
    }

    private void JumpToVersionButton_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        => Page_Main.Current.NavigateTo(typeof(Page_Version));
}
