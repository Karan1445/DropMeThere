using dropmethereapi.Models;
using FluentValidation;

namespace dropmethereapi.ValidationLogic
{
    public class ForRegistrationDemo:AbstractValidator<userRegsitermodel>
    {
        public ForRegistrationDemo()
        {
            RuleFor(user => user.UserName)
                .NotEmpty().WithMessage("Username is required.")
                .MaximumLength(150).WithMessage("Username cannot exceed 150 characters.");

            RuleFor(user => user.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format. Please provide a valid email address (e.g., example@example.com).")
                .MaximumLength(200).WithMessage("Email cannot exceed 200 characters.");

            RuleFor(user => user.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Invalid phone number. Ensure it includes country code (e.g., +123456789).")
                .MaximumLength(15).WithMessage("Phone number cannot exceed 15 characters.");

            RuleFor(user => user.PassWord)
                .NotEmpty().WithMessage("Password is required.")
                .MaximumLength(500).WithMessage("Password cannot exceed 500 characters.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches("[0-9]").WithMessage("Password must contain at least one numeric character.")
                .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character (e.g., @, #, $, etc.).");
            RuleFor(user => user.ConfirmPassWord)
            .NotEmpty().WithMessage("Confirm Password is required.")
            .Equal(user => user.PassWord).WithMessage("Confirm Password must match Password.");

            RuleFor(user => user.IsDriver)
                .NotEmpty().WithMessage("Driver status is required.")
                .Must(value => value == "Yes" || value == "No")
                .WithMessage("Driver status must be either 'true' or 'false'.");

        
        }
    }
}
