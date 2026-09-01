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
        Task<IEnumerable<Event>> SendCommandAsync<TCommand>(TCommand command)
            where TCommand : ICommand;
        Task<TResult> SendQueryAsync<TQuery, TResult>(TQuery query)
           where TQuery : IQuery<TResult>;
    }
}
