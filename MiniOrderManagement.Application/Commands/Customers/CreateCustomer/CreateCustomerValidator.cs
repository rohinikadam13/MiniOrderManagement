using FluentValidation;

namespace MiniOrderManagement.Application.Commands.Customers.CreateCustomer
{
    public class CreateCustomerValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Customer name is required")
                .MinimumLength(2).WithMessage("Customer name must be at least 2 characters");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address is required");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Phone number is required")
                .Matches(@"^\d{10}$|^\d{3}-\d{3}-\d{4}$").WithMessage("Phone must be valid format");
        }
    }
}
