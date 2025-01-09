using CommunityToolkit.Mvvm.ComponentModel;

namespace GUIApp.MainApp.ViewModels;

public partial class ContactsViewModel : ObservableObject
{
    [ObservableProperty]
    private string _title = "Contacts";

}
