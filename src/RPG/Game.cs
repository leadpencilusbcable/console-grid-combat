using Json;
using static Mappings;
using static HelperFunctions;

namespace RPG
{
    class Game
    {
        const int GRID_HEIGHT = 8;
        const int GRID_WIDTH = 8;
        const int CELL_HEIGHT = 3;
        const int CELL_WIDTH = 5;
        Grid Grid;
        List<Team> Teams;

        public Game()
        {
            Grid = new Grid(GRID_HEIGHT, GRID_WIDTH, CELL_HEIGHT, CELL_WIDTH);
            Teams = new();
        }

        public async Task Initialise(string filePath)
        {
            List<TeamJson> teamData = await JsonFileReader.ReadAsync<List<TeamJson>>(filePath);
            Teams = new();

            foreach(TeamJson teamJson in teamData){
                List<Character> members = teamJson.members.Select(member => new Character(
                    member.name,
                    member.initalHealth,
                    member.moveSpeed,
                    rand.Next(1, 101)
                )).ToList();

                Dictionary<string, Relationship> teamRelationships = teamJson.relationships.ToDictionary(
                    relationship => relationship.teamName,
                    relationship => Enum.Parse<Relationship>(relationship.relationship)
                );

                Team team = new(
                    teamJson.name,
                    teamJson.isPlayer,
                    teamRelationships,
                    members.Select(member =>
                        new Tuple<Character, Vector2i>(
                            member,
                            GridHelper.RandomiseCellItemLocation(
                                ref Grid,
                                new CellItem(member.Name)
                            )
                        )
                    ).ToList()
                );

                Teams.Add(team);
            }
        }

        public void Start()
        {
            while(true)
            {
                List<Tuple<int, int>> indexesByInitiative = OrderByInitiative();

                foreach(Tuple<int, int> indexes in indexesByInitiative){
                    Character currenctCharacter = Teams[indexes.Item1].Characters[indexes.Item2].Item1;
                    Vector2i currentPos = Teams[indexes.Item1].Characters[indexes.Item2].Item2;

                    int moves = 0;

                    List<Direction> validDirections = GridHelper.GetValidDirections(Grid, currentPos);
                    List<ConsoleKey> validKeys = GridHelper.GetValidKeys(validDirections);

                    while(moves < currenctCharacter.MoveSpeed && !Console.KeyAvailable){
                        Console.Clear();
                        Console.WriteLine(ConcatStringsByLine(Grid.ToString(), $"\n    {currenctCharacter.Name}'s turn!"));

                        ConsoleKey key = new();

                        while(!validKeys.Contains(key)){
                            key = Console.ReadKey(true).Key;
                        }

                        currentPos = GridHelper.MoveCellItemInDirection(ref Grid, currentPos, DirectionKeyMapping[key]);

                        moves++;

                        validDirections = GridHelper.GetValidDirections(Grid, currentPos);
                        validKeys = GridHelper.GetValidKeys(validDirections);
                    }

                    Teams[indexes.Item1].Characters[indexes.Item2] = new Tuple<Character, Vector2i>(currenctCharacter, currentPos);
                }
            }
        }

        private List<Tuple<int, int>> OrderByInitiative()
        {
            List<Tuple<int, int>> teamCharacterIndexes = new();

            for(int i = 0; i < Teams.Count; i++){
                for(int i2 = 0; i2 < Teams[i].Characters.Count; i2++){
                    if(Teams[i].Characters[i2].Item1.Status == Status.Healthy)
                        teamCharacterIndexes.Add(new Tuple<int, int>(i, i2));
                }
            }

            return teamCharacterIndexes.OrderByDescending(
                teamCharacterIndex => Teams[teamCharacterIndex.Item1].Characters[teamCharacterIndex.Item2].Item1.Initiative
            ).ToList();
        }
    }
}