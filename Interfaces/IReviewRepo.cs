using pokemonAPI.Models;
namespace pokemonAPI.Interfaces
{
   public interface IReviewRepo
   {
      // get all reviews 
      Task<IEnumerable<Review>> GetReviews();

      // get review by id
      Task<Review> GetReview(int reviewID);
      
      // get the reviewer of this review given the review id
      Task<Reviewer> GetReviewer(int reviewID);
   }
}