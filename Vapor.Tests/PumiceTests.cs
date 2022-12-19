using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace Vapor.Tests;

public class PumiceTests
{
    [Fact]
    public void CreatePumice()
    {
        var pumice = new Pumice();

        pumice.AddCube(new Cube(0, 0, 0));

        pumice.Cubes.Count.Should().Be(1);

        pumice.GetAllVisibleFaces().Count.Should().Be(6);

        pumice.AddCube(new Cube(1, 0, 0));

        pumice.GetAllVisibleFaces().Count.Should().Be(10);

        pumice.AddCube(new Cube(2, 0, 0));

        pumice.GetAllVisibleFaces().Count.Should().Be(14);

        pumice.AddCube(new Cube(1, 1, 0));

        pumice.GetAllVisibleFaces().Count.Should().Be(18);
    }

    [Fact]
    public void PumiceTestData()
    {
        var pumice = new Pumice();

        var cubePositions = new List<(int X, int Y, int Z)>
        {
            (2, 2, 2),
            (1, 2, 2),
            (3, 2, 2),
            (2, 1, 2),
            (2, 3, 2),
            (2, 2, 1),
            (2, 2, 3),
            (2, 2, 4),
            (2, 2, 6),
            (1, 2, 5),
            (3, 2, 5),
            (2, 1, 5),
            (2, 3, 5),
        };

        cubePositions.ForEach(p => pumice.AddCube(new Cube(p.X, p.Y, p.Z)));

        pumice.GetAllVisibleFaces().Count.Should().Be(64);

        // TODO: This test fails; I don't know why =(
        pumice.GetFacesVisibleFromTheOutside().Count.Should().Be(58);
    }
}