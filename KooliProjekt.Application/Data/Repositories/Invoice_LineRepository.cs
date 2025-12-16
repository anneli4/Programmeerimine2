using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Data.Repositories
{
    public class InvoiceLineRepository : BaseRepository<Invoice_Line>, IInvoiceLineRepository
    {
        public InvoiceLineRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
