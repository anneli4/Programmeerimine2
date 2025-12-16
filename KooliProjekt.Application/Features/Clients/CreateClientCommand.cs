using MediatR;

public record CreateClientCommand(
    string Name,
    string Email,
    string Address,
    string Phone,
    decimal Discount
) : IRequest<int>;
