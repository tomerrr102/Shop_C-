using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class OrderItem
    {


        // uniq number for recognize item in the order
        public int OrderItemUniqCode { get; set; }

        // the recognize number for product
        public int ProductID { get; set; }

        public string? ProductName { get; set; }


        // price of item
        public double ProductPrice { get; set; }

        // amount of items
        public int OrderItemAmount { get; set; }

        public double TotalProductPrice { get; set; }

        public override string ToString() => $@"
    Order item uniq code = {OrderItemUniqCode}
    Product ID = {ProductID}
    Product Name = {ProductName}
    Product Price = {ProductPrice}
    Order item amount = {OrderItemAmount}
    Total Product Price = {TotalProductPrice}";
    }
}
