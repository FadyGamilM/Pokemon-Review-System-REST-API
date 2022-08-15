using pokemonAPI.Interfaces;
using pokemonAPI.Models;
using pokemonAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace pokemonAPI.Repositories
{
   public class CountryRepo : ICountryRepo
   {
      // DI pattern
      private readonly AppDbContext _context;
      public CountryRepo(AppDbContext context)
      {
         this._context = context;
      }

      // get all countries
      public async Task<IEnumerable<Country>> GetCountries()
      {
         var countries = await this._context.Countries.AsNoTracking().ToListAsync();
         return countries;
      }
      // get country by id      
      public async Task<Country> GetCountry(int countryID)
      {
         var country = await this._context.Countries.Where(C => C.Id == countryID).FirstOrDefaultAsync();
         return country;
      }
      // get owners by country id
      public async Task<IEnumerable<Owner>> GetOwnersOfCountry(int countryId)
      {
         var country = await this._context.Countries.FindAsync(countryId);
         await this._context.Entry(country).Collection(C => C.Owners).LoadAsync();
         return country.Owners;
      }
      // create a new country
      public async Task<bool> CreateCountry(Country country)
      {
         await this._context.Countries.AddAsync(country);
         return this.SaveChanges();
      }
      // check if the new country is exist in db before
      public async Task<bool> IsCountryExistsByName(string countryName)
      {
         var country = await this._context.Countries.Where(C => C.Name == countryName).FirstOrDefaultAsync();
         if (country == null){
            return false;
         }else{
            return true;
         }
      }
      public bool SaveChanges()
      {
         return (bool)(this._context.SaveChanges() > 0);
      }
   }
}