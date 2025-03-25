using dropmethereapi.Models;
using FluentValidation;
using System.Text.RegularExpressions;

namespace dropmethereapi.ValidationLogic
{
    public class ForSeekerRequestHandler: AbstractValidator<SeekerRequestHandlerModel>
    {
        public ForSeekerRequestHandler() {
           

            RuleFor(x => x.UserID)
                .GreaterThan(0)
                .WithMessage("User ID must be greater than 0.").NotEmpty().WithMessage("UserID is required");

            RuleFor(x => x.UserName)
                .NotEmpty()
                .WithMessage("User name is required.")
                .MaximumLength(150)
                .WithMessage("User name cannot exceed 150 characters.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .WithMessage("Phone number is required.")
                .Matches(@"^\+?[1-9]\d{1,14}$")
                .WithMessage("Phone number must be in a valid international format (e.g., +123456789).");

            RuleFor(x => x.SeekerCity)
                .NotEmpty()
                .WithMessage("Seeker city is required.")
                .MaximumLength(250)
                .WithMessage("Seeker city cannot exceed 250 characters.");

            RuleFor(x => x.SeekerState)
                .NotEmpty()
                .WithMessage("Seeker state is required.")
                .MaximumLength(250)
                .WithMessage("Seeker state cannot exceed 250 characters.");

            RuleFor(x => x.SeekerArea)
                .NotEmpty()
                .WithMessage("Seeker area is required.")
                .MaximumLength(250)
                .WithMessage("Seeker area cannot exceed 250 characters.");

            RuleFor(x => x.CurrentLocationLatLong)
                .NotEmpty()
                .WithMessage("Current location is required.")
                .Must(IsValidLatLong)
                .WithMessage("Current location must be in valid latitude,longitude format.");

            RuleFor(x => x.RideStartPointLatLong)
                .NotEmpty()
                .WithMessage("Ride start point is required.")
                .Must(IsValidLatLong)
                .WithMessage("Ride start point must be in valid latitude,longitude format.");

            RuleFor(x => x.RideEndPointLatLong)
                .NotEmpty()
                .WithMessage("Ride end point is required.")
                .Must(IsValidLatLong)
                .WithMessage("Ride end point must be in valid latitude,longitude format.");

            RuleFor(x => x.ReqTime)
                .NotEmpty()
                .WithMessage("Request time is required.")
                .Must(reqTime => reqTime <= DateTime.Now)
                .WithMessage("Request time cannot be in the future.");
        }

        private bool IsValidLatLong(string latLong)
        {
            var regex = new Regex(@"^[-+]?([1-8]?\d(\.\d+)?|90(\.0+)?),\s*[-+]?((1[0-7]\d|\d{1,2})(\.\d+)?|180(\.0+)?)$");
            return regex.IsMatch(latLong);
        }
    }
}

