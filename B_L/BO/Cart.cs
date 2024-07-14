using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Cart
    {
        public string? CustomerName { get; set; }
        public string? CartMail { get; set; }
        public string? CartAdress { get; set; }
        
        public List<OrderItem?> items = new();
        public double TotalPrice { get; set; }
        public override string ToString() => $@"
    Customer Name = {CustomerName}
    Cart Mail = {CartMail}
    Cart Adress = {CartAdress}
    Order Product List size = {items.Count()}
    Total Price = {TotalPrice}
    ";

    }
}
