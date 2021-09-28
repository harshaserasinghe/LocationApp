using FluentValidation;
using Location.Core.Dtos;

namespace Location.Core.Validations
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
