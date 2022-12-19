namespace Vapor;

public class Pumice
{
    public List<Cube> Cubes { get; } = new();

    public List<Face> Faces() =>
        Cubes.SelectMany(c => c.Faces).ToList();

    public List<Face> GetAllVisibleFaces() =>
        Cubes.SelectMany(c => c.Faces).GroupBy(f => f.GetHashCode()).Where(g => g.Count() == 1).SelectMany(f => f).ToList();

    public List<Face> GetFacesVisibleFromTheOutside()
    {
        // TODO: Find good start face; for now assume that first face is on the outside
        var allVisibleFaces = GetAllVisibleFaces();
        var startFace = allVisibleFaces.First();

        var outsideVisibleFaces = new List<Face>();
        var facesToCheck = new List<Face> { startFace };

        while (facesToCheck.Count > 0)
        {
            var face = facesToCheck.First();

            face.GetNeighbors().ForEach(neighborFace =>
            {
                // Face is already in list of faces that are visible from the outside
                if (outsideVisibleFaces.Any(f => f.Equals(neighborFace))) return;

                // Face is not visible
                if (!allVisibleFaces.Any(f => f.Equals(neighborFace))) return;

                // Face is already in list of faces to check
                if (facesToCheck.Any(f => f.Equals(neighborFace))) return;

                facesToCheck.Add(neighborFace);
            });

            outsideVisibleFaces.Add(face);
            facesToCheck.Remove(face);
        }

        return outsideVisibleFaces;
    }

    public void AddCube(Cube cube)
    {
        Cubes.Add(cube);
    }
}