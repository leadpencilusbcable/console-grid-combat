static class Mappings
{
    public static readonly Dictionary<Direction, Vector2i> DirectionVecs = new(){
        { Direction.N, new Vector2i(-1, 0) },
        { Direction.NE, new Vector2i(-1, 1) },
        { Direction.E, new Vector2i(0, 1) },
        { Direction.SE, new Vector2i(1, 1) },
        { Direction.S, new Vector2i(1, 0) },
        { Direction.SW, new Vector2i(1, -1) },
        { Direction.W, new Vector2i(0, -1) },
        { Direction.NW, new Vector2i(-1, -1) }
    };

    public static readonly Dictionary<ConsoleKey, Direction> DirectionKeyMapping = new(){
        { ConsoleKey.D8, Direction.N },
        { ConsoleKey.D9, Direction.NE },
        { ConsoleKey.D6, Direction.E },
        { ConsoleKey.D3, Direction.SE },
        { ConsoleKey.D2, Direction.S },
        { ConsoleKey.D1, Direction.SW },
        { ConsoleKey.D4, Direction.W },
        { ConsoleKey.D7, Direction.NW }
    };
}