using dropmethereapi.Models;
using FluentValidation;
using FluentValidation.Validators;

namespace dropmethereapi.ValidationLogic
{
    public class ForHelperSideViewRides : AbstractValidator<HelperSideViewRidesModel>
    {
        public ForHelperSideViewRides() {
            RuleFor(x => x.SeekerUserID).GreaterThan(0).WithMessage("SeekerUserID must be greater than 0");
            RuleFor(x => x.HelperUserID).GreaterThan(0).WithMessage("HelperUserID must be greater than 0");
            RuleFor(x => x.RequestID).GreaterThan(0).WithMessage("RequestID must be greater than 0");
            RuleFor(x => x.HelpersCurrentLocationLatLong).NotEmpty().WithMessage("Location is required");
            RuleFor(x => x.HelperRechabletimetoStartPoint).NotEmpty().WithMessage("Reachable time is required");
            RuleFor(x => x.HelperDistanceFromStartPoint).NotEmpty().WithMessage("Distance is required");

        }
    }
}
