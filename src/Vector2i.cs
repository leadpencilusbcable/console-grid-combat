struct Vector2i
{
    public int X { get; }
    public int Y { get; }

    public Vector2i(int x, int y)
    {
        X = x;
        Y = y;
    }

    public static Vector2i operator +(Vector2i a, Vector2i b) => new Vector2i(a.X + b.X, a.Y + b.Y);
}