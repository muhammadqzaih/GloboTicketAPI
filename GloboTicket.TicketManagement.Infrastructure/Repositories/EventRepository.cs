using GloboTicket.TicketManagement.Application.Contracts.Persistence;
using GloboTicket.TicketManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GloboTicket.TicketManagement.Infrastructure.Repositories
{
    public class EventRepository : BaseRepository<Event>, IEventRepository
    {
        public EventRepository(GloboTicketDbContext context) : base(context)
        {
        }
        public Task<bool> IsEventNameAndDateUnique(string eventName, DateTime eventDate)
        {
            var matches = _context.Events
                .Any(e => e.EventName.Equals(eventName) && e.Date.Equals(eventDate));

            return Task.FromResult(matches);
        }
    }
}
