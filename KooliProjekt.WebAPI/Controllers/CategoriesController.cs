using MediatR;
using Microsoft.AspNetCore.Mvc;
using KooliProjekt.Application.Features.Categories;
using KooliProjekt.Application.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KooliProjekt.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<List<CategoryDto>>> Get()
        {
            var query = new GetCategoriesQuery();
            var categories = await _mediator.Send(query);
            return Ok(categories);
        }

        // POST: api/Categories
        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] CreateCategoryCommand command)
        {
            var categoryId = await _mediator.Send(command);
            return Ok(categoryId);
        }
    }
}
