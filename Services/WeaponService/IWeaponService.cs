using DotNet_rpg.DTO.Character;
using DotNet_rpg.DTO.Weapon;

namespace DotNet_rpg.Services.WeaponService
{
    public interface IWeaponService
    {
         Task<ServiceResponse<GetCharacterDto>> AddWeapon(AddWeaponDto newWeapon);
    }
}