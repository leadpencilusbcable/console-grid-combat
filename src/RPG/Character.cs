namespace RPG
{
    class Character
    {
        public string Name { get; }
        public Status Status { get; set; }
        public int MaxHealth { get; }
        public int Health { get; set; }
        public int MoveSpeed { get; set; }
        public int Initiative { get; }

        public Character(string name, int maxHealth, int moveSpeed, int initiative)
        {
            Name = name;
            MaxHealth = maxHealth;
            Health = maxHealth;
            MoveSpeed = moveSpeed;
            Initiative = initiative;

            CheckIfDead();
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;

            CheckIfDead();
        }

        private void CheckIfDead()
        {
            Status = MaxHealth > 0 ? Status.Healthy : Status.Dead;
        }
    }
}