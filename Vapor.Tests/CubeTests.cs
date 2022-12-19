using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace Vapor.Tests;

public class CubeTests
{
    [Fact]
    public void CreateCubeTests()
    {
        var cube = new Cube(1, 3, 7);

        cube.X.Should().Be(1);
        cube.Y.Should().Be(3);
        cube.Z.Should().Be(7);

        cube.Faces.Count.Should().Be(6);

        VerifyFace(cube, FaceTests.FrontFacePositions, new GridPosition(1, 3, 7));
        VerifyFace(cube, FaceTests.LeftFacePositions, new GridPosition(1, 3, 7));
        VerifyFace(cube, FaceTests.BottomFacePositions, new GridPosition(1, 3, 7));
        VerifyFace(cube, FaceTests.FrontFacePositions, new GridPosition(1, 3, 8)); // Z+1 => Back 
        VerifyFace(cube, FaceTests.LeftFacePositions, new GridPosition(2, 3, 7)); // X+1 => Right
        VerifyFace(cube, FaceTests.BottomFacePositions, new GridPosition(1, 4, 7)); // Y+1 => Top
    }

    private void VerifyFace(Cube cube, List<GridPosition> positions, GridPosition shift)
    {
        var shiftedPositions = ShiftPositions(positions, shift.X, shift.Y, shift.Z);
        var face = CreateFaceFromPositions(shiftedPositions);
        cube.Faces.Count(f => face.Equals(f)).Should().Be(1);
    }

    private static Face CreateFaceFromPositions(List<GridPosition> bottomPositions) =>
        new (bottomPositions[0], bottomPositions[1], bottomPositions[2], bottomPositions[3]);

    private List<GridPosition> ShiftPositions(List<GridPosition> positions, int dX, int dY, int dZ) =>
        positions.Select(p => p.Shift(dX, dY, dZ)).ToList();
}