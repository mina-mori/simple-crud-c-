using Application.ViewModels;
using DomainEntities.Entities;
using FluentValidation;

namespace Application.Validations
{

    public class UserCommandValidator : AbstractValidator<AddEditUserViewModel>
    {
        public UserCommandValidator()
        {
            RuleFor(u => u.FirstName)
                .NotEmpty().WithMessage("The first name is required.");

            RuleFor(u => u.LastName)
                .NotEmpty().WithMessage("The last name is required.");

            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("The email is required.")
                .Matches("^[^\\s@]+@[^\\s@]+\\.[^\\s@]+$").WithMessage("The email format is incorrect.");
            RuleFor(u => u.Phone)
                .NotEmpty().WithMessage("The phone is required.")
                .Matches("^(?:\\+\\d{1,3})?\\d{10}$").WithMessage("The phone format is incorrect.");
        }
    }
}
