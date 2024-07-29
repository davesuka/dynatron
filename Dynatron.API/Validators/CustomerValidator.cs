using Dynatron.API.DTO.Request;
using FluentValidation;

namespace Dynatron.API.Validators
{
    public class CustomerValidator : AbstractValidator<CustomerRequestDTO>
    {
        public CustomerValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("The First Name cannot be empty")
                .NotNull().WithMessage("The First Name cannot be null");

            RuleFor(x => x.LastName)
                .NotNull().WithMessage("The Last Name cannot be null")
                .NotEmpty().WithMessage("The Last Name cannot be empty");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("The Email cannot be empty")
                .NotNull().WithMessage("The Email cannot be null");
        }
    }
}
