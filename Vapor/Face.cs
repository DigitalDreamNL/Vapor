namespace Vapor;

public class Face : IEquatable<Face>
{
    public GridPosition A { get; }
    public GridPosition B { get; }
    public GridPosition C { get; }
    public GridPosition D { get; }

    public Face(GridPosition a, GridPosition b, GridPosition c, GridPosition d)
    {
        var sortedCorners = SortCorners(a, b, c, d);

        A = sortedCorners[0];
        B = sortedCorners[1];
        C = sortedCorners[2];
        D = sortedCorners[3];
    }

    private static List<GridPosition> SortCorners(GridPosition a, GridPosition b, GridPosition c, GridPosition d) =>
        new List<GridPosition> {a, b, c, d}.OrderBy(p => 4 * p.X + 2 * p.Y + 1 * p.Z).ToList();

    public List<Face> GetNeighbors()
    {
        var neighbors = new List<Face>();
        var transformations = GetTransformations();
        foreach (var direction in transformations)
        {
            direction.Value.ForEach(shift =>
            {
                neighbors.Add(FromPosition(shift.Shift(A.X, A.Y, A.Z), direction.Key));
            });
        }

        return neighbors;
    }

    private Dictionary<Direction, List<GridPosition>> GetTransformations()
    {
        if (A.Z == B.Z && B.Z == C.Z && C.Z == D.Z)
        {
            return new Dictionary<Direction, List<GridPosition>>
            {
                {Direction.Front, new List<GridPosition>
                {
                    new(-1, 0, 0),
                    new(1, 0, 0),
                    new(0, 1, 0),
                    new(0, -1, 0),
                }},
                {Direction.Bottom, new List<GridPosition>
                {
                    new(0, 0, 0),
                    new(0, 0, -1),
                    new(0, 1, 0),
                    new(0, 1, -1),
                }},
                {Direction.Left, new List<GridPosition>
                {
                    new(0, 0, 0),
                    new(1, 0, 0),
                    new(0, 0, -1),
                    new(1, 0, -1),
                }}
            };
        }

        if (A.Y == B.Y && B.Y == C.Y && C.Y == D.Y)
        {
            return new Dictionary<Direction, List<GridPosition>>
            {
                {Direction.Front, new List<GridPosition>
                {
                    new(0, 0, 0),
                    new(0, -1, 0),
                    new(0, 0, 1),
                    new(0, -1, 1),
                }},
                {Direction.Bottom, new List<GridPosition>
                {
                    new(-1, 0, 0),
                    new(1, 0, 0),
                    new(0, 0, 1),
                    new(0, 0, -1),
                }},
                {Direction.Left, new List<GridPosition>
                {
                    new(0, 0, 0),
                    new(1, 0, 0),
                    new(0, -1, 0),
                    new(1, -1, 0),
                }}
            };
        }

        if (A.X == B.X && B.X == C.X && C.X == D.X)
        {
            return new Dictionary<Direction, List<GridPosition>>
            {
                {Direction.Front, new List<GridPosition>
                {
                    new(0, 0, 0),
                    new(-1, 0, 0),
                    new(0, 0, 1),
                    new(-1, 0, 1),
                }},
                {Direction.Bottom, new List<GridPosition>
                {
                    new(0, 0, 0),
                    new(-1, 0, 0),
                    new(0, 1, 0),
                    new(-1, 1, 0),
                }},
                {Direction.Left, new List<GridPosition>
                {
                    new(0, 0, -1),
                    new(0, 0, 1),
                    new(0, 1, 0),
                    new(0, -1, 0),
                }}
            };
        }

        throw new Exception();

    }

    public static Face FromPosition(GridPosition position, Direction direction)
    {
        var shifts = direction switch
        {
            Direction.Front => new List<GridPosition>
            {
                new (0, 0, 0),
                new (0, 1, 0),
                new (1, 0, 0),
                new (1, 1, 0),
            },
            Direction.Bottom => new List<GridPosition>
            {
                new (0, 0, 0),
                new (0, 0, 1),
                new (1, 0, 0),
                new (1, 0, 1),
            },
            Direction.Left => new List<GridPosition>
            {
                new (0, 0, 0),
                new (0, 0, 1),
                new (0, 1, 0),
                new (0, 1, 1),
            },
            _ => throw new Exception(),
        };
        
        var positions = shifts.Select(s => position.Shift(s.X, s.Y, s.Z)).ToList();
        
        return new Face(positions[0], positions[1], positions[2], positions[3]);
    }

    public bool Equals(Face? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return A.Equals(other.A) && B.Equals(other.B) && C.Equals(other.C) && D.Equals(other.D);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Face) obj);
    }

    public override int GetHashCode() =>
        HashCode.Combine(A, B, C, D);

    public string ToString()
    {
        return $"({A.X},{A.Y},{A.Z}) ({B.X},{B.Y},{B.Z}) ({C.X},{C.Y},{C.Z}) ({D.X},{D.Y},{D.Z})";
    }
}