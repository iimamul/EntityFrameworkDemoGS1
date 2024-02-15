using AutoMapper;
using EntityFrameworkDemoGS1.DTOs;
using EntityFrameworkDemoGS1.Entities;

namespace EntityFrameworkDemoGS1.Utilities
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<GenreCreationDTO, Genre>();
            CreateMap<ActorCreationDTO, Actor>();
            CreateMap<MovieCreationDTO, Movie>()
                .ForMember(ent => ent.Genres,
                dto => dto.MapFrom(field => field.Genres.Select(id => new Genre() { Id = id })));

            CreateMap<MovieActorCreationDTO, MovieActor>();
        }
    }
}
