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

      [HttpPost("")]
      [ProducesResponseType(204)]
      [ProducesResponseType(400)]
      public async Task<IActionResult> CreateCategory([FromBody] CreateCategory category)
      {
         //* if the body of the request is empty .. so its a bad request
         if (category == null)
         {
            return BadRequest(ModelState);
         }
         //* then we need check if we have an instance of this category before ..
         var isCategoryExist = await this._categoryRepo.IsCategoryExistsByName(category.Name);
         // if the category exists .. return 422 "the server understand the request and the body is right but it can't proceed with the rquest" 
         if (isCategoryExist == true){
            ModelState.AddModelError("", "Category already exists in DB");
            return StatusCode(422, ModelState);
         }
         //* now if the body of the request is not valid .. return bad request 400
         if(! ModelState.IsValid){
            return BadRequest(ModelState);
         }
         //* mapping to the DB format
         var newCategory = this._mapper.Map<Category>(category);
         var creationResult = await this._categoryRepo.CreateCategory(newCategory);
         if(!creationResult){
            ModelState.AddModelError("", "Error while Saving the new category into the DB");
            return StatusCode(500, ModelState);
         }
         return Ok("succeed");

      }

   }
}