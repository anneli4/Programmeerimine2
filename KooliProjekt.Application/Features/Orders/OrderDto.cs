using System;
using System.Collections.Generic;

namespace KooliProjekt.Application.Features.Orders
{
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int ClientId { get; set; }
        public decimal Discount { get; set; }
    }
}
