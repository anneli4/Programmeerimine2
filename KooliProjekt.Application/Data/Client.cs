using System.Collections.Generic;

namespace KooliProjekt.Application.Data
{
    public class Client : Entity
    {
        
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public decimal Discount { get; set; }

        public ICollection<Order> Orders { get; set; }
        public ICollection<Invoice> Invoices { get; set; } 
    }
}