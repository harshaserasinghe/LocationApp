using AutoMapper;
using Location.Core.Dtos;
using Location.Core.Entities;

namespace Location.Core.Mappers
{
    public class VehicleMapper : Profile
    {
        public VehicleMapper()
        {
            CreateMap<Vehicle, VehicleDto>();
            CreateMap<VehicleCreateDto, Vehicle>();
        }
    }
}
