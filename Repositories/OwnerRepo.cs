using pokemonAPI.Data;
using pokemonAPI.Interfaces;
using pokemonAPI.Models;
using Microsoft.EntityFrameworkCore;
namespace pokemonAPI.Repositories
{
   public class OwnerRepo : IOwnerRepo
   {
      private readonly AppDbContext _context;
      public OwnerRepo(AppDbContext context)
      {
         this._context = context;
      }
      // get all owners 
      public async Task<IEnumerable<Owner>> GetOwners()
      {
         var owners = await this._context.Owners.ToListAsync();
         return owners;
      }
      // get owner by id
      public async Task<Owner> GetOwner(int ownerID)
      {
         var owner = await this._context.Owners.FindAsync(ownerID);
         return owner;
      }
      // get all pokemons of specific owner given the owner id
      public async Task<IEnumerable<Pokemon>> GetPokemonsByOwner(int ownerID)
      {
         var pokemons = await this._context.PokemonOwners.Where(PO => PO.OwnerId == ownerID).Select(PO => PO.Pokemon).AsNoTracking().ToListAsync();
         return pokemons;
      }

   }
}