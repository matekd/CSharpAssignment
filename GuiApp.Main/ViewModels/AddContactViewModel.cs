using Business.Interfaces;
using Business.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace GuiApp.Main.ViewModels;

public partial class AddContactViewModel(IServiceProvider serviceProvider, IContactService contactService) : ObservableObject
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    private readonly IContactService _contactService = contactService;

    [ObservableProperty]
    private ContactRegistrationForm _form = new();

    [RelayCommand]
    private void Save()
    {
        var result = _contactService.AddContact(Form);
        
        if (result) GoToMain();
    }

    [RelayCommand]
    private void GoToMain()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ContactsViewModel>();
    }
}
