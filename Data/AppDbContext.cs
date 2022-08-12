using Microsoft.EntityFrameworkCore;
using pokemonAPI.Models;
namespace pokemonAPI.Data
{
   public class AppDbContext : DbContext
   {
      public AppDbContext(DbContextOptions<AppDbContext> options) : base (options){}

      protected override void OnModelCreating (ModelBuilder modelBuilder)
      {
         modelBuilder.Entity<PokemonCategory>()
                        .HasKey(P_C => new {
                           P_C.PokemonId,
                           P_C.CategoryId
                        });
         modelBuilder.Entity<PokemonCategory>()
                        .HasOne(P_C => P_C.Pokemon)
                        .WithMany(P => P.PokemonCategories)
                        .HasForeignKey(P_C => P_C.PokemonId);
         modelBuilder.Entity<PokemonCategory>()
                        .HasOne(P_C => P_C.Category)
                        .WithMany(C => C.PokemonCategories)
                        .HasForeignKey(P_C => P_C.CategoryId);

         modelBuilder.Entity<PokemonOwner>()
                        .HasKey(P_O => new {
                           P_O.PokemonId,
                           P_O.OwnerId
                        });
         modelBuilder.Entity<PokemonOwner>()
                        .HasOne(P_O => P_O.Pokemon)
                        .WithMany(P => P.PokemonOwners)
                        .HasForeignKey(P_O => P_O.PokemonId);
         modelBuilder.Entity<PokemonOwner>()
                        .HasOne(P_O => P_O.Owner)
                        .WithMany(O => O.PokemonOwners)
                        .HasForeignKey(P_O => P_O.OwnerId);
      }

      // register the domain class models
      public DbSet<Category> Categories {get; set;}
      public DbSet<Country> Countries {get; set;}
      public DbSet<Owner> Owners {get; set;}
      public DbSet<Review> Reviews {get; set;}
      public DbSet<Reviewer> Reviewers {get; set;}
      public DbSet<Pokemon> Pokemons {get; set;}
      public DbSet<PokemonCategory> PokemonCategories {get; set;}
      public DbSet<PokemonOwner> PokemonOwners {get; set;}
   }
}