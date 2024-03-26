namespace RPG
{
    class Team
    {
        public string Name { get; }
        public bool IsPlayer { get; }
        public Dictionary<string, Relationship> TeamRelationships { get; }
        public List<Tuple<Character, Vector2i>> Characters { get; }

        public Team(string name, bool isPlayer, Dictionary<string, Relationship> teamRelationships, List<Tuple<Character, Vector2i>> characters)
        {
            Name = name;
            IsPlayer = isPlayer;
            TeamRelationships = teamRelationships;
            Characters = characters;
        }
    }
}