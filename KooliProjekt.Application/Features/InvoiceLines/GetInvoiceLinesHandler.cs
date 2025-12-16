using MediatR;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.InvoiceLines
{
    public class GetInvoiceLinesHandler : IRequestHandler<GetInvoiceLinesQuery, List<InvoiceLineDto>>
    {
        private readonly InvoiceLineRepository _repository;

        public GetInvoiceLinesHandler(InvoiceLineRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<InvoiceLineDto>> Handle(GetInvoiceLinesQuery request, CancellationToken cancellationToken)
        {
            var lines = await _repository.GetAllAsync();

            return lines.Select(l => new InvoiceLineDto
            {
                Id = l.Id,
                InvoiceId = l.InvoiceId,
                ItemId = l.ItemId,
                Quantity = l.Quantity,
                UnitPrice = l.UnitPrice,
                Discount = l.Discount,
                Total = l.Total
            }).ToList();
        }
    }
}

