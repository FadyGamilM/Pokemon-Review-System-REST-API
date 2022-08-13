namespace pokemonAPI.DTOs.CategoryDtos
{
   public class ReadCategory
   {
         [Key]
         public int Id { get; set; }
         public string Name { get; set; }
   }
}