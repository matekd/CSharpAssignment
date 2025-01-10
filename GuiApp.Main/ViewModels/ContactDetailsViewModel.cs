using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace GuiApp.Main.ViewModels;

public partial class ContactDetailsViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;

    public ContactDetailsViewModel(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    [ObservableProperty]
    private string _title = "Contact Details";

    [RelayCommand]
    private void GoToMain()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ContactsViewModel>();
    }

    [RelayCommand]
    private void GoToUpdateContact()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<AddContactViewModel>();
    }
}
