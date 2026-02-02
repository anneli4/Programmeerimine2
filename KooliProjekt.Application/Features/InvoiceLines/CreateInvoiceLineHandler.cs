using MediatR;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.InvoiceLines
{
    public class CreateInvoiceLineHandler : IRequestHandler<CreateInvoiceLineCommand, int>
    {
        private readonly InvoiceLineRepository _repository;

        public CreateInvoiceLineHandler(InvoiceLineRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(CreateInvoiceLineCommand request, CancellationToken cancellationToken)
        {
            var invoiceLine = new Invoice_Line
            {
                InvoiceId = request.InvoiceId,
                ItemId = request.ItemId,
                Quantity = request.Quantity,
                UnitPrice = request.UnitPrice,
                Discount = request.Discount,
                Total = request.Total
            };

            var createdLine = await _repository.AddAsync(invoiceLine);
            return createdLine.Id;
        }
    }
}
