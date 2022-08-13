namespace pokemonAPI.DTOs.OwnerDtos
{
   public class ReadOwner
   {
      [Key]
      public int Id { get; set; }
      public string Name { get; set; }
      public string Gym {get; set;}
   }
}