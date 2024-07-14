using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class OrderTracking
    {
        public int OrderUniqCode { get; set; }

        public Status? Status { get; set; }

        public List<Tuple<DateTime, Status>?> OrderTrackingList = new();

        public override string ToString() => $@"
    Order uniqcode = {OrderUniqCode}
    Order status: {Status}
    in: {OrderTrackingList[OrderTrackingList.Count() - 1].Item1} the order was {OrderTrackingList[OrderTrackingList.Count() - 1].Item2}      
   ";
    }
    //in: {OrderTrackingList[OrderTrackingList.Count() - 1].ToValueTuple()}
}

