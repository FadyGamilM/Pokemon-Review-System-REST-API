using pokemonAPI.Models;
using pokemonAPI.Interfaces;
using pokemonAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace pokemonAPI.Repositories
{
   public class CategoryRepo : ICategoryRepo
   {
      private readonly AppDbContext _context;
      public CategoryRepo(AppDbContext context)
      {
         this._context = context;
      }

      public async Task<IEnumerable<Category>> GetCategories()
      { 
         var categories = await this._context.Categories
                                 .OrderBy(C=>C.Name)
                                 .ToListAsync();
         return categories;
      }

      public async Task<Category> GetCategory(int Id)
      {
         var category = await this._context.Categories.FindAsync(Id);
         return category;
      }

      public async Task<IEnumerable<Pokemon>> GetPokemonByCategory(int Id)
      {
         var pokemons = await this._context.PokemonCategories.Where(
            PC => PC.CategoryId == Id
         ).Select(
            PC => PC.Pokemon
         ).AsNoTracking().ToListAsync();
         return pokemons;
      }

      public async Task<bool> IsCategoryExists(int Id)
      {
         var exists = this._context.Categories.Any(C => C.Id == Id);
         return exists;
      }

      public async Task<bool> CreateCategory(Category category)
      {
         await this._context.Categories.AddAsync(category);
         var CreatitonResult = this.SaveChanges();
         return CreatitonResult;
      }
      public async Task<bool> IsCategoryExistsByName(string categoryName)
      {
         var category = await this._context.Categories.Where(C => C.Name.Trim().ToUpper() == categoryName.TrimEnd().ToUpper()).FirstOrDefaultAsync();
         if (category != null){
            return true;
         }else{
            return false;
         }
      }

      public bool SaveChanges()
      {
         return (bool)(this._context.SaveChanges() > 0);
      }
   }
}