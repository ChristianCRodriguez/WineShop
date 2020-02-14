using System;
using System.Collections.Generic;

namespace WineShop.Models
{
    public partial class Items
    {
        public Items()
        {
            Orders = new HashSet<Orders>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public int WineCategoryId { get; set; }
        public int WineTypeId { get; set; }

        public virtual WineCategories WineCategory { get; set; }
        public virtual WineTypes WineType { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
