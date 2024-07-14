using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using DalApi;


namespace BlImplementation
{
    internal class BL_BO_Order : BlApi.IOrder
    {
        //private DalApi.IDal IDal = new Dal.DalList();

        private IDal dal = DalApi.Factory.Get();
        
        private BO.Status GetStatus(DO.Order order)
        {
            if (order.OrderDeliveryDate != null) { return BO.Status.Provided; } 
            if (order.OrderShipDate != null) { return BO.Status.Shiped; }
            return BO.Status.Confirmed;
        }


        private BO.Order ConvertOrder(DO.Order dataOrder)
        {
            
            BO.Order logicOrder = new BO.Order()
            {
                OrderUniqCode = dataOrder.OrderUniqCode,
                OrderCustomerName = dataOrder.OrderCustomerName,
                OrderCustomerAddress = dataOrder.OrderCustomerAddress,
                OrderCustomerEmail = dataOrder.OrderCustomerEmail,
                OrderCreationDate = dataOrder.OrderCreationDate,
                OrderShipDate = dataOrder.OrderShipDate,
                OrderDeliveryDate = dataOrder.OrderDeliveryDate,
                paymentDate = dataOrder.OrderCreationDate,// calculating the time when the customer payed
                OrderStatus = GetStatus(dataOrder),

            };

            foreach (var item in dal.OrderItem.GetAll(orderItem => orderItem.OrderID == dataOrder.OrderUniqCode))
            {
                logicOrder.OrderDetails.Add(new BO.OrderItem()
                {
                    ProductID = item.ProductID,
                    ProductName = dal.Product.Get(item.ProductID).CarName,
                    OrderItemAmount = item.OrderItemAmount,
                    ProductPrice = item.OrderProductPrice,
                    TotalProductPrice = item.OrderProductPrice * item.OrderItemAmount,
                });
            }
            // the total price foe all the order
            logicOrder.Totalprice = logicOrder.OrderDetails.Sum(item => item.TotalProductPrice);
            return logicOrder;
        }

        public IEnumerable<BO.OrderForList> GetOrderForList()
        {
            return from item in dal.Order.GetAll()
                   let orderItems = dal.OrderItem.GetAll(orderItem => orderItem.OrderID == item.OrderUniqCode)
                   select new BO.OrderForList()
                   {
                       ID = item.OrderUniqCode,
                       Name = item.OrderCustomerName,
                       Status = GetStatus(item),
                       AmountOfItems = orderItems.Count(),
                       TotalPrice = orderItems.Sum(orderItem => orderItem.OrderItemAmount * orderItem.OrderProductPrice),
                   };     
        }

        public BO.Order GetOrder(int id)
        {

            if (id <= 0) { throw new BO.InvalidInputException("id"); }

            // if we did not find the order then we throw an error
            try
            {
                DO.Order cheackOrder = dal.Order.Get(id);
                //DO.OrderItem cheackOrderItem = IDal.OrderItem.Get(id); 
            }
            catch { throw new BO.NotFoundException("order item"); }

            return ConvertOrder(dal.Order.Get(id));

        }

        public BO.Order UpdateOrder(int orderID)
        {


            try { DO.Order cheack = dal.Order.Get(orderID); }
            catch { throw new BO.NotFoundException("order"); }

             
            DO.Order dataOrder = dal.Order.Get(orderID); // we find the order from the data layer

            // we build our new logic order by using our GetOrder method  
            BO.Order logicOrder = ConvertOrder(dataOrder);

            //check if still not delivered
            if (dataOrder.OrderShipDate == null )
            {
                logicOrder.OrderShipDate = DateTime.Now;
                dataOrder.OrderShipDate = DateTime.Now;
                logicOrder.OrderStatus = BO.Status.Shiped;
            }
            else
            {
                throw new Exception("the product is already shiped");
            }

            dal.Order.Update(dataOrder); // update in data layer 
            return logicOrder;
        } 

        public BO.Order UpdateOrderDeliver(int orderID)
        {
            try { DO.Order cheack = dal.Order.Get(orderID); }
            catch { throw new BO.NotFoundException("order"); }


            DO.Order dataOrder = dal.Order.Get(orderID); // we find the order from the data layer

            // we build our new logic order by using our GetOrder method  
            BO.Order logicOrder = ConvertOrder(dataOrder);

            // if the product was not shiped then we cannot deliver it
            if (dataOrder.OrderShipDate == null)
                throw new Exception("the product was not shiped yet");
            
            //check if still not delivered
            if (dataOrder.OrderDeliveryDate == null)
            {
                logicOrder.OrderDeliveryDate = DateTime.Now;
                dataOrder.OrderDeliveryDate = DateTime.Now;
                logicOrder.OrderStatus = BO.Status.Provided;
            }
            else
            {
                throw new Exception("the product is already delivered");
            }

            dal.Order.Update(dataOrder); // update in data layer 
            return logicOrder;
        }

        public BO.OrderTracking OrderTrack(int orderID)
        {
            try
            {
                // cheack if the order exist
                DO.Order DalOrderCheck = dal.Order.Get(orderID);
            }
            catch (BO.NotFoundException ex)
            {
                throw new BO.NotFoundException("order");
            }

            //BO.Order order = GetOrder(orderID);
            DO.Order dataOrder = dal.Order.Get(orderID);
            BO.OrderTracking orderTracking = new BO.OrderTracking();

            orderTracking.OrderUniqCode = orderID;
            orderTracking.Status = GetStatus(dataOrder);

            // why should we add to the list insted of set the index 0 to what we whant?
            orderTracking.OrderTrackingList.Add(new Tuple<DateTime,
                BO.Status>((DateTime)dataOrder.OrderCreationDate, BO.Status.Confirmed));

            if(dataOrder.OrderShipDate != null)
            {
                orderTracking.OrderTrackingList.Add( new Tuple<DateTime,
               BO.Status>((DateTime)dataOrder.OrderShipDate, BO.Status.Shiped));
            }

            if (dataOrder.OrderDeliveryDate != null)
            {
                orderTracking.OrderTrackingList.Add(new Tuple<DateTime,
               BO.Status>((DateTime)dataOrder.OrderDeliveryDate, BO.Status.Provided));
            }
            return orderTracking;
        }
    }
}

