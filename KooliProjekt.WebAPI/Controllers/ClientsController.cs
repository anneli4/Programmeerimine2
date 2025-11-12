using KooliProjekt.Application.Data;
using KooliProjekt.Application.Features.Clients;
using KooliProjekt.Application.Infrastructure.Paging;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KooliProjekt.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<PagedResult<Client>>> GetClients([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var query = new GetClientsQuery { Page = page, PageSize = pageSize };
            var result = await Mediator.Send(query);
            return Ok(result);
        }
    }
}






