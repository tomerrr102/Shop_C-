using DO;
using System.Runtime.CompilerServices;
using DalApi;
namespace Dal;


internal class DalOrder: IOrder
{//

    [MethodImpl(MethodImplOptions.Synchronized)]
    public int Add(Order order)
    {
        if (DataSource.Orders.Exists(stat => stat.OrderUniqCode == order.OrderUniqCode))
            throw new AlreadyExistsException("order");

        order.OrderUniqCode = DataSource.Config.getRunOrder();
        DataSource.Orders.Add(order);
        return order.OrderUniqCode;
    }

    
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int uniqCode)
    {
        if (!DataSource.Orders.Exists(stat => stat.OrderUniqCode == uniqCode))
            throw new NotFoundException("order");
        
        Order temp = Get(uniqCode);
        DataSource.Orders.Remove(temp);
    }
    
    
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(Order order)
    {
        int index = DataSource.Orders.FindIndex(i => i.OrderUniqCode == order.OrderUniqCode);
        if (index == -1)
        {
            throw new NotFoundException("order");
        }
        DataSource.Orders[index] = order;
    }

    
    [MethodImpl(MethodImplOptions.Synchronized)]
    public Order Get(int uniqCode)
    {
        Order order = DataSource.Orders.Find(i => i.OrderUniqCode == uniqCode);
        return order.OrderUniqCode != default ? order : throw new NotFoundException("order");
    }

    
    public Order GetElementByCondition(Func<Order, bool>? cond)
    {
        return DataSource.Orders.Find(item => cond(item));
    }

    public IEnumerable<Order> GetAll(Func<Order, bool>? cond = null)
    {
        if (cond == null)
            return DataSource.Orders.Select(item => item);

        return DataSource.Orders.Where(item => cond(item));
    }
}
