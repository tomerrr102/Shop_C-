﻿using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    public interface IDal
    {
     public IProduct Product { get;}
     public IOrder Order { get; }
     public IOrderItem OrderItem { get; }

    }
}
