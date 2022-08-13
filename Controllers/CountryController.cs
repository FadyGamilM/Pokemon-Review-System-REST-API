using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using pokemonAPI.Interfaces;
using pokemonAPI.DTOs.CountryDtos;
using pokemonAPI.DTOs.OwnerDtos;
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
   }
}