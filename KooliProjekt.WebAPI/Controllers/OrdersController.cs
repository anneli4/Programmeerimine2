using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Features.Orders;
using KooliProjekt.Application.Infrastructure.Paging;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var command = new DeleteOrdersCommand { Id = id };
            var result = await Mediator.Send(command);

            if (!result) return NotFound();
            return NoContent();
        }
    }
}
