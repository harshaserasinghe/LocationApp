using FluentValidation;
using Location.Core.Dtos;

namespace Location.Core.Validations
{
    public class LocationValidator : AbstractValidator<LocationCreateDto>
    {
        public LocationValidator()
        {
            RuleFor(location => location.VehicleId).NotEmpty();
        }
    }
}
