using pokemonAPI.Models;
namespace pokemonAPI.Interfaces
{
   public interface IReviewerRepo
   {

      // get all reviewers 
      Task<IEnumerable<Reviewer>> GetReviewers();
      // get reviewer by id 
      Task<Reviewer> GetReveiewer(int reviewerID);
      // get all reviews of specific reviewer 
      Task<IEnumerable<Review>> GetReviews(int reviewerID);
      // get all the pokemons that this reviewer wrote reviews about
      Task<IEnumerable<Pokemon>> GetPokemons(int reviewerID);
      // create new reviewer 
      Task<bool> CreateReviewer(Reviewer reviewer);
      // save changes
      bool SaveChanges();
   }
}