using pokemonAPI.Models;
namespace pokemonAPI.Interfaces
{
    public interface IOwnerRepo
    {
        // get all owners 
        Task<IEnumerable<Owner>> GetOwners();
        // get owner by id
        Task<Owner> GetOwner(int ownerID);
        // get all pokemons of specific owner given the owner id
        Task<IEnumerable<Pokemon>> GetPokemonsByOwner(int ownerID);
        // create new owner
        Task<bool> CreateOwner(Owner owner);
        // save changes
        bool SaveChanges();
    }
}