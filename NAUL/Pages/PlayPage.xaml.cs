using Microsoft.UI.Xaml.Controls;
using NAUL.Services;

namespace NAUL;

public sealed partial class Page_Play : Page
{
    private string titleText => (VersionService.currentVersion?.IsVanilla ?? true)
        ? "Among Us"
        : VersionService.currentVersion?.Name;
    private string descriptionText => (VersionService.currentVersion?.IsVanilla ?? true)
        ? (VersionService.currentVersion?.GameVersion.ToString() + " | " + VersionService.currentVersion?.Platform)
        : (VersionService.currentVersion?.ModVersion.ToString() + " | " + VersionService.currentVersion?.Platform);

    public Page_Play()
    {
        this.InitializeComponent();
    }
}
