using pokemonAPI.Models;
namespace PokemonAPI.Interfaces
{
   public interface ICategoryRepo
   {
      //! get all categories
      Task<IEnumerable<Category>> GetCategories();
      //! get specific category by its Id
      Task<Category> GetCategory(int Id);
      //! get all pokemons by given category
      Task<Pokemon> GetPokemonByCategory(int Id);
      //! check if specific category is exist by given id
      Task<bool> IsCategoryExists (int Id);
   }
}