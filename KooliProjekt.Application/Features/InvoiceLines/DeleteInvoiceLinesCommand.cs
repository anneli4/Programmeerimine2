using MediatR;

namespace KooliProjekt.Application.Features.InvoiceLines
{
    public class DeleteInvoiceLineCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}

