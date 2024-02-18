namespace Json
{
    class TeamJson
    {
        public string name { get; set; }
        public bool isPlayer { get; set; }
        public List<CharacterJson> members { get; set; }
        public List<RelationshipJson> relationships { get; set; }
    }
}