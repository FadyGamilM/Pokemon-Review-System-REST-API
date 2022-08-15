using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using pokemonAPI.DTOs.PokemonDtos;
using pokemonAPI.Interfaces;
using pokemonAPI.DTOs.ReviewDtos;
using pokemonAPI.Models;

namespace pokemonAPI.Controllers
{  
   [ApiController]
   [Route("api/pokemons")]
   public class PokemonController : ControllerBase
   {
      //! Inject the Pokemon Repository Service 
      private readonly IPokemonRepo _pokemonRepo;
      //! Inject the AutoMapper Service
      private readonly IMapper _mapper;
      public PokemonController(IPokemonRepo pokemonRepo, IMapper mapper)
      {
         this._pokemonRepo = pokemonRepo;
         this._mapper = mapper;
      }

      // Get All Pokemons
      [HttpGet("")]
      // [ProducesResponseType(200, Type =typeof(IEnumerable<Pokemon>))]
      public async Task<IActionResult> GetPokemons ()
         {
            // get all pokemons in form of Pokemon class model
            var pokemons = await this._pokemonRepo.GetPokemons();
            // convert the result from DB into the reading DTO
            var AllPokemons = this._mapper.Map<IEnumerable<ReadPokemon>>(pokemons);
            // return the response
            return Ok(AllPokemons);
         }

         // Get Pokemon By Id "The pokemon details page"
         [HttpGet("{Id:int}")]
         public async Task<IActionResult> GetPokemonById([FromRoute]int Id)
         {
            // get the pokemon from DB
            var pokemon = await this._pokemonRepo.GetPokemon(Id);
            // if its null return Not Found 404
            if (pokemon == null){
               return NotFound($"There is no pokemon with ID : {Id}");
            }
            // map to the DTO and return the response
            return Ok(this._mapper.Map<ReadPokemon>(pokemon));
         }

         // Get Pokemon By Name "The pokemon details page"
         [HttpGet("{Name}")]
         public async Task<IActionResult> GetPokemonByName([FromRoute]string Name)
         {
            // get the pokemon from DB
            var pokemon = await this._pokemonRepo.GetPokemon(Name);
            // if its null return Not Found 404
            if (pokemon == null){
               return NotFound($"There is no pokemon with Name : {Name}");
            }
            // map to the DTO and return the response
            return Ok(this._mapper.Map<ReadPokemon>(pokemon));
         }

         // Get Pokemon rating By Id "The pokemon details page"
         [HttpGet("{Id:int}/rating")]
         public async Task<IActionResult> GetPokemonRating([FromRoute]int Id)
         {
            var pokemonRating = await this._pokemonRepo.GetPokemonRating(Id);
            return Ok(pokemonRating);
         }

         // Checkif pokemon is exist "The pokemon details page"
         [HttpGet("{Id:int}/exists")]
         public async Task<IActionResult> CheckIfPokemonExist([FromRoute]int Id)
         {
            var exists = await this._pokemonRepo.IsPokemonExists(Id);
            return Ok(exists);
         }

         // get all reviews of specific pokeomon
         [HttpGet("{pokemonID:int}/reviews")]
         public async Task<IActionResult> GetPokemonReviews ([FromRoute] int pokemonID)
         {
            var reviews = await this._pokemonRepo.GetReviewsByPokemonId(pokemonID);
            return Ok(this._mapper.Map<IEnumerable<ReadReview>>(reviews));
         }

         // create a new pokemon
         [HttpPost("")]
         [ProducesResponseType(204)]
         [ProducesResponseType(400)]
         public async Task<IActionResult> CreatePokemon ( [FromBody] CreatePokemon pokemonDto,
                                                                                    [FromQuery] int ownerID,
                                                                                    [FromQuery] int categoryID)
         {
            if (pokemonDto == null){
               return BadRequest(ModelState);
            }
            var ExistedPokemon = await this._pokemonRepo.IsPokemonExistsByName(pokemonDto.Name);
            if (ExistedPokemon == true){
               ModelState.AddModelError("", "This pokemon already exists");
               return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid){
               return BadRequest(ModelState);
            }
            var pokemon = this._mapper.Map<Pokemon>(pokemonDto);
            var CreationResult = await this._pokemonRepo.CreatePokemon(pokemon, ownerID, categoryID);
            if (CreationResult == false){
               ModelState.AddModelError("", "Error while saving the new pokemon into the DB");
               return StatusCode(500, ModelState);
            }else{
               return Ok("Succeed");
            }

         }
   }

}