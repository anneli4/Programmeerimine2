using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Data
{
    public class Order :Entity
    {
        
        public DateTime Date { get; set; }
        public int ClientId { get; set; }
        public decimal Discount { get; set; }

        public Client Client { get; set; }
        public ICollection<Order_Item> Order_Items { get; set; }
        public ICollection<Invoice> Invoices { get; set; }
    }
}
