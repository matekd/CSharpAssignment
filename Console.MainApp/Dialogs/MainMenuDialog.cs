using Business.Factories;
using Business.Interfaces;
using Business.Models;

namespace ConsoleApp.MainApp.Dialogs;

public class MainMenuDialog : MenuDialog, IMainMenuDialog
{
    private readonly IContactService _contactService;

    public MainMenuDialog(IContactService contactService)
    {
        _contactService = contactService;
    }

    public void Run()
    {
        while (true)
            MainMenu();
    }

    public void MainMenu()
    {
        Console.Clear();
        Console.WriteLine($"####### Main Menu #######\n");
        Console.WriteLine($"{"1.",-3} Create contact");
        Console.WriteLine($"{"2.",-3} View all contacts");
        Console.WriteLine($"{"3.",-3} Edit contact");
        Console.WriteLine($"{"4.",-3} Delete contact");
        Console.WriteLine($"{"q.",-3} Quit");
        Console.Write("\nChoose your menu option: ");

        var option = Console.ReadLine()!;
        
        if ( option == null )
        {
            InvalidOption();
            return;
        }

        switch (option.ToLower())
        {
            case "q":
                QuitOption();
                break;

            case "1":
                CreateOption();
                break;

            case "2":
                ViewOption();
                break;

            case "3":
                EditOption();
                break;

            case "4":
                DeleteOption();
                break;

            default:
                InvalidOption();
                break;
        }
    }

    protected override void QuitOption()
    {
        Console.Clear();
        Environment.Exit(0);
    }

    private void CreateOption()
    {
        ContactRegistrationForm form = ContactFactory.Create();

        Console.Clear();

        Console.Write("Enter FirstName: ");
        form.FirstName = Console.ReadLine()!;
        Console.Write("Enter LastName: ");
        form.LastName = Console.ReadLine()!;
        Console.Write("Enter Email: ");
        form.Email = Console.ReadLine()!;
        Console.Write("Enter Phone: ");
        form.Phone = Console.ReadLine()!;
        Console.Write("Enter Address: ");
        form.Address = Console.ReadLine()!;
        Console.Write("Enter Region: ");
        form.Region = Console.ReadLine()!;
        Console.Write("Enter PostalCode: ");
        form.PostalCode = Console.ReadLine()!;

        bool result = _contactService.AddContact(form);

        if (result)
            OutputDialog("Contact was successfully created.");
        else
            OutputDialog("Contact was not created successfully.");
    }
    private void ViewOption()
    {
        IEnumerable<Contact> list = _contactService.GetAllContacts();

        Console.Clear();
        Console.WriteLine("####### Contacts #######\n");

        foreach (var c in list)
        {
            Console.WriteLine($"- {c}");
        }

        Console.ReadKey();
    }
    private void DeleteOption()
    {
        IEnumerable<Contact> list = _contactService.GetAllContacts();

        int i;
        while (true)
        {
            Console.Clear();
            Console.WriteLine("####### Contacts #######\n");

            i = 1;
            foreach (var c in list)
            {
                Console.WriteLine($"[{i++}] {c}");
            }

            Console.Write("\nType the number of the contact to delete, or 'q' to exit: ");
            var option = Console.ReadLine();

            if (option == null)
            {
                InvalidOption();
                continue;
            }

            if (option.ToLower().Equals("q")) return;

            if (!int.TryParse(option, out int num))
            {
                InvalidOption();
                continue;
            }
            
            if (num < 0 || num > list.Count())
            {
                InvalidOption();
                continue;
            }
            bool result = _contactService.DeleteContact( list.ElementAt(num - 1).Id );
            if (result)
                OutputDialog("Contact was successfully deleted.");
            else 
                OutputDialog("Contact was not deleted successfully.");
            break;
        }
    }

    private void EditOption()
    {
        IEnumerable<Contact> list = _contactService.GetAllContacts();

        int i;
        while (true)
        {
            Console.Clear();
            Console.WriteLine("####### Contacts #######\n");

            i = 1;
            foreach (var c in list)
            {
                Console.WriteLine($"[{i++}] {c}");
            }

            Console.Write("\nType the number of the contact to update, or 'q' to exit: ");
            var option = Console.ReadLine();

            if (option == null)
            {
                InvalidOption();
                continue;
            }

            if (option.ToLower().Equals("q")) return;

            if (!int.TryParse(option, out int num))
            {
                InvalidOption();
                continue;
            }

            if (num < 0 || num > list.Count())
            {
                InvalidOption();
                continue;
            }

            ContactRegistrationForm form = ContactFactory.Create();

            Console.Clear();
            Console.WriteLine($"{list.ElementAt(num - 1).ToString()}");

            Console.Write("Enter FirstName (Or Leave blank to skip): ");
            form.FirstName = Console.ReadLine()!;
            Console.Write("Enter LastName (Or Leave blank to skip): ");
            form.LastName = Console.ReadLine()!;
            Console.Write("Enter Email (Or Leave blank to skip): ");
            form.Email = Console.ReadLine()!;
            Console.Write("Enter Phone (Or Leave blank to skip): ");
            form.Phone = Console.ReadLine()!;
            Console.Write("Enter Address (Or Leave blank to skip): ");
            form.Address = Console.ReadLine()!;
            Console.Write("Enter Region (Or Leave blank to skip): ");
            form.Region = Console.ReadLine()!;
            Console.Write("Enter PostalCode (Or Leave blank to skip): ");
            form.PostalCode = Console.ReadLine()!;

            bool result = _contactService.UpdateContact(form, list.ElementAt(num - 1).Id);
            if (result)
                OutputDialog("Contact was successfully updated.");
            else
                OutputDialog("Contact was not updated successfully.");
            break;
        }
    }
}
