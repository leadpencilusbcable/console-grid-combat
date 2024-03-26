using RPG;

Game game = new();
await game.Initialise("./res/teams.json");
game.Start();