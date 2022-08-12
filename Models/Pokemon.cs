namespace pokemonAPI.Models
{
   public class Pokemon
   {
      [Key]
      public int Id { get; set; }
      public string Name { get; set; }
      public DateTime Birthdate { get; set; }

      //! One To Many relation [the 1-Side]
      public ICollection<Review> Reviews {get; set;}
   }
}