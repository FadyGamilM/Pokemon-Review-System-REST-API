using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using pokemonAPI.DTOs.CategoryDtos;
using pokemonAPI.DTOs.PokemonDtos;
using pokemonAPI.Interfaces;
using pokemonAPI.Models;

namespace pokemonAPI.Controllers
{
   [ApiController]
   [Route("api/categories")]
   public class CategoryController : ControllerBase
   {
      // Dependency Injection Pattern
      private readonly ICategoryRepo _categoryRepo;
      private readonly IMapper _mapper;
      public CategoryController(ICategoryRepo categoryRepo, IMapper mapper)
      {
         this._categoryRepo = categoryRepo;
         this._mapper = mapper;
      }

      // Get all categories
      [HttpGet("")]
      public async Task<IActionResult> GetCategoriesAsync()
      {
         var categories = this._mapper.Map<IEnumerable<ReadCategory>>(await this._categoryRepo.GetCategories());
         return Ok(categories);
      }
      // Get category by id
      [HttpGet("{Id:int}")]
      public async Task<IActionResult> GetCategoryByIdAsync([FromRoute]int Id)
      {
         var category = this._mapper.Map<ReadCategory>(await this._categoryRepo.GetCategory(Id));
         if (category == null){
            return NotFound($"There is no category with ID : {Id}");
         }
         return Ok(category);
      }
      // get pokemons by category id
      [HttpGet("{Id:int}/pokemons")]
      public async Task<IActionResult> GetPokemonsByCategoryId([FromRoute] int Id)
      {
         var pokemons = await this._categoryRepo.GetPokemonByCategory(Id);
         var pokemonsDTO = this._mapper.Map<IEnumerable<ReadPokemon>>(pokemons);
         return Ok(pokemonsDTO);
      }
   }
}