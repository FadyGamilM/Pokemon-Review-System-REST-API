namespace pokemonAPI.Models
{
   public class Review
   {
      [Key]
      public int Id {get; set;}
      public string Title { get; set; }
      public string Text { get; set; }     

      //! One-To-Many relation with the Reviewer table [the N-side]
      public int ReviewerId {get; set;}
      public Reviewer Reviewer {get; set;} 

      //! One-To-Many relation with the Pokemon table [the N-side]
      public int PokemonId {get; set;}
      public Pokemon Pokemon {get; set;}
   }
}