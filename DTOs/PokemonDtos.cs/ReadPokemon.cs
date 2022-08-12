namespace pokemonAPI.DTOs.PokemonDtos
{
   public class ReadPokemon
   {
      [Key]
      public int Id { get; set; }
      public string Name { get; set; }
      public DateTime Birthdate { get; set; }
   }
}