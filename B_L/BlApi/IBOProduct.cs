using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi
{
    public interface IBOProduct
    {
        public IEnumerable<BO.ProductForList> GetProductForLists();
        public BO.Product GetProduct(int id);
        public BO.ProductItem GetProdctItem(int id, BO.Cart cart);
        public void AddProduct(BO.Product BOproduct);
        public void DeleteProduct(int id);
        public void UpdateProduct(BO.Product BOproduct);
        public IEnumerable<BO.ProductForList> GetAllByCondition(Func<BO.ProductForList?, bool>? cond = null);

    }
}
