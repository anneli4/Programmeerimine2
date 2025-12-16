using MediatR;
using Microsoft.AspNetCore.Mvc;
using KooliProjekt.Application.Features.Items;
using KooliProjekt.Application.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KooliProjekt.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ItemsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/Items
        [HttpGet]
        public async Task<ActionResult<List<ItemDto>>> Get()
        {
            var query = new GetItemsQuery();
            var items = await _mediator.Send(query);
            return Ok(items);
        }

        // POST: api/Items
        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] CreateItemCommand command)
        {
            var itemId = await _mediator.Send(command);
            return Ok(itemId);
        }
    }
}
