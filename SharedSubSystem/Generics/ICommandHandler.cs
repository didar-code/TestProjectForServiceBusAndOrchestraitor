using SharedSubSystem.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SharedSubSystem.Generics
{
    public interface ICommandHandler<TCommand>where TCommand : ICommand
    {
        Task<IEnumerable<Event>> HandleAsync(TCommand command);
    }
}
