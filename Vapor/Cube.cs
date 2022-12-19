namespace Vapor;

public class Cube
{
    public int X { get; }
    public int Y { get; }
    public int Z { get; }

    public List<Face> Faces { get; }

    public Cube(int x, int y, int z)
    {
        X = x;
        Y = y;
        Z = z;

        Faces = new List<Face>
        {
            Face.FromPosition(new GridPosition(x, y, z), Direction.Front),
            Face.FromPosition(new GridPosition(x, y, z), Direction.Left),
            Face.FromPosition(new GridPosition(x, y, z), Direction.Bottom),
            Face.FromPosition(new GridPosition(x, y, z + 1), Direction.Front), // Z+1 => Back
            Face.FromPosition(new GridPosition(x + 1, y, z), Direction.Left), // X+1 => Right
            Face.FromPosition(new GridPosition(x, y + 1, z), Direction.Bottom), // Y+1 => Top
        };
    }
}