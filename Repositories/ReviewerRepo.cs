using Microsoft.EntityFrameworkCore;
using pokemonAPI.Data;
using pokemonAPI.Interfaces;
using pokemonAPI.Models;

namespace pokemonAPI.Repositories
{
   public class ReviewerRepo : IReviewerRepo
   {
      // DI pattern
      private readonly AppDbContext _context;
      public ReviewerRepo(AppDbContext context)
      {
         this._context = context;
      }
      // get all reviewers 
      public async Task<IEnumerable<Reviewer>> GetReviewers()
      {
         var reviewers = await this._context.Reviewers.AsNoTracking().ToListAsync();
         return reviewers;
      }
      // get reviewer by id 
      public async Task<Reviewer> GetReveiewer(int reviewerID)
      {
         var reviewer = await this._context.Reviewers.FindAsync(reviewerID);
         return reviewer;
      }
      // get all reviews of specific reviewer 
      public async Task<IEnumerable<Review>> GetReviews(int reviewerID)
      {
         var reviewer = await this._context.Reviewers.FindAsync(reviewerID);
         await this._context.Entry(reviewer).Collection(R => R.Reviews).LoadAsync();
         return reviewer.Reviews;
      }
      // get all the pokemons that this reviewer wrote reviews about
      public async Task<IEnumerable<Pokemon>> GetPokemons(int reviewerID)
      {
         var reviewer = await this._context.Reviewers.FindAsync(reviewerID);
         await this._context.Entry(reviewer).Collection(R => R.Reviews).LoadAsync();
         var pokemons = new List<Pokemon>();
         foreach (var review in reviewer.Reviews)
         {
            pokemons.Add(review.Pokemon);
         }
         return pokemons;
      }
      // create reviewer 
      public async Task<bool> CreateReviewer(Reviewer reviewer)
      {
         await this._context.Reviewers.AddAsync(reviewer);
         return this.SaveChanges();
      }
      // save changes
      public bool SaveChanges()
      {
         return (bool)(this._context.SaveChanges() > 0);
      }
   }
}