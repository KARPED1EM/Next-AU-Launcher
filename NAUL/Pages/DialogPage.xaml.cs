using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace NAUL;

public sealed partial class Page_Dialog : Page
{
    public static Page_Dialog Current { get; private set; }

    public Page_Dialog()
    {
        Current = this;

        this.InitializeComponent();
    }

    public static ContentDialog Create(string title, string text, ContentDialog? dialog)
    {
        Current ??= new Page_Dialog();
        Current.TextBlock.Text = text;
        Current.TextBlock.Visibility = string.IsNullOrEmpty(text) ? Visibility.Collapsed : Visibility.Visible;

        dialog ??= new();
        dialog.Title ??= title;
        dialog.Style ??= Application.Current.Resources["DefaultContentDialogStyle"] as Style;

        return dialog;
    }
}
