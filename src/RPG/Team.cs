namespace RPG
{
    class Team
    {
        public string Name { get; }
        public bool IsPlayer { get; }
        public List<Character> Members { get; }
        public Dictionary<string, Relationship> TeamRelationships { get; }

        public Team(string name, bool isPlayer, List<Character> members, Dictionary<string, Relationship> teamRelationships)
        {
            Name = name;
            IsPlayer = isPlayer;
            Members = members;
            TeamRelationships = teamRelationships;
        }
    }
}