namespace RPG
{
    abstract class CellItem
    {
        public string VisualRepresentation { get; set; }

        public CellItem(string visualRepresentation)
        {
            VisualRepresentation = visualRepresentation;
        }
    }
}