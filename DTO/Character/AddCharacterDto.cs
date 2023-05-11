namespace DotNet_rpg.DTO.Character
{
    public class AddCharacterDto
    {
        public int HitPoints { get; set; } = 100;
        public int Stength { get; set; } = 10;
        public int Defense { get; set; } = 10;
        public int Intelligence { get; set; } = 10;
        public string Name { get; set; } = "arad";
        public RPGClass Class { get; set; } = RPGClass.knight;
    }
}