using pokemonAPI.DTOs.CategoryDtos;
using pokemonAPI.DTOs.PokemonDtos;
using pokemonAPI.DTOs.CountryDtos;
using pokemonAPI.DTOs.ReviewerDtos;
using pokemonAPI.DTOs.OwnerDtos;
using pokemonAPI.DTOs.ReviewDtos;
using AutoMapper;
using pokemonAPI.Models;

namespace pokemonAPI.Helpers
{
   public class MappingProfile : Profile
   {
      public MappingProfile()
      {
         //! Pokemon Resource
         CreateMap<Pokemon, ReadPokemon>().ReverseMap();
         CreateMap<CreatePokemon, Pokemon>().ReverseMap();
         //! Category Resource
         CreateMap<Category, ReadCategory>().ReverseMap();
         CreateMap<CreateCategory, Category>().ReverseMap();
         //! Country Resource
         CreateMap<Country, ReadCountry>().ReverseMap();
         CreateMap<CreateCountry, Country>().ReverseMap();
         //! Owner Resource
         CreateMap<Owner, ReadOwner>().ReverseMap();
         CreateMap<CreateOwner, Owner>().ReverseMap();
         //! Reviewer Resource
         CreateMap<Reviewer, ReadReviewer>();
         CreateMap<ReadReviewer, Reviewer>();
         CreateMap<CreateReviewer, Reviewer>();
         CreateMap<Reviewer, CreateReviewer>();
         //! Review Resource
         CreateMap<Review, ReadReview>();
         CreateMap<ReadReview, Review>();
         CreateMap<CreateReview, Review>();
         CreateMap<Review, CreateReview>();

      }
   }
}