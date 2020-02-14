using System;
using System.Collections.Generic;

namespace WineShop.Models
{
    public partial class WineCategories
    {
        public WineCategories()
        {
            Items = new HashSet<Items>();
        }

        public int WineCategoryId { get; set; }
        public string WineCategoryDescription { get; set; }

        public virtual ICollection<Items> Items { get; set; }
    }
}
