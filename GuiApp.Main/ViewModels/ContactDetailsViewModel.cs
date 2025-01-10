using Business.Interfaces;
using Business.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace GuiApp.Main.ViewModels;

public partial class ContactDetailsViewModel(IServiceProvider serviceProvider) : ObservableObject
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    [ObservableProperty]
    private Contact _contact = new(); 

    [RelayCommand]
    private void GoToMain()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ContactsViewModel>();
    }

    [RelayCommand]
    private void GoToUpdateContact()
    {
        var updateContactViewModel = _serviceProvider.GetRequiredService<UpdateContactViewModel>();
        updateContactViewModel.Contact = Contact;

        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = updateContactViewModel;
    }
}
