using FluentValidation;
using SharedSubSystem.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedSubSystem.Generics
{
    public class ValidatingCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand>
       where TCommand : ICommand
    {
        private readonly ICommandHandler<TCommand> _inner;
        private readonly IValidator<TCommand>? _validator;

        public ValidatingCommandHandlerDecorator(ICommandHandler<TCommand> inner,
            IValidator<TCommand>? validator = null)
        {
            _inner = inner;
            _validator = validator;
        }

        public async Task<IEnumerable<Event>> HandleAsync(TCommand command)
        {
            if (_validator != null)
            {
                var result = await _validator.ValidateAsync(command);
                if (!result.IsValid)
                {
                    var errors = string.Join("; ", result.Errors.Select(e => e.ErrorMessage));
                    throw new ValidationException(errors);
                }
            }

            return await _inner.HandleAsync(command);
        }
    }
}
