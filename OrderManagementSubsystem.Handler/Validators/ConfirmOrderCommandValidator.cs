using FluentValidation;
using OrderManagementSubsystem.DTOs.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSubsystem.Handler.Validators
{
    public class ConfirmOrderCommandValidator : AbstractValidator<ConfirmOrderCommand>
    {
        public ConfirmOrderCommandValidator()
        {
            RuleFor(x => x.OrderId).GreaterThan(0).WithMessage("OrderId must be a valid positive number.");
            RuleFor(x => x.PaymentId).GreaterThan(0).WithMessage("PaymentId must be a valid positive number.");
        }
    }
}
