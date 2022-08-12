using Microsoft.EntityFrameworkCore;
using pokemonAPI.Data;
using pokemonAPI.Interfaces;
using pokemonAPI.Models;

namespace pokemonAPI.Repositories
{
   public class PokemonRepo : IPokemonRepo
   {
      //* the DbContext service 
      private readonly AppDbContext _context;
      public PokemonRepo(AppDbContext context)
      {
         this._context = context;
      }
      public async Task<ICollection<Pokemon>> GetPokemons()
      {
         var pokemons = await this._context.Pokemons.OrderBy(p => p.Name)
                                                                              .AsNoTracking()
                                                                              .ToListAsync();
         return pokemons;
      }

      public async Task<Pokemon> GetPokemon(int Id)
      {
         var pokemon = await this._context.Pokemons.FindAsync(Id);
         return pokemon;
      }

      public async Task<Pokemon> GetPokemon(string Name)
      {
         var pokemon = await this._context.Pokemons.Where(p => p.Name == Name).FirstOrDefaultAsync();
         return pokemon;
      }

      public async Task<decimal> GetPokemonRating(int Id)
      {
         // we need to fetch the reviews first and check if there is no reviews for this
         // specific pokemon we return 0
         // thats the reason we can't query the average dierctly because the number of reviews might be 0 and divide by 0 is error 
         var reviews = this._context.Reviews.Where(R => R.PokemonId == Id);
         if (reviews.Count() == 0 )
         {
            return 0;
         }
         var sumOfRatings = reviews.Sum(R=>R.Rating);
         return (decimal)(sumOfRatings / reviews.Count()); 
      }


      public async Task<bool> IsPokemonExists(int Id)
      {
         var exists = this._context.Pokemons.Any(P => P.Id == Id);
         return exists;
      }
   }
}