using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using pokemonAPI.Interfaces;
using pokemonAPI.DTOs.CountryDtos;
using pokemonAPI.DTOs.OwnerDtos;
using pokemonAPI.Models;

namespace pokemonAPI.Controllers
{
   [ApiController]
   [Route("api/countries")]
   public class CountryController : ControllerBase
   {
      // DI pattern
      private readonly IMapper _mapper ;
      private readonly ICountryRepo _countryRepo;
      public CountryController(IMapper mapper, ICountryRepo countryRepo){
         this._countryRepo = countryRepo;
         this._mapper = mapper;
      }
      // get all countries
      [HttpGet("")]
      public async Task<IActionResult> GetCountries()
      {
         var countries = this._mapper.Map<IEnumerable<ReadCountry>>(await this._countryRepo.GetCountries());
         return Ok(countries);
      }
      // get country by id
      [HttpGet("{countryID:int}")]
      public async Task<IActionResult> GetCountryByID([FromRoute] int countryID)
      {
         var country = await this._countryRepo.GetCountry(countryID);
         if (country == null){
            return NotFound($"no country with ID : {countryID}");
         }
         return Ok(this._mapper.Map<ReadCountry>(country));
      }
      // get owners by country id
      [HttpGet("{countryID:int}/owners")]
      public async Task<IActionResult> GetOwnersByCountryId([FromRoute] int countryID)
      {
         var owners = await this._countryRepo.GetOwnersOfCountry(countryID);
         return Ok(this._mapper.Map<IEnumerable<ReadOwner>>(owners));
      }      
      [HttpPost("")]
      [ProducesResponseType(204)]
      [ProducesResponseType(400)]
      public async Task<IActionResult> CreateCountry([FromBody]CreateCountry country)
      {
         //* (1) Check if the body is empty
         if (country == null){
            return BadRequest(ModelState);
         }
         //* (2) check if this country is already exists in DB or not
         var alreadyExists = await this._countryRepo.IsCountryExistsByName(country.Name);
         if (alreadyExists == true){
            ModelState.AddModelError("", "this country already exists");
            return StatusCode(422, ModelState);
         }
         //* (3) check if the body is not null .. but its not valid 
         if (!ModelState.IsValid){
            return BadRequest(ModelState);
         }
         //* (4) Now we can start mapping and create the new item
         var newCountry = this._mapper.Map<Country>(country);
         var creationResult = await this._countryRepo.CreateCountry(newCountry);
         if (creationResult == true){
            return Ok("Succeed");
         }else{
            ModelState.AddModelError("", "Error while saving the new country into the Database");
            return StatusCode(500, ModelState);
         }
      }
   }
}






