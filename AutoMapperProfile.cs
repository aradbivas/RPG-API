using AutoMapper;
using DotNet_rpg.DTO.Character;
using DotNet_rpg.DTO.Skill;
using DotNet_rpg.DTO.Weapon;

namespace DotNet_rpg;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Character,GetCharacterDto>();
        CreateMap<AddCharacterDto,Character>();
        CreateMap<UpdateCharacterDto, Character>();
        CreateMap<Weapon, GetWeaponDto>();
        CreateMap<Skill,GetSkillDto>();

    }
}