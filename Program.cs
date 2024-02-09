using RPG;
using System.Diagnostics;

Grid grid = new(5, 5, 3, 5);

Stopwatch sw = new();

sw.Start();

Console.WriteLine(grid.ToString());

sw.Stop();

Console.WriteLine("Elapsed={0}",sw.Elapsed);