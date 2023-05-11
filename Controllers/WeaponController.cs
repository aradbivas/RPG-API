using DotNet_rpg.DTO.Character;
using DotNet_rpg.DTO.Weapon;
using DotNet_rpg.Services.WeaponService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotNet_rpg.Controllers
{

    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class WeaponController : ControllerBase
    {
        private readonly IWeaponService _weaponService;
        public WeaponController(IWeaponService weaponService)
        {
            this._weaponService = weaponService;

        }

        [HttpPost("addWeapon")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> AddWeapon(AddWeaponDto newWeapon)
        {
            return Ok(await _weaponService.AddWeapon(newWeapon));
        }
    }
}