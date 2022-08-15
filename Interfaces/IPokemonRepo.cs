using pokemonAPI.Models;

namespace pokemonAPI.Interfaces
{
    public interface IPokemonRepo
    {
        // read all the pokemons
        Task<ICollection<Pokemon>> GetPokemons();         

        // the details page of a signle pokemon given the Id
        Task<Pokemon> GetPokemon(int Id);

        // the details page of a signle pokemon given the name
        Task<Pokemon> GetPokemon(string Name);

        // get the rating of specific pokemon given the id
        Task<decimal> GetPokemonRating (int Id);

        // check if specific pokemon exists given an Id
        Task<bool> IsPokemonExists(int Id); 

        // get all the reviews of specific pokemon 
        Task<IEnumerable<Review>> GetReviewsByPokemonId(int pokemonID);

        // create new pokemon
        Task<bool> CreatePokemon(Pokemon pokemon, int ownerID, int categoryID);
        // check if pokemon exists
        Task<bool> IsPokemonExistsByName(string Name);

        // save changes
        bool SaveChanges();
        
    }
}