using Business.Helpers;

namespace Business.Tests.Helpers;

public class UniqueIdentifierGenerator_Tests
{
    [Fact]
    public void Generate_ShouldReturnGuidAsString()
    {
        // act
        var guid = UniqueIdentifierGenerator.Generate();

        // assert
        Assert.True(Guid.TryParse(guid, out _));
    }
}
