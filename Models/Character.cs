namespace DotNet_rpg.Models
{
    public class Character
    {
        public int Id { get; set; }
        public int HitPoints { get; set; } = 100;
        public int Stength { get; set; } = 10;
        public int Defense { get; set; } = 10;
        public int Intelligence { get; set; } = 10;
        public string Name { get; set; } = "arad";
        public RPGClass Class { get; set; } = RPGClass.knight;

        public int Fights { get; set; }
        public int Victories { get; set; }
        public int  Defets { get; set; }

        public User? user { get;set;}
        public Weapon Weapon {get; set;}
        public List<Skill> Skills { get; set; }
    }
}