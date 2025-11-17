using KooliProjekt.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Invoices
{
    public class DeleteInvoiceCommandHandler : IRequestHandler<DeleteInvoicesCommand, bool>
    {
        private readonly ApplicationDbContext _context;

        public DeleteInvoiceCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteInvoicesCommand request, CancellationToken cancellationToken)
        {
            var invoice = await _context.Invoices
                                .Include(i => i.Invoice_lines)
                                .FirstOrDefaultAsync(i => i.Id == request.Id);

            if (invoice == null) return false;

            _context.Invoices.Remove(invoice);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
