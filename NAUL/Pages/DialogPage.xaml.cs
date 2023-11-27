using System;
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

    public static ContentDialogResult Show(string title, string text, ContentDialog dialog)
    {
        ContentDialogResult result = ContentDialogResult.None;
        Current.DispatcherQueue.TryEnqueue(() =>
        {
            Current.TextBlock.Text = text;
            dialog.Title = title;
            dialog.XamlRoot = Current.XamlRoot;
            dialog.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            result = dialog.ShowAsync().GetAwaiter().GetResult();
        });
        return result;
    }
}
