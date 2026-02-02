using MediatR;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Invoices
{
    public class GetInvoicesHandler : IRequestHandler<GetInvoicesQuery, List<InvoiceDto>>
    {
        private readonly InvoiceRepository _repository;

        public GetInvoicesHandler(InvoiceRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<InvoiceDto>> Handle(GetInvoicesQuery request, CancellationToken cancellationToken)
        {
            var invoices = await _repository.GetAllAsync();

            return invoices.Select(i => new InvoiceDto
            {
                Id = i.Id,
                InvoiceNumber = i.InvoiceNumber,
                OrderId = i.OrderId,
                ClientId = i.ClientId,
                Date = i.Date,
                TotalAmount = i.TotalAmount,
                Discount = i.Discount,
                Paid = i.Paid
            }).ToList();
        }
    }
}

