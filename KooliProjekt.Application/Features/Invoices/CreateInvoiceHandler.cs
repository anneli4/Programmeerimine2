using MediatR;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Invoices
{
    public class CreateInvoiceHandler : IRequestHandler<CreateInvoiceCommand, int>
    {
        private readonly InvoiceRepository _repository;

        public CreateInvoiceHandler(InvoiceRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
        {
            var invoice = new Invoice
            {
                InvoiceNumber = request.InvoiceNumber,
                OrderId = request.OrderId,
                ClientId = request.ClientId,
                Date = request.Date,
                TotalAmount = request.TotalAmount,
                Discount = request.Discount,
                Paid = request.Paid
            };

            var createdInvoice = await _repository.AddAsync(invoice);
            return createdInvoice.Id;
        }
    }
}
