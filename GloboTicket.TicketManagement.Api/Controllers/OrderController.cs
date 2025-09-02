using GloboTicket.TicketManagement.Application.Features.Orders.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GloboTicket.TicketManagement.Api.Controllers
{
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly IMediator _mediator;
       
        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("/getpageordersformonth", Name = "GetPagedOrdersForMonth")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<PagedOrdersForMonthVm>> GetOrdersForMonth(
           DateTime date, int page, int size)
        {
            var orders = await _mediator.Send(new GetOrdersForMonthQuery()
            {
                Date = date,
                Page = page,
                Size = size
            });

            return Ok(orders);
        }
    }
}
