using System;
using System.Collections.Generic;

namespace WineShop.Models
{
    public partial class WineTypes
    {
        public WineTypes()
        {
            Items = new HashSet<Items>();
        }

        public int WineTypeId { get; set; }
        public string WineTypeDescription { get; set; }

        public virtual ICollection<Items> Items { get; set; }
    }
}
