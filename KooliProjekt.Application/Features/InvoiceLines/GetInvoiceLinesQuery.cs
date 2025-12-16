using MediatR;
using KooliProjekt.Application.Dto;
using System.Collections.Generic;

namespace KooliProjekt.Application.Features.InvoiceLines
{
    public record GetInvoiceLinesQuery() : IRequest<List<InvoiceLineDto>>;
}

