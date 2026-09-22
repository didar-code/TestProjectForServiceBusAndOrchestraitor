using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedSubSystem.Events
{
    public abstract class Event : IEvent
    {
        public Guid EventId { get; } = Guid.NewGuid();

        public DateTime OccurredOn { get; } = DateTime.UtcNow;
    }
}
