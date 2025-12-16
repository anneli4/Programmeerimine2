using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Data
{
    public class Item :Entity
    {
       
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string Photo { get; set; }

        public Category Category { get; set; }
        public ICollection<Order_Item> Order_items { get; set; }
        public ICollection<Invoice_Line> Invoice_lines { get; set; }
    }
}
