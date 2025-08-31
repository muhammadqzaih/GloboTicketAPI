using AutoMapper;
using GloboTicket.TicketManagement.Application.Contracts.Persistence;
using GloboTicket.TicketManagement.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GloboTicket.TicketManagement.Application.Features.Events.Queries.GetEventsList
{
    public class GetEventListQueryHandler : IRequestHandler<GetEventListQuery,
        List<EventListVm>>
    {
        private readonly IAsyncRepository<Event> _eventRepoistory;
        private readonly IMapper _mapper;

        public GetEventListQueryHandler(IAsyncRepository<Event> eventRepoistory, IMapper mapper)
        {
            _eventRepoistory = eventRepoistory;
            _mapper = mapper;
        }

        public async Task<List<EventListVm>> Handle(GetEventListQuery request, 
            CancellationToken cancellationToken)
        {
            var allEvents = (await _eventRepoistory.ListAllAsync()).OrderBy(e => e.Date);
            return _mapper.Map<List<EventListVm>>(allEvents);
        }
    }
}
