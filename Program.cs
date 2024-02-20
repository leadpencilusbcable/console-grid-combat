using RPG;
using static HelperFunctions;

const int GRID_HEIGHT = 8;
const int GRID_WIDTH = 8;
const int CELL_HEIGHT = 3;
const int CELL_WIDTH = 5;

Dictionary<ConsoleKey, Direction> directionKeys = new(){
    { ConsoleKey.D8, Direction.N },
    { ConsoleKey.D9, Direction.NE },
    { ConsoleKey.D6, Direction.E },
    { ConsoleKey.D3, Direction.SE },
    { ConsoleKey.D2, Direction.S },
    { ConsoleKey.D1, Direction.SW },
    { ConsoleKey.D4, Direction.W },
    { ConsoleKey.D7, Direction.NW }
};

Grid grid = new(GRID_HEIGHT, GRID_WIDTH, CELL_HEIGHT, CELL_WIDTH);

List<Team> teams = await GridHelper.InitialiseTeams("./res/teams.json");
List<Vector2i> cellsByInitiative = GridHelper.RandomiseCharacterLocations(ref grid, teams);

while(true)
{
    foreach(Vector2i pos in cellsByInitiative){
        Character character = (Character) grid.GetCellItem(pos);
        Vector2i currentPos = pos;
        int moves = 0;

        List<Direction> validDirections = GridHelper.GetValidDirections(grid, pos);
        List<ConsoleKey> validKeys = directionKeys.Where(key => validDirections.Contains(key.Value)).Select(key => key.Key).ToList();

        while(moves < character.MoveSpeed && !(Console.KeyAvailable && validKeys.Contains(Console.ReadKey(true).Key))){
            Console.Clear();
            Console.WriteLine(ConcatStringsByLine(grid.ToString(), $"\n    {character.Name}'s turn!"));

            currentPos = GridHelper.MoveCellItemInDirection(ref grid, currentPos, directionKeys[Console.ReadKey(true).Key]);

            moves++;
        }
    }
}