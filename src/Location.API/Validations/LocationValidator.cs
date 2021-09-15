using FluentValidation;
using Location.Service.Dtos;

namespace Location.API.Validations
{
    public class LocationValidator : AbstractValidator<LocationCreateDto>
    {
        public LocationValidator()
        {
            RuleFor(location => location.VehicleId).NotEmpty();
        }
    }
}
