using RPG;
using Json;
using static HelperFunctions;
using System.Numerics;

static class GridHelper
{
    private static readonly Dictionary<Direction, Vector2i> DirectionVecs = new(){
        { Direction.N, new Vector2i(-1, 0) },
        { Direction.NE, new Vector2i(-1, 1) },
        { Direction.E, new Vector2i(0, 1) },
        { Direction.SE, new Vector2i(1, 1) },
        { Direction.S, new Vector2i(1, 0) },
        { Direction.SW, new Vector2i(1, -1) },
        { Direction.W, new Vector2i(0, -1) },
        { Direction.NW, new Vector2i(-1, -1) }
    };

    public async static Task<List<Team>> InitialiseTeams(string filePath)
    {
        List<TeamJson> teamData = await JsonFileReader.ReadAsync<List<TeamJson>>(filePath);
        List<Team> teams = new();

        foreach(TeamJson teamJson in teamData){
            List<Character> members = teamJson.members.Select(member => new Character(
                member.name,
                member.name,
                member.initalHealth,
                member.moveSpeed,
                teamJson.name,
                rand.Next(1, 101)
            )).ToList();

            Dictionary<string, Relationship> teamRelationships = teamJson.relationships.ToDictionary(
                relationship => relationship.teamName,
                relationship => Enum.Parse<Relationship>(relationship.relationship)
            );

            Team team = new(
                teamJson.name,
                teamJson.isPlayer,
                members,
                teamRelationships
            );

            teams.Add(team);
        }

        return teams;
    }

    public static List<Vector2i> RandomiseCharacterLocations(ref Grid grid, List<Team> teams)
    {
        List<Vector2i> characterLocs = new();

        foreach(Team team in teams){
            foreach(Character member in team.Members){
                List<Vector2i> vacantLocs = grid.GetVacantCells();
                Vector2i vacantLoc = vacantLocs[rand.Next(0, vacantLocs.Count - 1)];

                characterLocs.Add(vacantLoc);
                grid.AddCellItem(member, vacantLoc);
            }
        }

        Grid grid1 = grid;

        return characterLocs.OrderBy(characterLoc => ((Character) grid1.GetCellItem(characterLoc)).Initiative).ToList();
    }

    public static List<Direction> GetValidDirections(Grid grid, Vector2i pos)
    {
        return DirectionVecs.Where(direction => {
            Vector2i newPos = pos + direction.Value;

            return newPos.X <= grid.Height && newPos.Y <= grid.Width && newPos.X > 0 && newPos.Y > 0 && grid.GetCellItem(newPos) == null;
        }).Select(direction => direction.Key).ToList();
    }

    public static Vector2i MoveCellItemInDirection(ref Grid grid, Vector2i pos, Direction direction)
    {
        Vector2i newPos = pos + DirectionVecs[direction];
        grid.MoveCellItem(pos, newPos);

        return newPos;
    }
}