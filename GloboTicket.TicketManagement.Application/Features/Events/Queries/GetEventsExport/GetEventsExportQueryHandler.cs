using AutoMapper;
using GloboTicket.TicketManagement.Application.Contracts.Infrastructure;
using GloboTicket.TicketManagement.Application.Contracts.Persistence;
using GloboTicket.TicketManagement.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GloboTicket.TicketManagement.Application.Features.Events.Queries.GetEventsExport
{
    internal class GetEventsExportQueryHandler : 
        IRequestHandler<GetEventExportQuery, EventExportFileVm>
    {
        private readonly IAsyncRepository<Event> _eventRepository;
        private readonly IMapper _mapper;
        private readonly ICsvExporter _csvExporter;

        public GetEventsExportQueryHandler(IAsyncRepository<Event> eventRepository,
            IMapper mapper, ICsvExporter csvExporter)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
            _csvExporter = csvExporter;
        }
        public async Task<EventExportFileVm> Handle(
            GetEventExportQuery request, CancellationToken cancellationToken)
        {
            var allEvents = _mapper
                .Map<List<EventExportDto>>((await _eventRepository.ListAllAsync())
                .OrderBy(x => x.Date));

            var fileData = _csvExporter.ExportEventsToCsv(allEvents);

            var eventExportFileDto = new EventExportFileVm
            {
                EventExportFile = $"Event_Export_{DateTime.Now.ToString("yyyyMMddHHmmss")}.csv",
                ContentType = "text/csv",
                Data = fileData
            };

            return eventExportFileDto;
        }


    }
}
