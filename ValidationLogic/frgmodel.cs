using dropmethereapi.Models;
using FluentValidation;

namespace dropmethereapi.ValidationLogic
{
    public class frgmodel: AbstractValidator<ForgetModel>
    {
        public frgmodel()
        {
            // Email validation
            RuleFor(user => user.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            // Phone Number validation
            RuleFor(user => user.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^[0-9]{10}$").WithMessage("Phone number must be exactly 10 digits.");

            // Password validation
            RuleFor(user => user.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
                .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches(@"[0-9]").WithMessage("Password must contain at least one number.")
                .Matches(@"[\W]").WithMessage("Password must contain at least one special character.");

            // Re_Password validation (must match Password)
            RuleFor(user => user.Re_Password)
                .Equal(user => user.Password).WithMessage("Passwords do not match.");
        }
    }
}
