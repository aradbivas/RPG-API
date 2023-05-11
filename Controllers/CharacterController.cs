using Microsoft.AspNetCore.Mvc;
using DotNet_rpg.Services.CharacterService;
using DotNet_rpg.DTO.Character;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace DotNet_rpg.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[Controller]")]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService characterService;

        public CharacterController(ICharacterService characterService)
        {
            this.characterService = characterService;

        }
        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> Get()
        {

            return  Ok( await characterService.GetAllCharacters());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> GetSingle(int id)
        {
            return  Ok( await characterService.GetCharacterById(id));
        }
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> AddCharacter(AddCharacterDto newCharacter)
        {
            return  Ok(await characterService.AddCharacter(newCharacter));
        }
        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> UpdateCharacter(UpdateCharacterDto updateCharacter)
        {
            var response = await characterService.UpdateCharacter(updateCharacter);
            if(response.Data == null)
            {
                return NotFound(response);
            }
            return  Ok(response);
        }
        [HttpDelete]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> DeleteCharacter(int id)
        {
            var response = await characterService.DeleteCharacter(id);
            if(response.Data == null)
            {
                return NotFound(response);
            }
            return  Ok(response);
        }
        [HttpPost("Skill")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> AddCharacterSkill(AddCharacterSkillDto addCharacterSkillDto)
        {
            var response = await characterService.AddCharacterSkill(addCharacterSkillDto);
            if(!response.Success)
            {
                return NotFound(response);
            }
            return  Ok(response);
        }
    }
}