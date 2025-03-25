using dropmethereapi.Models;
using FluentValidation;

namespace dropmethereapi.ValidationLogic
{
    public class AdminUserValidator : AbstractValidator<AdminUserModel>
    {
        public AdminUserValidator()
        {
            RuleFor(x => x.AdminName)
                .NotEmpty().WithMessage("Admin name is required.")
                .MaximumLength(500).WithMessage("Admin name cannot exceed 500 characters.");

            RuleFor(x => x.RoleName)
                .NotEmpty().WithMessage("Role name is required.")
                .Must(role => role == "Admin" || role == "Reader" || role == "Updater")
                .WithMessage("Role name must be 'Admin', 'Reader', or 'Updater'.");

            RuleFor(x => x.CreatedBy)
                .NotEmpty().WithMessage("Created by is required.")
                .MaximumLength(500).WithMessage("Created by cannot exceed 500 characters.");

            RuleFor(x => x.CreatedOn)
                .NotEmpty().WithMessage("Created on is required.")
                .MaximumLength(500).WithMessage("Created on cannot exceed 500 characters.");
        }
    }
}
