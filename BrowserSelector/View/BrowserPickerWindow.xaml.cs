using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using BrowserSelector.ViewModel;

namespace BrowserSelector.View;

public partial class BrowserPickerWindow : Window
{
    public BrowserPickerWindow()
    {
        InitializeComponent();
    }

    private void BrowserPickerWindow_OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        if (e.OldValue is BrowserPickerViewModel oldVm)
        {
            oldVm.HandlerSelected -= OnHandlerSelected;
        }

        if (e.NewValue is BrowserPickerViewModel newVm)
        {
            newVm.HandlerSelected += OnHandlerSelected;
        }
    }

    private void OnHandlerSelected(object? sender, EventArgs e)
    {
        DialogResult = true;
    }

    private void BrowserPickerWindow_OnDeactivated(object? sender, EventArgs e)
    {
        DialogResult ??= false;
    }

    private void BrowserPickerWindow_OnPreviewKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Escape)
        {
            DialogResult ??= false;
        }
    }

    private void BrowserPickerWindow_OnActivated(object? sender, EventArgs e)
    {
        // How dumb is this? Why is the window not Active yet since we're in the Activated event was raised?
        // Anyway, this is a workaround for that.
        Activate();
    }
}
