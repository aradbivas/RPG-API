namespace DotNet_rpg.Models
{
    public class Skill
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Damadge { get; set; }

        public List<Character> Characters { get; set; }
    }
}