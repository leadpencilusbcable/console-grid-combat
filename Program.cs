using RPG;
using Json;

const int GRID_HEIGHT = 8;
const int GRID_WIDTH = 8;
const int CELL_HEIGHT = 3;
const int CELL_WIDTH = 5;

List<Team> initialiseTeams(List<TeamJson> teamData)
{
    List<Team> teams = new();

    foreach(TeamJson teamJson in teamData){
        List<Character> members = teamJson.members.Select(member => new Character(
            member.name,
            member.name,
            member.initalHealth,
            member.moveSpeed,
            teamJson.name
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

void randomiseCharacterLocations(ref Grid grid, List<Team> teams)
{
    Random rand = new();

    foreach(Team team in teams){
        foreach(Character member in team.Members){
            List<Vector2i> pos = grid.GetVacantCells();
            grid.AddCellItem(member, pos[rand.Next(0, pos.Count - 1)]);
        }
    }
}

Grid grid = new(GRID_HEIGHT, GRID_WIDTH, CELL_HEIGHT, CELL_WIDTH);

List<Team> teams = initialiseTeams(await JsonFileReader.ReadAsync<List<TeamJson>>("./res/teams.json"));
randomiseCharacterLocations(ref grid, teams);

Console.WriteLine(grid.ToString());