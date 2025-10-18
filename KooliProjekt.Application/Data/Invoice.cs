using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Data
{
    public class Invoice
    {
        public int Id { get; set; }

        public string InvoiceNumber { get; set; }
        public int OrderId { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal Discount { get; set; }
        public decimal Paid { get; set; }

        public Order Order { get; set; }
        public ICollection<Invoice_Line> Invoice_lines { get; set; }
    }
}
