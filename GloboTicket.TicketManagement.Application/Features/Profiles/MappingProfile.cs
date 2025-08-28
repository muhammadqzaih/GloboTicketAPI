using AutoMapper;
using GloboTicket.TicketManagement.Application.Features.Events.Queries.GetEventDetails;
using GloboTicket.TicketManagement.Application.Features.Events.Queries.GetEventsList;
using GloboTicket.TicketManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GloboTicket.TicketManagement.Application.Features.Profiles
{
    public class MappingProfile : Profile
    {
        MappingProfile()
        {
            CreateMap<Event, EventListVm>().ReverseMap();
            CreateMap<Event, EventDetailsVm>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
        }
    }
}
