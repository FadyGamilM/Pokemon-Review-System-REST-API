namespace pokemonAPI.DTOs.CountryDtos
{
   public class ReadCountry
   {
      [Key]
      public int Id { get; set; }
      public string Name { get; set; }
   }
}