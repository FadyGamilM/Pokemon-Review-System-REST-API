namespace pokemonAPI.Models
{
   public class Country
   {
      [Key]
      public int Id { get; set; }
      public string Name { get; set; }
      //! One-To-Many relation with Owner [the 1-side]
      public ICollection<Owner> Owners {get; set;} 
   }
}