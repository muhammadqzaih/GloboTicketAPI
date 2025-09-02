using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GloboTicket.TicketManagement.Application.Features.Events.Queries.GetEventDetails
{
    public class EventDetailsVm
    {
        public Guid EventId { get; set; }
        public string EventName { get; set; } = string.Empty;
        public int EventPrice { get; set; }
        public string Artist { get; set; } = string.Empty;
        public DateTime Date {  get; set; }
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public Guid CategoryId { get; set; }
        public CategoryDto Category { get; set; } = default!;
    }
}
