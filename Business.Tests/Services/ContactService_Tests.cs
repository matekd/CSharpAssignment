using Business.Interfaces;
using Business.Models;
using Business.Services;
using Moq;

namespace Business.Tests.Services;

public class ContactService_Tests
{
    private readonly Mock<IContactService> _contactServiceMock;
    private readonly IContactService _contactService;
    private readonly Mock<IFileService<ContactEntity>> _fileServiceMock;

    public ContactService_Tests()
    {
        _contactServiceMock = new Mock<IContactService>();
        _fileServiceMock = new Mock<IFileService<ContactEntity>>();
        _contactService = new ContactService(_fileServiceMock.Object);
    }

    #region AddContact
    [Fact]
    public void AddContact_ShouldReturnTrue_WhenCreatingContact()
    {
        // arrange
        ContactRegistrationForm form = new()
        {
            FirstName = "Test",
            LastName = "Test",
            Email = "Test",
            Phone = "Test",
            Address = "Test",
            Region = "Test",
            PostalCode = "Test",
        };
        _contactServiceMock
            .Setup(cs => cs.AddContact(form))
            .Returns(true);

        // act
        bool result = _contactServiceMock.Object.AddContact(form);

        // assert
        Assert.True(result);
        _contactServiceMock.Verify(cs => cs.AddContact(form), Times.Once);
    }

    [Fact]
    public void AddContact_ShouldReturnFalse_WhenGivenNull()
    {
        // act
        bool result = _contactServiceMock.Object.AddContact(null!);

        // assert
        Assert.False(result);
    }
    #endregion

    #region GetAllContacts
    [Fact]
    public void GetAllContacts_ShouldReturnListOfContacts()
    {
        // arrange
        IEnumerable<Contact> contacts =
        [
            new() { FirstName = "John", 
                LastName = "Doe", 
                Email = "john.doe@domain.com", 
                Phone = "1234567890", 
                Address = "1600 Pennsylvania Avenue NW", 
                Region = "Washington", 
                PostalCode = "DC 20500" },
            new() { FirstName = "Jane",
                LastName = "Doe",
                Email = "jane.doe@domain.com",
                Phone = "1234567890",
                Address = "1600 Pennsylvania Avenue NW",
                Region = "Washington",
                PostalCode = "DC 20500" }
        ];
        
        _contactServiceMock.Setup(cs => cs.GetAllContacts()).Returns(contacts);

        // act
        IEnumerable<Contact> result = _contactServiceMock.Object.GetAllContacts();

        // assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.Equal("John", result.First().FirstName);
    }

    [Fact]
    public void GetAllContacts_ShouldThrowException_WhenListIsEmpty()
    {
        // arrange
        Exception exc = new Exception("No contacts exist");
        _contactServiceMock.Setup(cs => cs.GetAllContacts()).Throws(exc);

        // act
        Exception e = Assert.Throws<Exception>(() => _contactServiceMock.Object.GetAllContacts());

        // assert
        Assert.Equal("No contacts exist", e.Message);
    }

    [Fact]
    public void GetAllContacts_ShouldThrowException_WhenFileIsEmpty()
    {
        // arrange
        _fileServiceMock.Setup(fs => fs.LoadListFromFile()).Returns([]);

        // act
        Exception e = Assert.Throws<Exception>(() => _contactService.GetAllContacts());

        // assert
        Assert.Equal("No contacts exist", e.Message);
    }
    #endregion

    #region DeleteContact
    [Fact]
    public void DeleteContact_ShouldReturnTrue_WhenRemovingContact()
    {
        // arrange
        _contactServiceMock.Setup(cs => cs.DeleteContact(It.IsAny<string>())).Returns(true);

        // act
        bool result = _contactServiceMock.Object.DeleteContact("test_id");

        // assert
        Assert.True(result);
    }

