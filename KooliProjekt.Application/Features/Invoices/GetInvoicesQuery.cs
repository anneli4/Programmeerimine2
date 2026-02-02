using MediatR;
using KooliProjekt.Application.Dto;
using System.Collections.Generic;

namespace KooliProjekt.Application.Features.Invoices
{
    public record GetInvoicesQuery() : IRequest<List<InvoiceDto>>;
}

