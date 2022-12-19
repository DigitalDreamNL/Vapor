using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace Vapor.Tests;

public class FaceTests
{
    [Fact]
    public void CreateFrontFace() => CreateFaceTests(FrontFacePositions);

    [Fact]
    public void CreateBottomFace() => CreateFaceTests(BottomFacePositions);

    [Fact]
    public void CreateLeftFace() => CreateFaceTests(LeftFacePositions);

    public void CreateFaceTests(List<GridPosition> positions)
    {
        var face = new Face(
            positions[1],
            positions[2],
            positions[3],
            positions[0]);

        face.A.Should().Be(positions[0]);
        face.B.Should().Be(positions[1]);
        face.C.Should().Be(positions[2]);
        face.D.Should().Be(positions[3]);
    }

    [Fact]
    public void CreateFromPositionFrontTests() => CreateFromPositionTests(Direction.Front, FrontFacePositions);


    [Fact]
    public void CreateFromPositionBottomTests() => CreateFromPositionTests(Direction.Bottom, BottomFacePositions);

    [Fact]
    public void CreateFromPositionLeftTests() => CreateFromPositionTests(Direction.Left, LeftFacePositions);

    public void CreateFromPositionTests(Direction direction, List<GridPosition> positions)
    {
        var position = new GridPosition(0, 0, 0);
        var face = Face.FromPosition(position, direction);

        face.A.Should().Be(positions[0]);
        face.B.Should().Be(positions[1]);
        face.C.Should().Be(positions[2]);
        face.D.Should().Be(positions[3]);
    }

    [Fact]
    public void FlipFacesTests()
    {
        var frontNeighbors = Face.FromPosition(new GridPosition(0, 0, 0), Direction.Front).GetNeighbors();
        frontNeighbors.Count.Should().Be(12);
        
        
        var leftFace = Face.FromPosition(new GridPosition(0, 0, 0), Direction.Left);
        leftFace.GetNeighbors().Count.Should().Be(12);
        
        
        var bottomFace = Face.FromPosition(new GridPosition(0, 0, 0), Direction.Bottom);
        bottomFace.GetNeighbors().Count.Should().Be(12);
    }

    public static readonly List<GridPosition> FrontFacePositions = new()
    {
        new(0, 0, 0),
        new(0, 1, 0),
        new(1, 0, 0),
        new(1, 1, 0)
    };

    public static readonly List<GridPosition> BottomFacePositions = new()
    {
        new (0, 0, 0),
        new (0, 0, 1),
        new (1, 0, 0),
        new (1, 0, 1),
    };

    public static readonly List<GridPosition> LeftFacePositions = new()
    {
        new(0, 0, 0),
        new(0, 0, 1),
        new(0, 1, 0),
        new(0, 1, 1)
    };
}