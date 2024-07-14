using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using DalList;
using DalApi;

namespace Dal;

internal class DalOrderItem : IOrderItem
{

    [MethodImpl(MethodImplOptions.Synchronized)]
    public int Add(OrderItem orderItem)
    {
        if (DataSource.OrderItems.Exists(stat => stat.OrderItemUniqCode == orderItem.OrderItemUniqCode))
            throw new AlreadyExistsException("Order Item");

        orderItem.OrderItemUniqCode = DataSource.Config.getRunOrderItems();
        DataSource.OrderItems.Add(orderItem);
        return orderItem.OrderItemUniqCode;
    }


    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int id)
    {
        if (!DataSource.OrderItems.Exists(stat => stat.OrderID == id))
            throw new NotFoundException("order Item");
        OrderItem temp = Get(id);
        DataSource.OrderItems.Remove(temp);
    }


    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(OrderItem orderItem)
    {
        int index = DataSource.OrderItems.FindIndex(i => i.OrderID == orderItem.OrderID);
        if (index == -1)
        {
            throw new NotFoundException("order Item");
        }
        DataSource.OrderItems[index] = orderItem;
    }


    [MethodImpl(MethodImplOptions.Synchronized)]
    public OrderItem Get(int Id)
    {
        OrderItem orderItem = DataSource.OrderItems.Find(i => i.OrderID == Id);
        return orderItem.OrderID != default ? orderItem : throw new NotFoundException("order Item");
    }

    
    public OrderItem GetElementByCondition(Func<OrderItem, bool>? cond)
    {
        return DataSource.OrderItems.Find(item => cond(item));
    } // new method

    public IEnumerable<OrderItem> GetAll(Func<OrderItem, bool>? cond = null)
    {
        if (cond == null)
            return DataSource.OrderItems.Select(item => item);

        return DataSource.OrderItems.Where(item => cond(item));
    } // updated method
 
    //public IEnumerable<OrderItem> GetOrdersByCondition(Predicate<OrderItem> predicate)
    //{
    //    return DataSource.OrderItems.Where(item => predicate(item));

    //}



}
