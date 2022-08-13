using pokemonAPI.DTOs.CategoryDtos;
using pokemonAPI.DTOs.PokemonDtos;
using pokemonAPI.DTOs.CountryDtos;
using pokemonAPI.DTOs.OwnerDtos;
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
         //! Category Resource
         CreateMap<Category, ReadCategory>().ReverseMap();
         //! Country Resource
         CreateMap<Country, ReadCountry>().ReverseMap();
         //! Owner Resource
         CreateMap<Owner, ReadOwner>().ReverseMap();
      }
   }
}