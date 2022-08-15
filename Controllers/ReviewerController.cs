using Microsoft.AspNetCore.Mvc;
using pokemonAPI.Interfaces;
using pokemonAPI.Models;
using AutoMapper;
using pokemonAPI.DTOs.ReviewerDtos;
namespace pokemonAPI.Controllers
{
   [ApiController]
   [Route("api/reviewers")]
   public class ReviewerController : ControllerBase
   {
      private readonly IReviewerRepo _reviewerRepo;
      private readonly IMapper _mapper;
      public ReviewerController(IReviewerRepo reviewerRepo, IMapper mapper)
      {
         this._mapper = mapper;
         this._reviewerRepo = reviewerRepo;
      }
      [HttpGet("")]
      public async Task<IActionResult> GetReviewers ()
      {
         var reveiwers = await this._reviewerRepo.GetReviewers();
         var reviewersDto = this._mapper.Map<IEnumerable<ReadReviewer>>(reveiwers);
         return Ok(reviewersDto);
      }
      [HttpGet("{Id:int}")]
      public async Task<IActionResult> GetReviewer([FromRoute]int Id)
      {
         var reviewer = await this._reviewerRepo.GetReveiewer(Id);
         var reviewerDto = this._mapper.Map<ReadReviewer>(reviewer);
         return Ok(reviewerDto);
      }      
   }
}