using Business.Factories;
using Business.Models;

namespace Business.Tests.Factories;

public class ContactFactory_Tests
{
    [Fact]
    public void Create_ShouldReturnContactRegistrationForm()
    {
        // act
        var form = ContactFactory.Create();

        // assert
        Assert.NotNull(form);
        Assert.IsType<ContactRegistrationForm>(form);
    }

    [Fact]
    public void Create_ShouldReturnContactRegistrationForm_WhenContactIsGiven()
    {
        // arrange
        Contact con = new()
        {
            Id = "123",
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@domain.com",
            Phone = "1234567890",
            Address = "Fakestreet 1",
            Region = "",
            PostalCode = ""
        };

        // act
        ContactRegistrationForm form = ContactFactory.Create(con);

        // assert
        Assert.NotNull(form);
        Assert.IsType<ContactRegistrationForm>(form);
        Assert.Equal(con.FirstName, form.FirstName);
    }

    [Fact]
    public void Create_ShouldReturnContactEntity_WhenContactRegistrationFormIsGiven()
    {
        // arrange
        ContactRegistrationForm form = new()
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@domain.com",
            Phone = "1234567890",
            Address = "Fakestreet 1",
            Region = "",
            PostalCode = ""
        };

        // act
        ContactEntity ce = ContactFactory.Create(form);

        // assert
        Assert.NotNull(ce);
        Assert.IsType<ContactEntity>(ce);
        Assert.Equal(form.FirstName, ce.FirstName);
    }

    [Fact]
    public void Create_ShouldReturnContact_WhenContactEntityIsGiven()
    {
        // arrange
        ContactEntity ce = new()
        {
            Id = "123",
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@domain.com",
            Phone = "1234567890",
            Address = "Fakestreet 1",
            Region = "",
            PostalCode = ""
        };

        // act
        Contact con = ContactFactory.Create(ce);

        // assert
        Assert.NotNull(con);
        Assert.IsType<Contact>(con);
        Assert.Equal(ce.FirstName, con.FirstName);
    }
}
