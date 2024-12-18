using Business.Interfaces;
using Business.Models;
using Business.Services;
using ConsoleApp.MainApp.Dialogs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder()
    .ConfigureServices(services =>
    {
        services.AddTransient<IContactService, ContactService>();
        services.AddSingleton<IFileService<ContactEntity>>(new FileService<ContactEntity>(@"d:\\vsProject\\data\", "contacts.json"));

        services.AddTransient<IMainMenuDialog, MainMenuDialog>();
    })
    .Build();

using (var scope = host.Services.CreateScope()) {
    var mainMenuDialog = scope.ServiceProvider.GetService<IMainMenuDialog>()!;
    mainMenuDialog.Run();
}
