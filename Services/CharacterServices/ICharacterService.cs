
using DotNet_rpg.DTO.Character;
namespace DotNet_rpg.Services.CharacterService;
public interface ICharacterService
{
        Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters();
        Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id);
        Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto character);
        Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updated);
         Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id);
         Task<ServiceResponse<GetCharacterDto>> AddCharacterSkill(AddCharacterSkillDto newCharacterSkill);



}