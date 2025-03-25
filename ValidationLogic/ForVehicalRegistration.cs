using dropmethereapi.Models;
using dropmethereapi.Repos.VehicalRegistration;
using FluentValidation;

namespace dropmethereapi.ValidationLogic
{
    public class ForVehicalRegistration:AbstractValidator<VehicalRegistrationModel>
    {
        public ForVehicalRegistration() {
            RuleFor(x => x.UserID)
                  .GreaterThan(0)
                  .WithMessage("User ID must be greater than 0.");

            // Validate VehicalNumber (Indian vehicle number format: e.g., GJ01AB1234)
            RuleFor(x => x.VehicalNumber)
                .NotEmpty()
                .WithMessage("Vehicle number is required.")
                .Matches(@"^[A-Z]{2}[0-9]{2}[A-Z]{1,2}[0-9]{1,4}$")
                .WithMessage("Invalid vehicle number format. Example: GJ01AB1234.");

            // Validate HelperDL (Indian driving license format: e.g., DL-0420110149646)
            RuleFor(x => x.HelperDL)
                .NotEmpty()
                .WithMessage("Helper driving license is required.")
                .Matches(@"^[A-Z]{2}[0-9]{2}[0-9]{11}$")
                .WithMessage("Invalid driving license format. Example: DL0420110149646.");

            // Validate HelperLocality (basic check for non-empty value, max 300 characters)
            RuleFor(x => x.HelperLocality)
                .NotEmpty()
                .WithMessage("Helper locality is required.")
                .MaximumLength(300)
                .WithMessage("Helper locality must not exceed 300 characters.");

            // Validate VehicalName (basic check for non-empty value, max 350 characters)
            RuleFor(x => x.VehicalName)
                .NotEmpty()
                .WithMessage("Vehicle name is required.")
                .MaximumLength(350)
                .WithMessage("Vehicle name must not exceed 350 characters.");

            // Validate VehicalType (e.g., Car, Truck, Bike)
            RuleFor(x => x.VehicalType)
                .NotEmpty()
                .WithMessage("Vehicle type is required.")
                .Must(type => new[] { "Car", "Truck", "Bike", "Bus" }.Contains(type))
                .WithMessage("Vehicle type must be one of the following: Car, Truck, Bike, Bus.");

            // Validate VehicalColor (basic check for non-empty value, max 350 characters)
            RuleFor(x => x.VehicalColor)
                .NotEmpty()
                .WithMessage("Vehicle color is required.")
                .MaximumLength(350)
                .WithMessage("Vehicle color must not exceed 350 characters.");
        }
    
    }
}
