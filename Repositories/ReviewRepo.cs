using pokemonAPI.Data;
using pokemonAPI.Interfaces;
using pokemonAPI.Models;
using Microsoft.EntityFrameworkCore;
namespace pokemonAPI.Repositories
{
   public class ReviewRepo : IReviewRepo
   {
      //* the DbContext service 
      private readonly AppDbContext _context;
      public ReviewRepo(AppDbContext context)
      {
         this._context = context;
      }
      public async Task<Review> GetReview(int reviewID)
      {
         var review = await this._context.Reviews.FindAsync(reviewID);
         return review;
      }

      public async Task<Reviewer> GetReviewer(int reviewID)
      {
         var review = await this._context.Reviews.FindAsync(reviewID);
         this._context.Entry(review).Reference(R => R.Reviewer).LoadAsync();
         return review.Reviewer;
      }

      public async Task<IEnumerable<Review>> GetReviews()
      {
         var reviews = await this._context.Reviews.AsNoTracking().ToListAsync();
         return reviews;      }
   }
}