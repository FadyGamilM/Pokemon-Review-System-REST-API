using pokemonAPI.DTOs.PokemonDtos;
using AutoMapper;
using pokemonAPI.Models;

namespace PokemonAPI.Helpers
{
   public class MappingProfile : Profile
   {
      public MappingProfile()
      {
         CreateMap<Pokemon, ReadPokemon>().ReverseMap();
      }
   }
}