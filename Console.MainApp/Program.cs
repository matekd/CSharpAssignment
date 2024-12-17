using Business.Interfaces;
using Business.Models;
using Business.Services;
using System.Net;
using System.Numerics;

IContactService cs = new ContactService( new FileService<ContactEntity>(@"d:\vsProject\data", "contacts.json") );

var form = new ContactRegistrationForm
{
    FirstName = "Bob",
    LastName = "Kerman",
    Email = "blank",
    Phone = "phone",
    Address = "address",
    Region = "region",
    PostalCode = "postalcode"
};

cs.AddContact(form);

Contact c = cs.GetContacts().First();
Console.WriteLine($"Name: {c.FirstName} {c.LastName}");
string id = c.Id;

var form2 = new ContactRegistrationForm
{
    FirstName = "Jeb",
};

cs.UpdateContact(form2, id);

Contact c2 = cs.GetContactById(id);
Console.WriteLine($"Name: {c2.FirstName} {c2.LastName}");

cs.DeleteContact(id);

try
{
    Contact c3 = cs.GetContactById(id);
    Console.WriteLine($"Name: {c3.FirstName} {c3.LastName}");
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

Console.WriteLine("\n");

List<ContactRegistrationForm> list = new List<ContactRegistrationForm> {
    new ContactRegistrationForm
        {
            FirstName = "Bob",
            LastName = "Kerman",
            Email = "blank",
            Phone = "phone",
            Address = "address",
            Region = "region",
            PostalCode = "postalcode"
        },
    new ContactRegistrationForm
        {
            FirstName = "Jeb",
            LastName = "Kerman",
            Email = "blank",
            Phone = "phone",
            Address = "address",
            Region = "region",
            PostalCode = "postalcode"
        },
    new ContactRegistrationForm
        {
            FirstName = "Bill",
            LastName = "Kerman",
            Email = "blank",
            Phone = "phone",
            Address = "address",
            Region = "region",
            PostalCode = "postalcode"
        },
    };
foreach(var item in list)
{
    cs.AddContact(item);
}
var getList = cs.GetContacts();
foreach (var item in getList)
{
    Console.WriteLine(item.FirstName);
}
Console.WriteLine(getList.Count());