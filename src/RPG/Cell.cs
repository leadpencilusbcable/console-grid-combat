namespace RPG
{
    class Cell
    {
        public CellItem? CellItem { get; set; }

        public Cell()
        {
            CellItem = null;
        }

        public Cell(CellItem cellItem)
        {
            CellItem = cellItem;
        }

        public string ToString(string wall, int height, int innerWidth)
        {
            string ret = "";

            for(int i = 0; i < height; i++){
                string line = wall;

                for(int i2 = 0; i2 < innerWidth; i2++){
                    if(CellItem != null && (((i * innerWidth) + i2) < CellItem.VisualRepresentation.Length))
                        line += CellItem.VisualRepresentation[(i * innerWidth) + i2];
                    else
                        line += ' ';
                }

                ret += line + wall + (i == height - 1 ? "" : Environment.NewLine);
            }

            return ret;
        }
    }
}