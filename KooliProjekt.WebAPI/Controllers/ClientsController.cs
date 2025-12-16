using MediatR;
using Microsoft.AspNetCore.Mvc;
using KooliProjekt.Application.Features.Clients;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KooliProjekt.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClientsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/Clients
        [HttpGet]
        public async Task<ActionResult<List<ClientDto>>> Get()
        {
            var query = new GetClientsQuery();
            var clients = await _mediator.Send(query);
            return Ok(clients);
        }

        // POST: api/Clients
        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] CreateClientCommand command)
        {
            var clientId = await _mediator.Send(command);
            return Ok(clientId);
        }

        // Hiljem saab lisada PUT ja DELETE otspunkte
    }
}
