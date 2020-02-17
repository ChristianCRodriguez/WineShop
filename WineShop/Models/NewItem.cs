using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WineShop.Models
{
    public class NewItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public int WineCategoryId { get; set; }
        public int WineTypeId { get; set; }

        public List<WineCategories> WineCategoryList { get; set; }
        public List<WineTypes> WineTypeList { get; set; }
    }
}
