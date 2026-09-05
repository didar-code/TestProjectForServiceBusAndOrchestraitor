using Microsoft.Extensions.DependencyInjection;
using SharedSubSystem;
using SharedSubSystem.Events;
using SharedSubSystem.Generics;

namespace ServiceBus
{
    public class ServiceBus : IServiceBus
    {
        private readonly IServiceProvider _serviceProvider;

        public ServiceBus(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<IEnumerable<Event>> SendCommandAsync<TCommand>(TCommand command)
            where TCommand : ICommand
        {
            var handler = _serviceProvider.GetRequiredService<ICommandHandler<TCommand>>();

            return await handler.HandleAsync(command);
        }

        public async Task<TResult> SendQueryAsync<TQuery, TResult>(TQuery query)
            where TQuery : IQuery<TResult>
        {
            var handler =_serviceProvider.GetRequiredService<IQueryHandler<TQuery, TResult>>();

            return await handler.HandleAsync(query);
        }
    }
}