using Business.Interfaces;
using Business.Models;
using Business.Services;

namespace Business.Tests.Services;

public class FileService_Tests
{
    [Fact]
    public void LoadListFromFile_ShouldReturnContentFromFile()
    {
        // arrange
        string fileName = $"{Guid.NewGuid().ToString()}.json";
        IFileService<ContactEntity> fileService = new FileService<ContactEntity>(fileName);
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

        try
        {
            fileService.SaveListToFile(new List<ContactEntity> { ce });

            // act
            var result = fileService.LoadListFromFile();

            Assert.NotEmpty(result);
            Assert.Equal(ce.FirstName, result[0].FirstName);

        }
        finally {
            if (File.Exists(fileName))
                File.Delete(fileName);
        }
    }

    [Fact]
    public void SaveListToFile_ShouldReturnTrue()
    {
        // arrange
        string fileName = $"{Guid.NewGuid().ToString()}.json";
        IFileService<ContactEntity> fileService = new FileService<ContactEntity>(fileName);
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

        try
        {
            // act
            var result = fileService.SaveListToFile(new List<ContactEntity> { ce });

            // assert
            Assert.True(result);
        }
        finally
        {
            if (File.Exists(fileName))
                File.Delete(fileName);
        }
    }
}
