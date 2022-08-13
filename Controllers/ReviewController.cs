using Microsoft.AspNetCore.Mvc;
using pokemonAPI.Interfaces;
using pokemonAPI.DTOs.ReviewDtos;
using pokemonAPI.DTOs.ReviewerDtos;
using AutoMapper;
namespace pokemonAPI.Controllers
{
   [ApiController]
   [Route("api/reviews")]
   public class ReviewController : ControllerBase
   {
      // DI pattern
      private readonly IMapper _mapper;
      private readonly IReviewRepo _reviewRepo;
      public ReviewController(IMapper mapper, IReviewRepo reviewRepo)
      {
         this._mapper = mapper;
         this._reviewRepo = reviewRepo;
      }
      // get all reviews
      [HttpGet("")]
      public async Task<IActionResult> GetReviews ()
      {
         var reveiws = await this._reviewRepo.GetReviews();
         return Ok(this._mapper.Map<IEnumerable<ReadReview>>(reveiws));
      }
      // get review by id 
      [HttpGet("{reviewID:int}")]
      public async Task<IActionResult> GetReview(int reviewID)
      {
         var review = await this._reviewRepo.GetReview(reviewID);
         if (review == null)
         {
            return NotFound();
         }
         return Ok(this._mapper.Map<ReadReview>(review));
      }
      // get reviewer of this review
      [HttpGet("{reviewID}/reviewer")]
      public async Task<IActionResult> GetReviewer(int reviewID)
      {
         var reviewer = await this._reviewRepo.GetReviewer(reviewID);
         return Ok(this._mapper.Map<ReadReviewer>(reviewer));
      }

   }
}