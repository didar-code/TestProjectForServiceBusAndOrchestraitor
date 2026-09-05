using FluentValidation;
using PaymentManagementSubSystem.DTOs.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentManagementSubSystem.Handler.Validators
{
    public class FailPaymentCommandValidator : AbstractValidator<FailPaymentCommand>
    {
        public FailPaymentCommandValidator()
        {
            RuleFor(x => x.PaymentId).GreaterThan(0).WithMessage("PaymentId must be a valid positive number.");
        }
    }
}
