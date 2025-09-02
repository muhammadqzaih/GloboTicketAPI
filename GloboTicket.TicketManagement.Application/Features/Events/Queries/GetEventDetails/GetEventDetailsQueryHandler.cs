using AutoMapper;
using GloboTicket.TicketManagement.Application.Contracts.Persistence;
using GloboTicket.TicketManagement.Application.Exceptions; 
using GloboTicket.TicketManagement.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace GloboTicket.TicketManagement.Application.Features.Events.Queries.GetEventDetails
{
    public class GetEventDetailsQueryHandler : IRequestHandler<GetEventDetailsQuery, EventDetailsVm>
    {
        private readonly IAsyncRepository<Event> _eventRepository;
        private readonly IAsyncRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;

        public GetEventDetailsQueryHandler(
            IAsyncRepository<Event> eventRepository,
            IAsyncRepository<Category> categoryRepository,
            IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _eventRepository = eventRepository;
            _mapper = mapper;
        }

        public async Task<EventDetailsVm> Handle(GetEventDetailsQuery request,
            CancellationToken cancellationToken)
        {
            var @event = await _eventRepository.GetByIdAsync(request.Id);
            if (@event == null)
                throw new NotFoundException($"Event not found.");

            var eventDetailsDto = _mapper.Map<EventDetailsVm>(@event);

            var eventCategory = await _categoryRepository.GetByIdAsync(@event.CategoryId);
            if (eventCategory == null)
                throw new NotFoundException($"Category not found.");

            eventDetailsDto.Category = _mapper.Map<CategoryDto>(eventCategory);

            return eventDetailsDto;
        }
    }
}
