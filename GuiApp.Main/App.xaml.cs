using Business.Interfaces;
using Business.Models;
using Business.Services;
using GuiApp.Main.ViewModels;
using GuiApp.Main.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;

namespace GuiApp.Main;

public partial class App : Application
{
    private IHost _host;

    public App()
    {
        _host = Host.CreateDefaultBuilder()
            .ConfigureServices(services =>
            {
                services.AddTransient<IContactService, ContactService>();
                services.AddSingleton<IFileService<ContactEntity>>(new FileService<ContactEntity>(@"d:\\vsProject\\data\", "contacts.json"));

                services.AddSingleton<MainViewModel>();
                services.AddSingleton<MainWindow>();

                services.AddTransient<ContactsViewModel>();
                services.AddTransient<ContactsView>();

                services.AddTransient<AddContactViewModel>();
                services.AddTransient<AddContactView>();

                services.AddTransient<ContactDetailsViewModel>();
                services.AddTransient<ContactDetailsView>();

                services.AddTransient<UpdateContactViewModel>();
                services.AddTransient<UpdateContactView>();
            })
            .Build();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        var mainWindow = _host.Services.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }
}
