using System;
using System.Collections.Generic;

namespace WineShop.Models
{
    public partial class OrderStatuses
    {
        public OrderStatuses()
        {
            Orders = new HashSet<Orders>();
        }

        public int OrderStatusId { get; set; }
        public string OrderStatusDescription { get; set; }

        public virtual ICollection<Orders> Orders { get; set; }
    }
}
