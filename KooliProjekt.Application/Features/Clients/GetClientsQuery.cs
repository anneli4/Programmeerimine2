using MediatR;
using System.Collections.Generic;
using KooliProjekt.Application.Features.Clients;

namespace KooliProjekt.Application.Features.Clients
{
    // See ütleb MediatR-ile, et päring tagastab List<ClientDto>
    public record GetClientsQuery() : IRequest<List<ClientDto>>;
}
