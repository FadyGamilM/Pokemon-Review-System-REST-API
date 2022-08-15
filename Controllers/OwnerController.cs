using Microsoft.AspNetCore.Mvc;
using pokemonAPI.Interfaces;
using pokemonAPI.DTOs.OwnerDtos;
using pokemonAPI.DTOs.PokemonDtos;
using AutoMapper;
using pokemonAPI.Models;

namespace pokemonAPI.Controllers
{
   [ApiController]
   [Route("api/owners")]
   public class OwnerController : ControllerBase
   {
      // DI pattern
      private readonly IOwnerRepo _ownerRepo;
      private readonly ICountryRepo _countryRepo;
      private readonly IMapper _mapper;
      public OwnerController(IOwnerRepo ownerRepo, IMapper mapper, ICountryRepo countryRepo)
      {
         this._mapper = mapper;
         this._ownerRepo = ownerRepo;
         this._countryRepo = countryRepo;
      }
      // get all owners 
      [HttpGet("")]
      public async Task<IActionResult> GetOwners()
      {
         var owners = await this._ownerRepo.GetOwners();
         return Ok(this._mapper.Map<IEnumerable<ReadOwner>>(owners));
      } 
      // get owner by owner id
      [HttpGet("{ownerID:int}")]
      public async Task<IActionResult> GetOwnerById([FromRoute] int ownerID)
      {
         var owner = await this._ownerRepo.GetOwner(ownerID);
         if (owner == null){
            return NotFound($"there is no owner with ID : {ownerID}");
         }
         return Ok(this._mapper.Map<ReadOwner>(owner));
      }
      // get all pokemons of this specific owner given owner id
      [HttpGet("{ownerID:int}/pokemons")]
      public async Task<IActionResult> GetPokemonsByOwner([FromRoute] int ownerID)
      {
         var pokemons = await this._ownerRepo.GetPokemonsByOwner(ownerID);
         return Ok(this._mapper.Map<IEnumerable<ReadPokemon>>(pokemons));
      }
      // create a new owner
      [HttpPost("")]
      [ProducesResponseType(204)]
      [ProducesResponseType(400)]
      public async Task<IActionResult> CreateOwner([FromQuery] int counretID, [FromBody] CreateOwner ownerDto)
      {
         if (ownerDto == null){
            return BadRequest(ModelState);
         }

         if (!ModelState.IsValid){
            return BadRequest(ModelState);
         }

         var owner = this._mapper.Map<Owner>(ownerDto);
         owner.Country = await this._countryRepo.GetCountry(counretID);
         var creationResult = await this._ownerRepo.CreateOwner(owner);
         if (creationResult==true){
            return Ok("Succeed");
         } else{
            ModelState.AddModelError("", "Error while saving the new owner in DB");
            return StatusCode(500, ModelState);
         }
      }
   }
}