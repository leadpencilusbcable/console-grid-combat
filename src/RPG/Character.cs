namespace RPG
{
    class Character : CellItem
    {
        public float MoveSpeed { get; set; }

        public Character(string visualRepresentation, float moveSpeed): base(visualRepresentation)
        {
            MoveSpeed = moveSpeed;
        }
    }
}