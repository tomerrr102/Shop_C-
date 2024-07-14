using BlImplementation;
using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using BlApi;



namespace B_L
{
    internal sealed class BL : IBL
    {
        internal static BL Instance { get; } = new BL();
        public BlApi.IOrder BO_Order => new BL_BO_Order();
        public IBOCart BO_Cart => new BL_BOCart();
        public IBOProduct BO_Product => new BL_BOProduct();

    }
}
