using System;
using System.Collections.Generic;

namespace KooliProjekt.Application.Data
{
    public class Category : Entity
    {
        
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Item> Items { get; set; }
    }
}