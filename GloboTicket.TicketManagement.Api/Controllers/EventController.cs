using GloboTicket.TicketManagement.Api.Common;
using GloboTicket.TicketManagement.Application.Features.Events.Commands.CreateEvent;
using GloboTicket.TicketManagement.Application.Features.Events.Commands.DeleteEvent;
using GloboTicket.TicketManagement.Application.Features.Events.Commands.UpdateEvent;
using GloboTicket.TicketManagement.Application.Features.Events.Queries.GetEventDetails;
using GloboTicket.TicketManagement.Application.Features.Events.Queries.GetEventsExport;
using GloboTicket.TicketManagement.Application.Features.Events.Queries.GetEventsList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GloboTicket.TicketManagement.Api.Controllers
{
    [Route("api/[controller]")]
    public class EventController : Controller
    {
        private readonly IMediator _mediator;
        public EventController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("all", Name = "GetAllEvents")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<List<EventListVm>>>> GetAllEvents()
        {
            var result = await _mediator.Send(new GetEventListQuery());
           
            return Ok(new ApiResponse<List<EventListVm>>()
            {
                Message = "Events retrieved",
                Data = result
            });
        }

        [HttpGet("{id}", Name = "GetEventById")]
        public async Task<ActionResult<ApiResponse<EventDetailsVm>>> GetEventById(Guid id)
        {
            var result = await _mediator
                .Send(new GetEventDetailsQuery() { Id = id});

            return Ok(new ApiResponse<EventDetailsVm>
            {
                Message = "Event retuned successfully",
                Data = result
            });
        }

        [HttpPost(Name = "AddEvent")]
        public async Task<ActionResult<ApiResponse<Guid>>>
            CreateEvent([FromBody] CreateEventCommand createEventCommand)
        {
            var eventId = await _mediator.Send(createEventCommand);

            return Ok(new ApiResponse<Guid>
            {
                Message = "Event created successfully",
                Data = eventId
            });
        }

        [HttpPut(Name = "UpdateEvent")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<ApiResponse>> Update(
            [FromBody] UpdateEventCommand updateEventCommand)
        {
            await _mediator.Send(updateEventCommand);

            return Ok(new ApiResponse
            {
                Message = "Event updated successfully"
            });
        }

        [HttpDelete("{id}", Name = "DeleteEvent")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<ApiResponse>> Delete(Guid id)
        {
            await _mediator.Send(new DeleteEventCommand { EventId = id });

            return Ok(new ApiResponse
            {
                Message = "Event deleted successfully"
            });
        }


        [HttpGet("export", Name = "ExportEvents")]
        public async Task<ActionResult<ApiResponse<FileResult>>> ExportEvents()
        {
            var fileDto = await _mediator.Send(new GetEventExportQuery());

            return Ok(new ApiResponse<FileResult>
            {
                Message = "Events exported successfully",
                Data = File(fileDto.Data, fileDto.ContentType, fileDto.EventExportFile)
            });
        }
    }
}