    [Fact]
    public void DeleteContact_ShouldReturnFalse_IfFailedToRemoveContact()
    {
        // arrange
        _contactServiceMock.Setup(cs => cs.DeleteContact(It.IsAny<string>())).Returns(false);

        // act
        bool result = _contactServiceMock.Object.DeleteContact("test_id");

        // assert
        Assert.False(result);

    }

    [Fact]
    public void DeleteContact_ShouldThrowException_IfContactDoesNotExist()
    {
        // arrange
        Exception exc = new Exception("Contact does not exist");
        _contactServiceMock.Setup(cs => cs.DeleteContact(It.IsAny<string>())).Throws(exc);

        // act
        Exception e = Assert.Throws<Exception>(() => _contactServiceMock.Object.DeleteContact(It.IsAny<string>()));

        // assert
        Assert.Equal("Contact does not exist", e.Message);

    }
    #endregion

    #region UpdateContact
    [Fact]
    public void UpdateContact_ShouldReturnTrue_WhenUpdatingContact()
    {
        // arrange
        ContactRegistrationForm form = new()
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@domain.com",
            Phone = "1234567890",
            Address = "1600 Pennsylvania Avenue NW",
            Region = "Washington",
            PostalCode = "DC 20500"
        };
        _contactServiceMock.Setup(cs => cs.UpdateContact(form, It.IsAny<string>())).Returns(true);

        // act
        bool result = _contactServiceMock.Object.UpdateContact(form, It.IsAny<string>());

        // assert
        Assert.True(result);
    }

    [Fact]
    public void UpdateContact_ShouldReturnFalse_IfFailedToUpdate()
    {
        // arrange
        // Assumes valid and existing Id
        _contactServiceMock.Setup(cs => cs.UpdateContact(null!, It.IsAny<string>())).Returns(false);

        // act
        bool result = _contactServiceMock.Object.UpdateContact(null!, It.IsAny<string>());

        // assert
        Assert.False(result);
    }

    [Fact]
    public void UpdateContact_ShouldThrowException_IfIdDoesnNotExist()
    {
        // arrange
        ContactRegistrationForm form = new()
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@domain.com",
            Phone = "1234567890",
            Address = "1600 Pennsylvania Avenue NW",
            Region = "Washington",
            PostalCode = "DC 20500"
        };
        Exception exc = new Exception("Contact does not exist");
        _contactServiceMock.Setup(cs => cs.UpdateContact(form, It.IsAny<string>())).Throws(exc);

        // act
        Exception e = Assert.Throws<Exception>(() => _contactServiceMock.Object.UpdateContact(form, It.IsAny<string>()));

        // assert
        Assert.Equal("Contact does not exist", e.Message);
    }
    #endregion

    #region GetContactById
    [Fact]
    public void GetContactById_ShouldReturnContact()
    {
        // arrange
        string id = "1";
        Contact contact = new()
        {
            Id = id,
            FirstName = "Test",
            LastName = "Test",
            Email = "Test",
            Phone = "Test",
            Address = "Test",
            Region = "Test",
            PostalCode = "Test",
        };
        _contactServiceMock
            .Setup(cs => cs.GetContactByID(id))
            .Returns(contact);

        // act
        var result = _contactServiceMock.Object.GetContactByID(id);

        // assert
        Assert.IsType<Contact>(result);
        Assert.Equal(id, result.Id);
        _contactServiceMock.Verify(cs => cs.GetContactByID(id), Times.Once);
    }

    [Fact]
    public void GetContactById_ShouldThrowException_IfIdDoesNotExist()
    {
        // arrange
        Exception exc = new Exception("Contact does not exist");
        _contactServiceMock.Setup(cs => cs.GetContactByID(It.IsAny<string>())).Throws(exc);

        // act
        Exception e = Assert.Throws<Exception>(() => _contactServiceMock.Object.GetContactByID(It.IsAny<string>()));

        // assert
        Assert.Equal("Contact does not exist", e.Message);
    }
    #endregion
}
