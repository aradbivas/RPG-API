
using System.Security.Claims;
using AutoMapper;
using DotNet_rpg.Data;
using DotNet_rpg.DTO.Character;
using Microsoft.EntityFrameworkCore;

namespace DotNet_rpg.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _dataContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CharacterService(IMapper mapper, DataContext dataContext, IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;
            this._mapper = mapper;
            this._dataContext = dataContext;
        }
        private int GetUserId()
        {
            return int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto character)
        {
            var ServiceResponse = new ServiceResponse<List<GetCharacterDto>>();
            Character NormalCharacter = _mapper.Map<Character>(character);
            NormalCharacter.user = await _dataContext.Users.FirstOrDefaultAsync(c => c.Id == GetUserId());
            _dataContext.Add(NormalCharacter);
            await _dataContext.SaveChangesAsync();
            ServiceResponse.Data = await _dataContext.Characters
            .Where(c => c.user.Id == GetUserId())
            .Select(c => _mapper.Map<GetCharacterDto>(c))
            .ToListAsync();
            return ServiceResponse;

        }
        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updateCharacter)
        {
            var ServiceResponse = new ServiceResponse<GetCharacterDto>();

            try
            {

                Character foundcharacter = await _dataContext.Characters.
                Include(c => c.user).
                FirstOrDefaultAsync(c => c.Id == updateCharacter.Id);
                if(foundcharacter.user.Id == GetUserId())
                {
                    foundcharacter.Name = updateCharacter.Name;
                    foundcharacter.HitPoints = updateCharacter.HitPoints;
                    foundcharacter.Stength = updateCharacter.Stength;
                    foundcharacter.Defense = updateCharacter.Defense;
                    foundcharacter.Intelligence = updateCharacter.Intelligence;
                    foundcharacter.Class = updateCharacter.Class;

                    await _dataContext.SaveChangesAsync();


                    ServiceResponse.Data = _mapper.Map<GetCharacterDto>(foundcharacter);
                }
                else
                {
                    ServiceResponse.Success = false;
                    ServiceResponse.Message = "Character not found";
                }

            }
            catch (Exception ex)
            {
                ServiceResponse.Success = false;
                ServiceResponse.Message = ex.Message;
            }
            return ServiceResponse;

        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            var ServiceResponse = new ServiceResponse<List<GetCharacterDto>>();
            var dbCharacters = await _dataContext.Characters
            .Include(c => c.Skills)
            .Include(c => c.Weapon).
            Where(c => c.user.Id == GetUserId()).ToListAsync();
            ServiceResponse.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return ServiceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            var ServiceResponse = new ServiceResponse<GetCharacterDto>();
            var dbCharacters = await _dataContext.Characters
            .Include(c => c.Skills)
            .Include(c => c.Weapon)
            .FirstOrDefaultAsync(c => c.Id == id && c.user.Id == GetUserId());
            ServiceResponse.Data = _mapper.Map<GetCharacterDto>(dbCharacters);
            return ServiceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            var ServiceResponse = new ServiceResponse<List<GetCharacterDto>>();
            try
            {
                Character foundCharacter = await _dataContext.Characters.FirstOrDefaultAsync(c => c.Id == id && c.user.Id == GetUserId());
                if(foundCharacter != null)
                {
                    _dataContext.Characters.Remove(foundCharacter);
                    await _dataContext.SaveChangesAsync();
                    ServiceResponse.Data = _dataContext.Characters
                    .Where(c => c.user.Id == GetUserId())
                    .Select(c => _mapper.Map<GetCharacterDto>(c))
                    .ToList();
                }
                else{
                    ServiceResponse.Success = false;
                    ServiceResponse.Message = "Character not found";
                }


            }
            catch (Exception ex)
            {
                ServiceResponse.Success = false;
                ServiceResponse.Message = ex.Message;
            }
            return ServiceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> AddCharacterSkill(AddCharacterSkillDto newCharacterSkill)
        {
          var ServiceResponse = new ServiceResponse<GetCharacterDto>();
          try
          {
             var character = await _dataContext.Characters
             .Include(c => c.Weapon)
             .Include(c => c.Skills)
             .FirstOrDefaultAsync(c => c.Id == newCharacterSkill.CharacterId && c.user.Id == GetUserId());
             if(character == null)
             {
                ServiceResponse.Success = false;
                ServiceResponse.Message = "Charcter not found";
                return ServiceResponse;
             }
             var Skill = await _dataContext.Skills.FirstOrDefaultAsync(s => s.Id == newCharacterSkill.SkillId);
             if(Skill == null)
             {
                ServiceResponse.Success = false;
                ServiceResponse.Message = "Skill not found";
                return ServiceResponse;
             }
             character.Skills.Add(Skill);
             await _dataContext.SaveChangesAsync();
             ServiceResponse.Data = _mapper.Map<GetCharacterDto>(character);
            

          }
          catch(Exception ex)
          {
            ServiceResponse.Success = false;
            ServiceResponse.Message = ex.Message;
          }

          return ServiceResponse;
        }
    }
}