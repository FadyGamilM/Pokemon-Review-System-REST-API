namespace pokemonAPI.Models
{
   public class Owner
   {
      [Key]
      public int Id { get; set; }
      public string Name { get; set; }
      public string Gym {get; set;}
   }
}