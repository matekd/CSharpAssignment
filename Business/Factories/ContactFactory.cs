using Business.Helpers;
using Business.Models;

namespace Business.Factories;

public static class ContactFactory
{
    public static ContactRegistrationForm Create()
    {
        return new ContactRegistrationForm();
    }

    public static ContactEntity Create(ContactRegistrationForm form)
    {
        ContactEntity ce = new()
        {
            Id = UniqueIdentifierGenerator.Generate(),
            FirstName = form.FirstName,
            LastName = form.LastName,
            Email = form.Email,
            Phone = form.Phone,
            Address = form.Address,
            Region = form.Region,
            PostalCode = form.PostalCode,
        };
        return ce;
    }

    public static Contact Create(ContactEntity ce)
    {
        Contact c = new()
        {
            Id = ce.Id,
            FirstName = ce.FirstName,
            LastName = ce.LastName,
            Email = ce.Email,
            Phone = ce.Phone,
            Address = ce.Address,
            Region = ce.Region,
            PostalCode = ce.PostalCode,
        };
        return c;
    }
}
