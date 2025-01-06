using Business.Models;

namespace Business.Interfaces;

public interface IContactService
{
    public bool AddContact(ContactRegistrationForm form);
    public bool UpdateContact(ContactRegistrationForm form, string id);
    public bool DeleteContact(string id);
    public IEnumerable<Contact> GetAllContacts();
    public Contact GetContactByID(string id);
}
