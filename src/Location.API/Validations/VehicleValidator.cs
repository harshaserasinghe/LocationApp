using FluentValidation;
using Location.Service.Dtos;

namespace Location.API.Validations
{
    public class VehicleValidator : AbstractValidator<VehicleCreateDto>
    {
        public VehicleValidator()
        {
            RuleFor(vehicle => vehicle.VehicleId).NotEmpty();
            RuleFor(vehicle => vehicle.LicenceNo).NotEmpty();
        }
    }
}
