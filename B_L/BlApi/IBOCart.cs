using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi
{
    public interface IBOCart
    {
        public BO.Cart AddItemToCart(BO.Cart cart,int OrderItemId);
        public BO.Cart UpdateProductAmount(BO.Cart cart , int orderItemId , int newAmount);
        public BO.Cart EnterCart(BO.Cart cart);


    }
}
