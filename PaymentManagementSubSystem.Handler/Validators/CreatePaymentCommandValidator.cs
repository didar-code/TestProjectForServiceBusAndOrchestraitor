using FluentValidation;
using PaymentManagementSubSystem.DTOs.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentManagementSubSystem.Handler.Validators
{
    public class CreatePaymentCommandValidator : AbstractValidator<CreatePaymentCommand>
    {
        public CreatePaymentCommandValidator()
        {
            RuleFor(x => x.OrderId).GreaterThan(0).WithMessage("OrderId must be a valid positive number.");
            RuleFor(x => x.Amount).GreaterThan(0).WithMessage("Amount must be greater than zero.");
            RuleFor(x => x.PaymentMethod).NotEmpty().WithMessage("Payment method is required.");
        }
    }
}
