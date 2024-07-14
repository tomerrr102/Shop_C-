using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BO;
using DalApi;

namespace BlImplementation
{
    internal class BL_BOCart : IBOCart
    {
        //private DalApi.IDal iDal = new Dal.DalList();
        private IDal dal = DalApi.Factory.Get();

        public BO.Cart AddItemToCart(BO.Cart cart, int ProductId)
        {

            try
            {
                // cheack if the product exist
                DO.Product DalProductCheck = dal.Product.Get(ProductId);
            }
            catch(BO.NotFoundException ex)
            {
                throw new BO.NotFoundException("product");

            }

            //  then import the product from Dal
            DO.Product DalProduct = dal.Product.Get(ProductId);
            if (DalProduct.CarAmount < 1)
            {
                throw new BO.EmptyAmountExeption("amount");
            }

            // cheack if we ordered this product (if the product is in the order list)
            BO.OrderItem orderItem = cart.items.Find(item => item.ProductID == ProductId);

            if (orderItem != default)
            {
                orderItem.OrderItemAmount++;
                orderItem.TotalProductPrice += DalProduct.CarPrice;
                cart.TotalPrice += DalProduct.CarPrice;
            }

            else
            {      
            cart.items.Add(new BO.OrderItem
                {
                    OrderItemAmount = 1,
                    ProductID = ProductId,
                    ProductName = DalProduct.CarName,
                    ProductPrice = DalProduct.CarPrice,
                    TotalProductPrice = DalProduct.CarPrice
                });
                cart.TotalPrice += DalProduct.CarPrice;
            }
            return cart;
        }

        public BO.Cart UpdateProductAmount(BO.Cart cart, int ProductId, int newAmount)
        {

            try
            {
                // cheack if the product exist
                DO.Product DalProductCheck = dal.Product.Get(ProductId);
            }
            catch (BO.NotFoundException ex)
            {
                throw new BO.NotFoundException("product");
            }

            // import our product from data layer
            DO.Product p = dal.Product.Get(ProductId);
            
            // find the order in the order list
            BO.OrderItem orderItem = cart.items.Find(item => item.ProductID == ProductId);

            if (orderItem == null) { throw new BO.NotFoundException("order"); }

            if (newAmount > orderItem.OrderItemAmount)
            {
                
                cart.TotalPrice += p.CarPrice * (newAmount - orderItem.OrderItemAmount); // set the new price to be the difference between the amounts
                orderItem.OrderItemAmount = newAmount; // set the amount of orders to be the new amount
                orderItem.TotalProductPrice = p.CarPrice * newAmount; // the new price of the orders is  
            }

            else if (newAmount < orderItem.OrderItemAmount && newAmount != 0)
            {
                cart.TotalPrice -= p.CarPrice * (orderItem.OrderItemAmount - newAmount);
                orderItem.OrderItemAmount = newAmount; // set the amount of orders to be the new amount
                orderItem.TotalProductPrice = p.CarPrice * newAmount; // the new price of the orders    
            }
            
            else if (newAmount == 0)
            {
                cart.TotalPrice -= orderItem.TotalProductPrice * (orderItem.OrderItemAmount);
                cart.items.Remove(orderItem); // delete the product we want to remove
            }
            
            return cart;
        }
        
        public BO.Cart EnterCart(BO.Cart cart)
        {
            if (cart.CartAdress == "" || cart.CartMail == "" || cart.CustomerName == "")
            {
                throw new InvalidInputException("the data");
            }

            foreach (var item in cart.items)
            {
                try
                {
                    DO.Product P = dal.Product.Get(item.ProductID);
                    if (item.OrderItemAmount < 1) { throw new BO.InvalidInputException("id"); }
                    if (P.CarAmount < item.OrderItemAmount) { throw new InvalidInputException("Order Item Amount"); }
                }
                catch(InvalidInputException ex)
                {
                    throw new InvalidInputException(" The ID or amount where wrong");
                }
                catch
                {
                    throw new BO.NotFoundException("product");
                }

            }

            DO.Order newOrder = new DO.Order()
            {
                OrderCustomerAddress = cart.CartAdress,
                OrderCustomerEmail = cart.CartMail,
                OrderCustomerName = cart.CustomerName,
                OrderCreationDate = DateTime.Now,
                OrderDeliveryDate = null,
                OrderShipDate = null,
            };

            int orderID = dal.Order.Add(newOrder);

            foreach (var item in cart.items)
            {
                dal.OrderItem.Add(new DO.OrderItem()
                {
                    OrderID = orderID,
                    ProductID = item.ProductID,
                    OrderItemAmount = item.OrderItemAmount,
                    OrderProductPrice = item.ProductPrice
                    
                });
                DO.Product P1 = dal.Product.Get(item.ProductID);
                P1.CarAmount -= item.OrderItemAmount;
                dal.Product.Update(P1);
            }
            return cart;
        }
    }
}
