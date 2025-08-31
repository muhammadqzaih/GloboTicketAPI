using GloboTicket.TicketManagement.Application.Contracts.Persistence;
using GloboTicket.TicketManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GloboTicket.TicketManagement.Infrastructure.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(GloboTicketDbContext context) : base(context)
        {
        }
        public async Task<List<Category>> GetCategoriesWithEvents(bool includePassedEvents)
        {
            var query = _context.Categories.AsQueryable();

            if (includePassedEvents)
            {
                query = query.Include(c => c.Events);
            }
            else
            {
                query = query.Include(c => c.Events.Where(e => e.Date >= DateTime.Today));
            }

            return await query.ToListAsync();
        }

    }
}
