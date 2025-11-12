using KooliProjekt.Application.Data;
using KooliProjekt.Application.Features.Orders;
using KooliProjekt.Application.Infrastructure.Paging;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KooliProjekt.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<PagedResult<Order>>> GetOrders([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var query = new GetOrdersQuery { Page = page, PageSize = pageSize };
            var result = await Mediator.Send(query);
            return Ok(result);
        }
    }
}

