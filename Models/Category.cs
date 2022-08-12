namespace pokemonAPI.Models
{
   public class Category
   {
      [Key]
      public int Id { get; set; }
      public string Name { get; set; }
      //! Many-To-Many relations 
      public ICollection<PokemonCategory> PokemonCategories {get; set;}
   }
}