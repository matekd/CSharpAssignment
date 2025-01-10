using Business.Factories;
using Business.Interfaces;
using Business.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace GuiApp.Main.ViewModels;

public partial class UpdateContactViewModel(IServiceProvider serviceProvider, IContactService contactService) : ObservableObject
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    private readonly IContactService _contactService = contactService;

    [ObservableProperty]
    private Contact _contact = new(); 

    [RelayCommand]
    private void GoToMain()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ContactsViewModel>();
    }

    [RelayCommand]
    private void Save()
    {
        ContactRegistrationForm form = ContactFactory.Create(Contact);
        var result = _contactService.UpdateContact(form, Contact.Id);

        if (result) GoToMain();
    }

    [RelayCommand]
    private void Delete()
    {
        var result = _contactService.DeleteContact(Contact.Id);

        if (result) GoToMain();
    }
}
