using KooliProjekt.Application.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Data.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context) { }

        public async Task<List<Category>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _db.Categories.ToListAsync(cancellationToken);
        }
    }
}


