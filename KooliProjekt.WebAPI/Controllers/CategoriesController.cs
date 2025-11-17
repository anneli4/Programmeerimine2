using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Features.Categories;
using KooliProjekt.Application.Infrastructure.Paging;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KooliProjekt.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<PagedResult<Category>>> GetCategories([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var query = new GetCategoriesQuery { Page = page, PageSize = pageSize };
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var command = new DeleteCategoriesCommand { Id = id };
            var result = await Mediator.Send(command);

            if (!result) return NotFound();
            return NoContent();
        }
    }
}
