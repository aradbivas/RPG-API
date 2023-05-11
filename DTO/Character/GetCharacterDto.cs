using DotNet_rpg.DTO.Skill;
using DotNet_rpg.DTO.Weapon;

namespace DotNet_rpg.DTO.Character
{
    public class GetCharacterDto
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
        public GetWeaponDto Weapon {get;set;}
        public List<GetSkillDto> Skills { get; set; }
        
    }
}