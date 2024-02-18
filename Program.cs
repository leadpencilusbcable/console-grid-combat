using RPG;

const int GRID_HEIGHT = 8;
const int GRID_WIDTH = 8;
const int CELL_HEIGHT = 3;
const int CELL_WIDTH = 5;

Grid grid = new(GRID_HEIGHT, GRID_WIDTH, CELL_HEIGHT, CELL_WIDTH);

List<Team> teams = await GridHelper.InitialiseTeams("./res/teams.json");
GridHelper.RandomiseCharacterLocations(ref grid, teams);

Console.WriteLine(grid.ToString());