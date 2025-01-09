using CommunityToolkit.Mvvm.ComponentModel;

namespace GuiApp.Main.ViewModels;

public partial class ContactsViewModel : ObservableObject
{
    [ObservableProperty]
    private string _title = "Contacts";
}
