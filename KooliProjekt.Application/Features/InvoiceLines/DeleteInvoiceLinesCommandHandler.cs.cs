using KooliProjekt.Application.Data;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.InvoiceLines
{
    public class DeleteInvoiceLinesCommandHandler : IRequestHandler<DeleteInvoiceLineCommand, bool>
    {
        private readonly ApplicationDbContext _context;

        public DeleteInvoiceLinesCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteInvoiceLineCommand request, CancellationToken cancellationToken)
        {
            var line = await _context.Invoice_Lines.FindAsync(request.Id);
            if (line == null) return false;

            _context.Invoice_Lines.Remove(line);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
