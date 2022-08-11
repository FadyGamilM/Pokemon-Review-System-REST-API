namespace pokemonAPI.Models
{
   public class pokemon
   {
      [Key]
      public int Id { get; set; }
      public string Name { get; set; }
      public DateTime Birthdate { get; set; }
   }
}