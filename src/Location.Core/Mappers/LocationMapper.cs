using AutoMapper;
using Location.Core.Dtos;

namespace Location.Core.Mappers
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
