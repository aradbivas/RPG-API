using System.Security.Claims;
using AutoMapper;
using DotNet_rpg.Data;
using DotNet_rpg.DTO.Character;
using DotNet_rpg.DTO.Weapon;
using Microsoft.EntityFrameworkCore;

namespace DotNet_rpg.Services.WeaponService
{
    public class WeaponService : IWeaponService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        public WeaponService(DataContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            this._mapper = mapper;
            this._httpContextAccessor = httpContextAccessor;
            this._context = context;

        }
        public async Task<ServiceResponse<GetCharacterDto>> AddWeapon(AddWeaponDto newWeapon)
        {
            ServiceResponse<GetCharacterDto> serviceResponse = new ServiceResponse<GetCharacterDto>();

            try
            {
            Character character = await _context.Characters
            .FirstOrDefaultAsync(c => c.Id == newWeapon.CharacterId && c.user.Id == int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)));
            if(character == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Character not found";
                return serviceResponse;
            }
            Weapon weapon = new Weapon{
                Name = newWeapon.Name,
                Damage = newWeapon.Damage,
                Character = character
            };
            _context.Weapons.Add(weapon);
            await _context.SaveChangesAsync();
            serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);

            }
            catch(Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
    }
}