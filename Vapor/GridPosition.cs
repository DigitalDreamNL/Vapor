namespace Vapor;

public struct GridPosition : IEquatable<GridPosition>
{
    public int X { get; }
    public int Y { get; }
    public int Z { get; }

    public GridPosition(int x, int y, int z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public GridPosition Shift(int dX, int dY, int dZ) =>
        new (X + dX, Y + dY, Z + dZ);

    public bool Equals(GridPosition other) =>
        X == other.X && Y == other.Y && Z == other.Z;

    public override bool Equals(object? obj) =>
        obj is GridPosition other && Equals(other);

    public override int GetHashCode() =>
        HashCode.Combine(X, Y, Z);
}