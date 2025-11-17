using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Features.Items;
using KooliProjekt.Application.Infrastructure.Paging;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KooliProjekt.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<PagedResult<Item>>> GetItems([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var query = new GetItemsQuery { Page = page, PageSize = pageSize };
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var command = new DeleteItemsCommand { Id = id };
            var result = await Mediator.Send(command);

            if (!result) return NotFound();
            return NoContent();
        }
    }
}
