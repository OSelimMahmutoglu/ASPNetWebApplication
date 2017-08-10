using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace North.Models.ViewModel
{
    public class ProductViewModel
    {
        public int? CategoryId { get; internal set; }
        public string CategoryName { get; internal set; }
        public bool Discontinued { get; internal set; }
        public string ProductName { get; internal set; }
        public int ProductsID { get; internal set; }
        public decimal? UnitPrice { get; internal set; }
        public short? UnitsInStock { get; internal set; }
    }
}