using Business.Factories;
using Business.Helpers;
using Business.Interfaces;
using Business.Models;
using System.Diagnostics;

namespace Business.Services;

public class ContactService : IContactService
{
    private readonly List<ContactEntity> _contacts = [];
    private readonly IFileService<ContactEntity> _fileService;

    public ContactService(IFileService<ContactEntity> fileService)
    {
        _fileService = fileService;
        _contacts = _fileService.LoadListFromFile();
    }

    public bool AddContact(ContactRegistrationForm form)
    {
        try
        {
            ContactEntity ce = ContactFactory.Create(form);
            _contacts.Add(ce);
            _fileService.SaveListToFile(_contacts);

            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return false;
        }
    }

    public bool DeleteContact(string id)
    {
        ContactEntity ce = _contacts.Find(e => e.Id == id)!;

        return _contacts.Remove(ce); ;
    }

    public Contact GetContactById(string id)
    {
        ContactEntity ce = _contacts.Find(e => e.Id == id)!;
        
        if (ce == null)
            throw new Exception("User does not exist");

        Contact c = ContactFactory.Create(ce);

        return c;
    }

    public IEnumerable<Contact> GetContacts()
    {
        List<Contact> list = [];

        foreach (ContactEntity ce in _contacts)
        {
            list.Add(ContactFactory.Create(ce));
        }

        return list;
    }

    public bool UpdateContact(ContactRegistrationForm form, string id)
    {
        ContactEntity ce = _contacts.Find(e => e.Id == id)!;

        if (ce == null)
            throw new Exception("User does not exist");

        ce.FirstName  = form.FirstName  ?? ce.FirstName;
        ce.LastName   = form.LastName   ?? ce.LastName;
        ce.Email      = form.Email      ?? ce.Email;
        ce.Phone      = form.Phone      ?? ce.Phone;
        ce.Address    = form.Address    ?? ce.Address;
        ce.Region     = form.Region     ?? ce.Region;
        ce.PostalCode = form.PostalCode ?? ce.PostalCode;

        return true;
    }
}
