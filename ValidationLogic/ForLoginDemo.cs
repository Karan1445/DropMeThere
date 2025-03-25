using dropmethereapi.Models;
using FluentValidation;

namespace dropmethereapi.ValidationLogic
{
    public class ForLoginDemo: AbstractValidator<userDumomodel>
    {
        public ForLoginDemo()
        {
            RuleFor(user => user.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format. Please provide a valid email address (e.g., example@example.com).");

            RuleFor(user => user.PassWord)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches("[0-9]").WithMessage("Password must contain at least one numeric character.")
                .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character (e.g., @, #, $, etc.).");
        }
    }
}
