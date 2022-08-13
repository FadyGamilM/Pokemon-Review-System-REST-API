using pokemonAPI.Models;
namespace pokemonAPI.Interfaces
{
   public interface ICountryRepo 
   {
      // get all countries
      Task<IEnumerable<Country>> GetCountries();
      // get country by id
      Task<Country> GetCountry(int countryID);
      // get all owners of specific country by country id
      Task<IEnumerable<Owner>> GetOwnersOfCountry(int countryId);
   }
}