namespace pokemonAPI.Models
{
   public class Owner
   {
      [Key]
      public int Id { get; set; }
      public string Name { get; set; }
      public string Gym {get; set;}

      //! One-To-Many relation with Country [the N-side]
      public int CountryId {get; set;}
      public Country Country {get; set;}
   }
}