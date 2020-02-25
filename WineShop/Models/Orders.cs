using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WineShop.Models
{
    public partial class Orders
    {
        [JsonPropertyName("orderId")]
        public int OrderId { get; set; }

        [JsonPropertyName("orderDate")]
        public DateTime OrderDate { get; set; }

        [JsonPropertyName("orderStatusId")]
        public int OrderStatusId { get; set; }

        [JsonPropertyName("userId")]
        public string UserId { get; set; }

        [JsonPropertyName("itemId")]
        public int ItemId { get; set; }

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        [JsonPropertyName("orderTotal")]
        public double OrderTotal { get; set; }

        [JsonPropertyName("item")]
        public virtual Items Item { get; set; }

        [JsonPropertyName("orderStatus")]
        public virtual OrderStatuses OrderStatus { get; set; }

        [JsonPropertyName("user")]
        public virtual AspNetUsers User { get; set; }
    }
}
