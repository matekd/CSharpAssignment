using Business.Interfaces;
using Business.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;

namespace GuiApp.Main.ViewModels;

public partial class ContactsViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IContactService _contactService;

    [ObservableProperty]
    private ObservableCollection<Contact> _contacts = [];

    [RelayCommand]
    private void GoToAddContact()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<AddContactViewModel>();
    }

    [RelayCommand]
    private void GoToContactDetails(Contact contact)
    {
        var contactDetailViewModel = _serviceProvider.GetRequiredService<ContactDetailsViewModel>();
        contactDetailViewModel.Contact = contact;

        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = contactDetailViewModel;
    }
    public ContactsViewModel(IServiceProvider serviceProvider, IContactService contactService)
    {
        _serviceProvider = serviceProvider;
        _contactService = contactService;
        _contacts = new ObservableCollection<Contact>(_contactService.GetAllContacts());
    }
}
