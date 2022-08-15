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

      // get all reviews of specific pokemon
      public async Task<IEnumerable<Review>> GetReviewsByPokemonId(int pokemonID)
      {
         var reviews = await this._context.Reviews.Where(R => R.PokemonId== pokemonID).AsNoTracking().ToListAsync();
         return reviews;
      }


      public async Task<bool> IsPokemonExists(int Id)
      {
         var exists = this._context.Pokemons.Any(P => P.Id == Id);
         return exists;
      }

      public async Task<bool> CreatePokemon(Pokemon pokemon, int ownerID, int categoryID)
      {
         //* (1) get the entities from the tables that the pokemon make a M-N relatioship with
         var categoryEntity = await this._context.Categories.Where(C => C.Id == categoryID).FirstOrDefaultAsync();
         var ownerEntity = await this._context.Owners.Where(O => O.Id == ownerID).FirstOrDefaultAsync();
         //* (2) Create the join-table entities to be added to the join-tables
         var PokemonCategoryEntity = new PokemonCategory()
         {
            Pokemon = pokemon,
            Category = categoryEntity
         };
         var PokemonOwnerEntity = new PokemonOwner()
         {
            Pokemon = pokemon,
            Owner = ownerEntity
         };
         //* (3) Add the join-Entities to the join-tables
         await this._context.PokemonCategories.AddAsync(PokemonCategoryEntity);
         await this._context.PokemonOwners.AddAsync(PokemonOwnerEntity);
         //* (4) add the Pokemon entity now 
         await this._context.Pokemons.AddAsync(pokemon);
         //* (5) save the tracked changes by EFCore
         return this.SaveChanges();
      }

      public async Task<bool> IsPokemonExistsByName(string pokemonName)
      {
         var exists = await this._context.Pokemons.Where(P => P.Name == pokemonName).FirstOrDefaultAsync();
         if (exists == null){
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