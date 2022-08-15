using Microsoft.AspNetCore.Mvc;
using pokemonAPI.Interfaces;
using pokemonAPI.DTOs.ReviewDtos;
using pokemonAPI.DTOs.ReviewerDtos;
using AutoMapper;
using pokemonAPI.Models;

namespace pokemonAPI.Controllers
{
   [ApiController]
   [Route("api/reviews")]
   public class ReviewController : ControllerBase
   {
      // DI pattern
      private readonly IMapper _mapper;
      private readonly IReviewRepo _reviewRepo;
      private readonly IPokemonRepo _pokemonRepo;
      private readonly IReviewerRepo _reviewerRepo;
      public ReviewController(IMapper mapper, IReviewRepo reviewRepo,
                                         IPokemonRepo pokemonRepo, IReviewerRepo reviewerRepo)
      {
         this._mapper = mapper;
         this._reviewRepo = reviewRepo;
         this._pokemonRepo = pokemonRepo;
         this._reviewerRepo = reviewerRepo;
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

      [HttpPost("")]
      [ProducesResponseType(204)]
      [ProducesResponseType(400)]
      public async Task<IActionResult> CreateReview([FromBody] CreateReview reviewDto, [FromQuery] int pokemonID, [FromQuery] int reviewerID)
      {
         if (reviewDto == null){
            return BadRequest(ModelState);
         }
         if (!ModelState.IsValid){
            return BadRequest(ModelState);
         }
         var review = this._mapper.Map<Review>(reviewDto);
         review.Pokemon = await this._pokemonRepo.GetPokemon(pokemonID);
         review.Reviewer = await this._reviewerRepo.GetReveiewer(reviewerID);
         var creationResult = await this._reviewRepo.CreateReview(review);
         if(creationResult == true){
            return Ok("Succeed");
         }else{
            ModelState.AddModelError("", "Error while saving the new review in the DB");
            return StatusCode(500, ModelState);
         }
      }
   }
}