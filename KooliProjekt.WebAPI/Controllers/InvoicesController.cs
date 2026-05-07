using MediatR;
using Microsoft.AspNetCore.Mvc;
using KooliProjekt.Application.Features.Invoices;
using KooliProjekt.Application.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KooliProjekt.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoicesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public InvoicesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<InvoiceDto>>> Get()
        {
            var query = new GetInvoicesQuery();
            var invoices = await _mediator.Send(query);
            return Ok(invoices);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] CreateInvoiceCommand command)
        {
            var invoiceId = await _mediator.Send(command);
            return Ok(invoiceId);
        }
    }
}
