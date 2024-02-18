namespace RPG
{
    class Character : CellItem
    {
        public string Name { get; }
        public int Health { get ; set; }
        public int MoveSpeed { get; set; }
        public string TeamName { get; }

        public Character(string visualRepresentation, string name, int health, int moveSpeed, string teamName): base(visualRepresentation)
        {
            Name = name;
            Health = health;
            MoveSpeed = moveSpeed;
            TeamName = teamName;
        }
    }
}