﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GloboTicket.TicketManagement.Application.Features.Events.Queries.GetEventDetails
{
    public class CategoryDto
    {
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;    
    }
}
