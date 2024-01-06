using System.ComponentModel;
using System.Windows;

namespace BrowserSelector.View;

public partial class UserOptionsWindow : Window
{
    public UserOptionsWindow()
    {
        InitializeComponent();
    }

    private void UserOptionsWindow_OnClosing(object? sender, CancelEventArgs e)
    {
        e.Cancel = true;
        Hide();
    }
}
