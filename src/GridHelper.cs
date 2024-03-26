using RPG;
using static Mappings;
using static HelperFunctions;

static class GridHelper
{
    public static Vector2i RandomiseCellItemLocation(ref Grid grid, CellItem cellItem)
    {
        List<Vector2i> vacantLocs = grid.GetVacantCells();
        Vector2i vacantLoc = vacantLocs[rand.Next(0, vacantLocs.Count - 1)];

        grid.AddCellItem(cellItem, vacantLoc);

        return vacantLoc;
    }

    public static List<Direction> GetValidDirections(Grid grid, Vector2i pos)
    {
        return DirectionVecs.Where(direction => {
            Vector2i newPos = pos + direction.Value;

            return newPos.X <= grid.Height && newPos.Y <= grid.Width && newPos.X > 0 && newPos.Y > 0 && grid.GetCellItem(newPos) == null;
        }).Select(direction => direction.Key).ToList();
    }

    public static List<ConsoleKey> GetValidKeys(List<Direction> directions)
    {
        return DirectionKeyMapping.Where(
            key => directions.Contains(key.Value)
        ).Select(key => key.Key).ToList();
    }

    public static Vector2i MoveCellItemInDirection(ref Grid grid, Vector2i pos, Direction direction)
    {
        Vector2i newPos = pos + DirectionVecs[direction];
        grid.MoveCellItem(pos, newPos);

        return newPos;
    }
}