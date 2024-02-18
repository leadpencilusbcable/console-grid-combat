using RPG;
using Json;

static class GridHelper
{
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

    public static void RandomiseCharacterLocations(ref Grid grid, List<Team> teams)
    {
        Random rand = new();

        foreach(Team team in teams){
            foreach(Character member in team.Members){
                List<Vector2i> pos = grid.GetVacantCells();
                grid.AddCellItem(member, pos[rand.Next(0, pos.Count - 1)]);
            }
        }
    }
}