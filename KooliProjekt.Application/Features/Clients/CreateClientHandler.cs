using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

public class CreateClientHandler : IRequestHandler<CreateClientCommand, int>
{
    private readonly ClientRepository _repository;

    public CreateClientHandler(ClientRepository repository)
    {
        _repository = repository;
    }

    public async Task<int> Handle(CreateClientCommand request, CancellationToken cancellationToken)
    {
        var client = new Client
        {
            Name = request.Name,
            Email = request.Email,
            Address = request.Address,
            Phone = request.Phone,
            Discount = request.Discount
        };

        var createdClient = await _repository.AddAsync(client);

        return createdClient.Id;
    }
}
