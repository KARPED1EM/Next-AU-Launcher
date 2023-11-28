using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace NAUL;

public sealed partial class Page_Dialog : Page
{
    public Page_Dialog(string text)
    {
        this.InitializeComponent();

        TextBlock.Text = text;
        TextBlock.Visibility = string.IsNullOrEmpty(text) ? Visibility.Collapsed : Visibility.Visible;
    }

    public static ContentDialog Create(string title, string text, ContentDialog dialog = null)
    {
        dialog ??= new();
        dialog.Title ??= title;
        dialog.XamlRoot ??= Page_Main.Current.XamlRoot;
        dialog.Style ??= Application.Current.Resources["DefaultContentDialogStyle"] as Style;
        dialog.Content ??= new Page_Dialog(text);

        if (string.IsNullOrEmpty(dialog.PrimaryButtonText) && string.IsNullOrEmpty(dialog.SecondaryButtonText) && string.IsNullOrEmpty(dialog.CloseButtonText))
            dialog.CloseButtonText = "ÖªµÀÁË";

        return dialog;
    }
}
