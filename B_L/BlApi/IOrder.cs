using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi
{
    public interface IOrder
    {

        public IEnumerable<BO.OrderForList> GetOrderForList();

        public BO.Order GetOrder(int id);

        public BO.Order UpdateOrder(int id);

        public BO.Order UpdateOrderDeliver(int OrderID);

        public BO.OrderTracking OrderTrack(int OrderID);
    }
}
