using SharedSubSystem.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedSubSystem;

namespace ServiceBus
{
    public interface IServiceBus
    {
        Task<IEnumerable<Event>> SendAsync<TCommand>(TCommand command)
            where TCommand : ICommand;
        Task<TResult> QueryAsync<TQuery, TResult>(TQuery query)
           where TQuery : IQuery<TResult>;
    }
}
