using GloboTicket.TicketManagement.Api.Common;
using GloboTicket.TicketManagement.Application.Features.Categories.Commands.CreateCategory;
using GloboTicket.TicketManagement.Application.Features.Categories.Queries.GetCategoriesList;
using GloboTicket.TicketManagement.Application.Features.Categories.Queries.GetCategoriesListWithEvents;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GloboTicket.TicketManagement.Api.Controllers
{

    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("all", Name = "GetAllCategories")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<List<CategoriesListVm>>>> GetAllCategories()
        {
            var dtos = await _mediator.Send(new GetCategoriesListQuery());

            return Ok(new ApiResponse<List<CategoriesListVm>>
            {
                Message = "Categories retrieved successfully",
                Data = dtos
            });
        }

        [HttpGet("allwithevents", Name = "GetCategoriesWithEvents")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<List<CategoryEventListVm>>>>
            GetCategoriesWithEvents(bool includeHistory)
        {
            var dtos = await _mediator.Send(
                new GetCategoriesListWithEventsQuery { IncludeHistory = includeHistory });

            return Ok(new ApiResponse<List<CategoryEventListVm>>
            {
                Message = "Categories with events retrieved successfully",
                Data = dtos
            });
        }

        [HttpPost(Name = "AddCategory")]
        public async Task<ActionResult<ApiResponse<CreateCategoryCommandResponse>>> Create(
            [FromBody] CreateCategoryCommand createCategoryCommand)
        {
            var response = await _mediator.Send(createCategoryCommand);

            return Ok(new ApiResponse<CreateCategoryCommandResponse>
            {
                Message = "Category created successfully",
                Data = response
            });
        }
    }
}