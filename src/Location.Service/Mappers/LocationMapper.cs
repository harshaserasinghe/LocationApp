using AutoMapper;
using Location.Service.Dtos;

namespace Location.Service.Mappers
{
    public class LocationMapper : Profile
    {
        public LocationMapper()
        {
            CreateMap<Entities.Location, LocationDto>();
            CreateMap<LocationCreateDto, Entities.Location>();
        }
    }
}
