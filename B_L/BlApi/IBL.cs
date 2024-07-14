using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;


namespace BlApi
{
    public interface IBL
    {
        public IOrder BO_Order { get; }
        public IBOCart BO_Cart { get; }
        public IBOProduct BO_Product { get; }

    }
}
