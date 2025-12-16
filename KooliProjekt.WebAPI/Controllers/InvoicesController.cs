using MediatR;
using Microsoft.AspNetCore.Mvc;
using KooliProjekt.Application.Features.InvoiceLines;
using KooliProjekt.Application.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KooliProjekt.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceLinesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public InvoiceLinesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/InvoiceLines
        [HttpGet]
        public async Task<ActionResult<List<InvoiceLineDto>>> Get()
        {
            var query = new GetInvoiceLinesQuery();
            var lines = await _mediator.Send(query);
            return Ok(lines);
        }

        // POST: api/InvoiceLines
        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] CreateInvoiceLineCommand command)
        {
            var lineId = await _mediator.Send(command);
            return Ok(lineId);
        }
    }
}
