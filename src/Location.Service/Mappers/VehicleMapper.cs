using AutoMapper;
using Location.Service.Dtos;
using Location.Service.Entities;

namespace Location.Service.Mappers
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
