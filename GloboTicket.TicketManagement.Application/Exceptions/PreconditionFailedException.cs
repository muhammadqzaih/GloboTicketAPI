using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GloboTicket.TicketManagement.Application.Exceptions
{
    public class PreconditionFailedException : Exception
    {
        public PreconditionFailedException(string message) : base(message) { }
        public PreconditionFailedException(string message, Exception innerException)
            : base(message) { }
    }
}
