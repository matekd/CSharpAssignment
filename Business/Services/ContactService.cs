using Business.Factories;
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

        if (!_contacts.Remove(ce)) return false;

        return _fileService.SaveListToFile(_contacts);
    }

    public Contact GetContactById(string id)
    {
        ContactEntity ce = _contacts.Find(e => e.Id == id)!;
        
        if (ce == null)
            throw new Exception("Contact does not exist");

        Contact c = ContactFactory.Create(ce);

        return c;
    }

    public IEnumerable<Contact> GetAllContacts()
    {
        if (_contacts == null)
            throw new Exception("No contacts exist");

        return _contacts.Select(ContactFactory.Create);
    }

    public bool UpdateContact(ContactRegistrationForm form, string id)
    {
        ContactEntity ce = _contacts.Find(e => e.Id == id)!;

        if (ce == null)
            throw new Exception("Contact does not exist");

        ce.FirstName  = string.IsNullOrEmpty(form.FirstName)  ? ce.FirstName  : form.FirstName;
        ce.LastName   = string.IsNullOrEmpty(form.LastName)   ? ce.LastName   : form.LastName;
        ce.Email      = string.IsNullOrEmpty(form.Email)      ? ce.Email      : form.Email;
        ce.Phone      = string.IsNullOrEmpty(form.Phone)      ? ce.Phone      : form.Phone;
        ce.Address    = string.IsNullOrEmpty(form.Address)    ? ce.Address    : form.Address;
        ce.Region     = string.IsNullOrEmpty(form.Region)     ? ce.Region     : form.Region;
        ce.PostalCode = string.IsNullOrEmpty(form.PostalCode) ? ce.PostalCode : form.PostalCode;
                                                                         
        return _fileService.SaveListToFile(_contacts);
    }
}
