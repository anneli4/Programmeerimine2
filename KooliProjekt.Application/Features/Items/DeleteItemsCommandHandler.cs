using KooliProjekt.Application.Data;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Items
{
    public class DeleteItemCommandHandler : IRequestHandler<DeleteItemsCommand, bool>
    {
        private readonly ApplicationDbContext _context;

        public DeleteItemCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteItemsCommand request, CancellationToken cancellationToken)
        {
            var item = await _context.Items.FindAsync(request.Id);
            if (item == null) return false;

            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

