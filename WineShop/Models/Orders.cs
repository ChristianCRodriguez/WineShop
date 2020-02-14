using System;
using System.Collections.Generic;

namespace WineShop.Models
{
    public partial class Orders
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public int OrderStatusId { get; set; }
        public string UserId { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public double OrderTotal { get; set; }

        public virtual Items Item { get; set; }
        public virtual OrderStatuses OrderStatus { get; set; }
        public virtual AspNetUsers User { get; set; }
    }
}
