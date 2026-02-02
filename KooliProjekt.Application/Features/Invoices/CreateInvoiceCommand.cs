using MediatR;
using System;

namespace KooliProjekt.Application.Features.Invoices
{
    public record CreateInvoiceCommand(
        string InvoiceNumber,
        int OrderId,
        int ClientId,
        DateTime Date,
        decimal TotalAmount,
        decimal Discount,
        decimal Paid
    ) : IRequest<int>;
}
