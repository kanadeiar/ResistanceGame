using FluentAssertions;

namespace ResistanceGame.Tests.Integration;

public class DeveloperTests
{
    [Fact]
    public void TestDev()
    {
        true.Should().BeTrue();
    }
}