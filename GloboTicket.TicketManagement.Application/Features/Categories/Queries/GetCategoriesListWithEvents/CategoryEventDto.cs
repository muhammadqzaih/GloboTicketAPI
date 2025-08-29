using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GloboTicket.TicketManagement.Application.Features.Categories.Queries.GetCategoriesListWithEvents
{
    public class CategoryEventDto
    {
        public Guid EventId { get; set; }
        public string EventName { get; set; } = string.Empty;
        public int EventPrice { get; set; }
        public string Artist { get; set; } = string.Empty;
        public DateTime Date {  get; set; } 
        public Guid CategortId { get; set; }
    }
}
