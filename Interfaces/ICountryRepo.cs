using pokemonAPI.Models;
namespace pokemonAPI.Interfaces
{
   public interface ICountryRepo 
   {
      /* ------------------------- read functionality ------------------------- */
      // get all countries
      Task<IEnumerable<Country>> GetCountries();
      // get country by id
      Task<Country> GetCountry(int countryID);
      // get all owners of specific country by country id
      Task<IEnumerable<Owner>> GetOwnersOfCountry(int countryId);
      /* -------------------------- create functionality -------------------------- */
      Task<bool> CreateCountry(Country country);
      Task<bool> IsCountryExistsByName (string countryName);
      bool SaveChanges();

   }
}