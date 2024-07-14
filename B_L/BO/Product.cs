using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Product
    {
        public int CarID { get; set; }
        public string? CarName { get; set; }
        public CarCategory? CarType { get; set; }
        public double CarPrice { get; set; }
        public int CarAmount { get; set; }
        

        public override string ToString() => $@"
    Car ID = {CarID}
    Car name = {CarName}
    Car type = {CarType}
    Car price = {CarPrice}
    Car amount = {CarAmount}";
    }
}

