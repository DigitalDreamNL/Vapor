using FluentAssertions;
using Xunit;

namespace Vapor.Tests;

public class GridPositionTests
{
    [Fact]
    public void EqualsTests()
    {
        var a = new GridPosition(1, 3, 7);
        var b = new GridPosition(1, 3, 7);
        var c = new GridPosition(2, 4, 8);

        a.Equals(b).Should().BeTrue();
        a.Equals(c).Should().BeFalse();
    }
}