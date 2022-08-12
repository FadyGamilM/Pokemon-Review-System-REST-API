
namespace pokemonAPI.Models
{
   public class Reviewer
   {
      [Key]
      public int Id {get; set;}
      public string FirstName {get; set;}

      public string LastName {get ; set;}

      //! One-To-Many relation with the Review [the 1-Side]
      public ICollection<Review> Reviews {get; set;}
   }
}