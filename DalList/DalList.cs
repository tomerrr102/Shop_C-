using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    internal sealed class DalList : IDal
    {
        private DalList() { }
        public static IDal Instance { get; } = new DalList();

        public IProduct Product => new DalProduct();

        public IOrder Order =>  new DalOrder();

        public IOrderItem OrderItem => new DalOrderItem();
    }
}
