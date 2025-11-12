using KooliProjekt.Application.Data;
using KooliProjekt.Application.Features.InvoiceLines;
using KooliProjekt.Application.Infrastructure.Paging;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KooliProjekt.WebAPI.Controllers
{
    public class InvoiceLinesController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<PagedResult<Invoice_Line>>> GetInvoiceLines([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var query = new GetInvoiceLinesQuery { Page = page, PageSize = pageSize };
            var result = await Mediator.Send(query);
            return Ok(result);
        }
    }
}

