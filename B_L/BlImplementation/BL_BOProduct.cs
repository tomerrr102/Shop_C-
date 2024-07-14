using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using DalList;
using BlApi;


namespace BlImplementation
{
    internal class BL_BOProduct : IBOProduct
    {
       //private DalApi.IDal iDal = new Dal.DalList();

        private IDal dal = DalApi.Factory.Get();



        public IEnumerable<BO.ProductForList> GetProductForLists()
        {
            IEnumerable<BO.ProductForList> productForList = new List<BO.ProductForList>();

            return from item in dal.Product.GetAll()
                   select new BO.ProductForList()
                   {
                       CarID = item.CarID,
                       CarName = item.CarName,
                       CarPrice = item.CarPrice,
                       CarType = (BO.CarCategory)item.CarType
                   };
        }
        public BO.Product GetProduct(int id)
        {
            //id invalid or empty list 
            if (id < 0) throw new BO.InvalidInputException("id");

            BO.Product product = new BO.Product();

            foreach (var item in dal.Product.GetAll())
            {
                if (item.CarID == id)
                {
                    product.CarID = item.CarID;
                    product.CarName = item.CarName;
                    product.CarPrice = item.CarPrice;
                    product.CarType = (BO.CarCategory)item.CarType;
                    product.CarAmount = item.CarAmount;
                    return product;
                }
            }
            throw new BO.NotFoundException("product");
        }

        public BO.ProductItem GetProdctItem(int id, BO.Cart cart) // why we adding a parameter of cart?
        {
            //id invalid or empty list 
            if (id < 0) throw new BO.InvalidInputException("id");

            BO.ProductItem ProductItem = new BO.ProductItem();

            DO.Product p = dal.Product.Get(id);
            ProductItem.CarPrice = p.CarPrice;
            ProductItem.CarType = (BO.CarCategory?)p.CarType;
            ProductItem.CarAmount = 0; // אם המוצר לא בסל קניות אז הכמות שווה ל 0
            ProductItem.CarID = p.CarID;
            ProductItem.CarName = p.CarName;
            ProductItem.InStock = p.CarAmount > 0 ? true : false;


            foreach(var item in cart.items)
            {
                if(id == item.ProductID)
                {
                    ProductItem.CarAmount = item.OrderItemAmount;
                }
            }

            return ProductItem;

            //throw new BO.NotFoundException("product item");
        }

        public void AddProduct(BO.Product BOproduct)
        {
            if (BOproduct.CarID <= 0) throw new BO.InvalidInputException("id");
            if (BOproduct.CarName == "") throw new BO.InvalidInputException("name");
            if (BOproduct.CarPrice <= 0) throw new BO.InvalidInputException("price");
            if (BOproduct.CarAmount <= 0) throw new BO.InvalidInputException("not in stock");

            foreach (var item in dal.Product.GetAll())
            {
                if (item.CarID == BOproduct.CarID)
                {
                    throw new BO.AlreadyExistsException("product item");
                }
            }

            DO.Product Dalproduct = new DO.Product();

            Dalproduct.CarID = BOproduct.CarID;
            Dalproduct.CarName = BOproduct.CarName;
            Dalproduct.CarPrice = BOproduct.CarPrice;
            Dalproduct.CarAmount = BOproduct.CarAmount;
            Dalproduct.CarType = (DO.Enums.CarCategory)BOproduct.CarType;

            dal.Product.Add(Dalproduct);
        }

        public void DeleteProduct(int id)
        {

            foreach (var item in dal.Order.GetAll())
            {
                if (id == item.OrderUniqCode)
                {
                    throw new BO.AlreadyExistsException("order");
                }
            }

            try
            {
                dal.Product.Delete(id);
            }
            catch (BO.NotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void UpdateProduct(BO.Product BOproduct)
        {
            if (BOproduct.CarID <= 0) throw new BO.InvalidInputException("id");
            if (BOproduct.CarName == "") throw new BO.InvalidInputException("name");
            if (BOproduct.CarPrice <= 0) throw new BO.InvalidInputException("price");
            if (BOproduct.CarAmount <= 0) throw new BO.InvalidInputException("not in stock");

            // convert to "DO product"
            DO.Product Dalproduct = new DO.Product();

            Dalproduct.CarID = BOproduct.CarID;
            Dalproduct.CarName = BOproduct.CarName;
            Dalproduct.CarPrice = BOproduct.CarPrice;
            Dalproduct.CarType = (DO.Enums.CarCategory)BOproduct.CarType;
            Dalproduct.CarAmount = BOproduct.CarAmount;


            dal.Product.Update(Dalproduct);


        }
            
        public IEnumerable<BO.ProductForList> GetAllByCondition(Func<BO.ProductForList, bool>? cond = null)
        {
            
            
            List<BO.ProductForList> lst = new List<BO.ProductForList>();
            
            
                   foreach(var item in dal.Product.GetAll())
                   lst.Add(new BO.ProductForList
                   {
                       CarID = item.CarID,
                       CarName = item.CarName,
                       CarPrice = item.CarPrice,
                       CarType = (BO.CarCategory)item.CarType
                   });

            if (cond == null)
                return lst.Select(item => item);

            return lst.Where(item => cond(item));

        }    
    }
}
