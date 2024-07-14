using BO;
using Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class BL
    {
        public DalProduct DalProduct = new DalProduct();
        public IEnumerable<ProductForList> GetproductForLists()
        {
            IEnumerable<ProductForList> productForList = new List<ProductForList>();
            foreach (var items in DalProduct.GetProductsList())
            {
                productForList.Append(new ProductForList
                {
                    CarID = items.CarID,
                    CarName = items.CarName,
                    CarPrice = items.CarPrice,
                    CarType = (CarCategory)items.CarType
                });
            }
            return productForList;
        }
    }
}
