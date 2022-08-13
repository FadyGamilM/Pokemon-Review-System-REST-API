namespace pokemonAPI.DTOs.ReviewDtos
{
   public class ReadReview
   {
      [Key]
      public int Id {get; set;}
      public string Title { get; set; }
      public string Text { get; set; }
      public float Rating {get; set;}     

   }
}