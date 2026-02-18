using FluentValidation;
using System;

namespace MiniOrderManagement.Application.Commands.Orders.CreateOrder
{
    public class CreateOrderValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderValidator()
        {
            RuleFor(x => x.OrderDate)
                .NotEmpty().WithMessage("Order date is required")
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Order date cannot be in the future");

            RuleFor(x => x.TotalAmount)
                .GreaterThan(0).WithMessage("Total amount must be greater than zero");

            RuleFor(x => x.CustomerId)
                .GreaterThan(0).WithMessage("Customer ID must be valid");
        }
    }
}
