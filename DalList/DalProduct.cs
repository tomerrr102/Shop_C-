using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DO;
using static System.Collections.Specialized.BitVector32;
using DalApi;

namespace Dal;

internal class DalProduct : IProduct 
{
    [MethodImpl(MethodImplOptions.Synchronized)]
    public int Add(Product p)
    {
        if (DataSource.Products.Exists(stat => stat.CarID == p.CarID))
            throw new AlreadyExistsException("Prodect");

        DataSource.Products.Add(p);
        return p.CarID;
    }

    
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int id)
    {
        if (!DataSource.Products.Exists(stat => stat.CarID == id))
            throw new NotFoundException("Product");
        Product temp = Get(id);
        DataSource.Products.Remove(temp);
    }

    
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(Product p)
    {
        int index = DataSource.Products.FindIndex(i => i.CarID == p.CarID);
        if (index == -1)
        {
            throw new NotFoundException("Product");
        }
        DataSource.Products[index] = p;
    }

    
    [MethodImpl(MethodImplOptions.Synchronized)]
    public Product Get(int Id)
    {
        Product p = DataSource.Products.Find(i => i.CarID == Id);
        return p.CarID != default ? p : throw new NotFoundException("Product");
    }

    
    public Product GetElementByCondition(Func<Product, bool>? cond)
    {
        return DataSource.Products.Find(item => cond(item));
    } 

    public IEnumerable<Product> GetAll(Func<Product, bool>? cond = null)
    {        
        if(cond == null)
            return DataSource.Products.Select(item => item);
        
        return DataSource.Products.Where(item => cond(item));
    } 

}
