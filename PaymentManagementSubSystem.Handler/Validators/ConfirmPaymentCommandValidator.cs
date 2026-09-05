using FluentValidation;
using PaymentManagementSubSystem.DTOs.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentManagementSubSystem.Handler.Validators
{
    public class ConfirmPaymentCommandValidator : AbstractValidator<ConfirmPaymentCommand>
    {
        public ConfirmPaymentCommandValidator()
        {
            RuleFor(x => x.PaymentId).GreaterThan(0).WithMessage("PaymentId must be a valid positive number.");
        }
    }
}
