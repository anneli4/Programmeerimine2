using MediatR;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Features.Clients;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Clients
{
    public class GetClientsHandler : IRequestHandler<GetClientsQuery, List<ClientDto>>
    {
        private readonly ClientRepository _repository;

        public GetClientsHandler(ClientRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ClientDto>> Handle(GetClientsQuery request, CancellationToken cancellationToken)
        {
            var clients = await _repository.GetAllAsync();

            return clients.Select(c => new ClientDto
            {
                Id = c.Id,
                Name = c.Name,
                Email = c.Email,
                Address = c.Address,
                Phone = c.Phone,
                Discount = c.Discount
            }).ToList();
        }
    }
}
