using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class OrderForList
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public double TotalPrice { get; set; }
        public int AmountOfItems { get; set; }
        public Status? Status { get; set; }

        public override string ToString() => $@"
    ID = {ID}
    Name = {Name}
    TotalPrice = {TotalPrice}
    AmountOfItems = {AmountOfItems}
    Status = {Status}
    ";
    }
}