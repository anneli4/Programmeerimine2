using MediatR;

namespace KooliProjekt.Application.Features.InvoiceLines
{
    public record CreateInvoiceLineCommand(
        int InvoiceId,
        int ItemId,
        int Quantity,
        decimal UnitPrice,
        decimal Discount,
        decimal Total
    ) : IRequest<int>;
}
