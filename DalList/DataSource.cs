using DO;
using static DO.Enums;
namespace Dal;

internal static class DataSource
{
    
    static DataSource()
    {
        s_Initialize();
    }
   
    //lists of all the structs
    public static List <Product> Products = new List<Product>(50);
    public static List <Order> Orders = new List <Order> (100);
    public static List <OrderItem> OrderItems = new List<OrderItem>(200);


    internal static Random random = new Random();
    internal class Config
    {
        private static int RunOrder = 1000;
        private static int RunOrderItems = 1000;
                             
        public static int getRunOrder()
        {
            return RunOrder++;
        }
        public static int getRunOrderItems()
        {
            return RunOrderItems++; 
        }
    }

    public static void CreateProducts()
    {

        int[] Years = new int[] {2010, 2011, 2012, 2013, 2014, 2015, 2016, 2017,2018,2019};
                            
        
        for (int i = 0; i < 10; i++)
        {
            Products.Add(new Product
            {
                CarID = random.Next(100000,999999),
                CarName = ((CarCompanys)random.Next(0, 4)).ToString() + " Year " + Years[i],
                CarType = (CarCategory)random.Next(0, 4),
                CarPrice = random.Next(100000) + 40000, // 40,000 <= price <= 100,000
                CarAmount = random.Next(2,4)
            });  
        }
        
        Products.Add(new Product
        {
            CarID = 1111,
            CarName = ((CarCompanys)random.Next(0, 4)).ToString() + " Year " + 2020,
            CarType = (CarCategory)random.Next(0, 4),
            CarPrice = random.Next(100000) + 40000, // 40,000 <= price <= 100,000
            CarAmount = random.Next(15, 30)
        });
    }
    public static void CreateOrders()
    {
        for (int i = 0; i < 10; i++)
        {
            Orders.Add(new Order
            {
                OrderCustomerAddress = ((Adresses)random.Next(0, 7)).ToString(),
                OrderUniqCode = Config.getRunOrder(),
                OrderCustomerName = ((Names)i).ToString(),
                OrderCustomerEmail = ((Names)i).ToString() + "@gmail.com", 
                OrderCreationDate = DateTime.Now,
                OrderDeliveryDate = null,
                OrderShipDate = null,
            }); 
        }
    }
    public static void CreateOrderItems() // an item in the order
    {
        for (int i = 0; i < 10; i++)
        {
            OrderItems.Add(new OrderItem
            {
                OrderItemUniqCode = Config.getRunOrderItems(),
                OrderID = Orders[i].OrderUniqCode, 
                ProductID = Products[i].CarID,
                OrderProductPrice = Products[i].CarPrice,
                OrderItemAmount = random.Next(1,3)
            });
        }
    }
    public static void s_Initialize()
    {
        CreateProducts();
        CreateOrders();
        CreateOrderItems();
    }
}
