using AutoMapper;
using GloboTicket.TicketManagement.Application.Contracts.Persistence;
using GloboTicket.TicketManagement.Domain.Entities;
using MediatR;


namespace GloboTicket.TicketManagement.Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandHandler : 
        IRequestHandler<CreateCategoryCommand, CreateCategoryCommandResponse>
    {
        private readonly IAsyncRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;

        public  CreateCategoryCommandHandler(
            IAsyncRepository<Category> categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<CreateCategoryCommandResponse> Handle(
           CreateCategoryCommand request, CancellationToken cancellationToken)
         {
           var createCategoryCommandResponse = new CreateCategoryCommandResponse();

           var validator = new CreateCagtegoryCommandValidator();
           var validationResult = await validator.ValidateAsync(request);

      
         var category = new Category { Name = request.Name };

         category = await _categoryRepository.AddAsync(category);
         createCategoryCommandResponse.Category = _mapper.Map<CreateCategoryDto>
         (category);
         

            return createCategoryCommandResponse;
         }
    }
}
