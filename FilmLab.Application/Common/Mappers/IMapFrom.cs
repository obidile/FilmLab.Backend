using AutoMapper;

namespace FilmLab.Application.Mappers;

public interface IMapFrom<T>
{
    void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
}
