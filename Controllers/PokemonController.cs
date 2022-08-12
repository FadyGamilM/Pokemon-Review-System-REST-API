using Microsoft.AspNetCore.Mvc;
using pokemonAPI.Interfaces;
using pokemonAPI.Models;
namespace PokemonAPI.Controllers
{  
   [ApiController]
   [Route("api/pokemons")]
   public class PokemonController : ControllerBase
   {
      private readonly IPokemonRepo _pokemonRepo;
      public PokemonController(IPokemonRepo pokemonRepo)
      {
         this._pokemonRepo = pokemonRepo;
      }

      // Get All Pokemons
      [HttpGet("")]
      [ProducesResponseType(200, Type =typeof(IEnumerable<Pokemon>))]
      public async Task<IActionResult> GetPokemons ()
         {
            var pokemons = await this._pokemonRepo.GetPokemons();
            // if (!ModelState.IsValid)
            //    return BadRequest(ModelState);
            return Ok(pokemons);
         }

         // Get Pokemon By Id "The pokemon details page"
         [HttpGet("{Id:int}")]
         public async Task<IActionResult> GetPokemon([FromRoute]int Id)
         {
            var pokemon = await this._pokemonRepo.GetPokemon(Id);
            if (pokemon == null){
               return NotFound($"There is no pokemon with ID : {Id}");
            }
            return Ok(pokemon);
         }
   }

}