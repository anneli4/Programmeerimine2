using System;
using System.Collections.Generic;

namespace KooliProjekt.Application.Data
{
    public class Invoice
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; }
        public int OrderId { get; set; }
        public int ClientId { get; set; }      // foreign key
        public DateTime Date { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal Discount { get; set; }
        public decimal Paid { get; set; }

        public Order Order { get; set; }
        public Client Customer { get; set; }   // navigatsiooniproperty, nimeks Customer
        public ICollection<Invoice_Line> Invoice_lines { get; set; }
    }
}
