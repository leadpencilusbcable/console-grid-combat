using static HelperFunctions;

namespace RPG
{
    class Grid
    {
        public int Height { get; }
        public int Width { get; }
        private readonly Cell[,] Cells;
        private readonly int CellHeight;
        private readonly int CellWidth;

        public Grid(int height, int width, int cellHeight, int cellWidth)
        {
            Height = height;
            Width = width;

            Cells = new Cell[height, width];

            for(int i = 0; i < Cells.GetLength(0); i++){
                for(int i2 = 0; i2 < Cells.GetLength(1); i2++)
                    Cells[i, i2] = new Cell();
            }

            CellHeight = cellHeight;
            CellWidth = cellWidth;
        }

        public CellItem GetCellItem(Vector2i pos)
        {
            return Cells[pos.X - 1, pos.Y - 1].CellItem;
        }

        public void AddCellItem(CellItem cellItem, Vector2i pos)
        {
            Cells[pos.X - 1, pos.Y - 1].CellItem = cellItem;
        }

        public void RemoveCellItem(Vector2i pos)
        {
            Cells[pos.X - 1, pos.Y - 1].CellItem = null;
        }

        public void MoveCellItem(Vector2i startPos, Vector2i endPos)
        {
            AddCellItem(Cells[startPos.X - 1, startPos.Y - 1].CellItem, endPos);
            RemoveCellItem(startPos);
        }

        public List<Vector2i> GetVacantCells()
        {
            List<Vector2i> vacantPos = new();

            for(int i = 0; i < Cells.GetLength(0); i++){
                for(int i2 = 0; i2 < Cells.GetLength(1); i2++){
                    if(Cells[i, i2].CellItem == null)
                        vacantPos.Add(new Vector2i(i + 1, i2 + 1));
                }
            }

            return vacantPos;
        }

        public override string ToString()
        {
            string separator = new('-', (CellWidth + 2) * Cells.GetLength(1));

            string grid = (
                "  " +
                string.Join(new string(' ', CellWidth + 1), Enumerable.Range('A', Cells.GetLength(1)).Select(x => (char) x).ToArray()) +
                new string(' ', CellWidth + 1) +
                Environment.NewLine
            );

            for(int i = 0; i < Cells.GetLength(0); i++){
                grid += (
                    ' ' +
                    separator +
                    Environment.NewLine
                );

                string line = "";

                for(int i2 = 0; i2 < Cells.GetLength(1); i2++){
                    line = ConcatStringsByLine(
                        line,
                        Cells[i, i2].ToString("|", CellHeight, CellWidth)
                    );
                }

                line = ConcatStringsByLine(
                    (i + 1) + Environment.NewLine.Repeat(CellHeight - 1, " ") + ' ',
                    line
                );

                grid += line + Environment.NewLine;
            }

            return grid + ' ' + separator;
        }
    }
}