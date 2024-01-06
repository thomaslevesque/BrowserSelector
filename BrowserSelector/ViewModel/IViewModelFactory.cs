namespace BrowserSelector.ViewModel;

public interface IViewModelFactory
{
    BrowserPickerViewModel CreateBrowserPickerViewModel(Uri url);
}
