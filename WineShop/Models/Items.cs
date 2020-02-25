using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WineShop.Models
{
    public partial class Items
    {
        public Items()
        {
            Orders = new HashSet<Orders>();
        }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("price")]
        public double Price { get; set; }

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        [JsonPropertyName("wineCategoryId")]
        public int WineCategoryId { get; set; }

        [JsonPropertyName("wineTypeId")]
        public int WineTypeId { get; set; }

        [JsonPropertyName("wineCategory")]
        public virtual WineCategories WineCategory { get; set; }

        [JsonPropertyName("wineType")]
        public virtual WineTypes WineType { get; set; }

        [JsonPropertyName("orders")]
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
