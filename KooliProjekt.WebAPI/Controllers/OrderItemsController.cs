using MediatR;
using Microsoft.AspNetCore.Mvc;
using KooliProjekt.Application.Features.OrderItems;
using KooliProjekt.Application.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KooliProjekt.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderItemsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderItemsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/OrderItems
        [HttpGet]
        public async Task<ActionResult<List<OrderItemDto>>> Get()
        {
            var query = new GetOrderItemsQuery();
            var items = await _mediator.Send(query);
            return Ok(items);
        }

        // POST: api/OrderItems
        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] CreateOrderItemCommand command)
        {
            var itemId = await _mediator.Send(command);
            return Ok(itemId);
        }
    }
}
