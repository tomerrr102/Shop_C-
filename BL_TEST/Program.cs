using BlApi;
using BO;
using System.Diagnostics;

namespace BL_Test;
enum EntityOptions { EXIT, TestProduct, TestOrder, TestOrderItem }
enum TestProductFuncs { EXIT, AddProduct, UpdateProduct, GetProduct, GetProductForLists, GetProdctItem, Delete }
enum TestCartFuncs { EXIT, AddItemToCart, UpdateProductAmount, EnterCart }
enum TestOrderFuncs { EXIT, OrderList, OrderDetails, UpdateSendDeliver, UpdateOrderDeliver, OrderTrack }


class Program
{
    //public static B_L.BL BL_func = new B_L.BL();
    private static IBL BL_func = Factory.GetBL();

    static public void TestProduct()
    {

        Cart cart = new Cart();

        Console.WriteLine("Please enter your name for your new cart");
        string Name = Console.ReadLine();

        Console.WriteLine("Please enter your email:");
        string Email = Console.ReadLine();

        Console.WriteLine("Please Enter your address");
        string Adress = Console.ReadLine();

        cart.CustomerName = Name;
        cart.CartAdress = Adress;
        cart.CartMail = Email;


        int choice;
        do
        {
            Console.WriteLine(@"
choose from the following options:
0. EXIT.
1. Test Add Product.
2. Test Update Product.
3. Test GetProduct.
4. Test Get ProductForLists.
5. Test Get ProdctItem. 
6. Test Delete Product.
Your choice:");
            int.TryParse(Console.ReadLine(), out choice);
            switch ((TestProductFuncs)choice)
            {
                case TestProductFuncs.AddProduct:
                    Console.WriteLine(@"
You have selected to add a new Product.
Please enter an ID number for the new Product: ");
                    int.TryParse(Console.ReadLine(), out int newProductID);

                    Console.WriteLine("Please enter the name of the Product:");
                    string newProductName = Console.ReadLine();

                    Console.WriteLine("Enter the category of the new Product");
                    int.TryParse(Console.ReadLine(), out int newProducCategory);

                    Console.WriteLine("Enter the Price of the Product: ");
                    double.TryParse(Console.ReadLine(), out double newProductPrice);

                    Console.WriteLine("Enter the number of products left in stock: ");
                    int.TryParse(Console.ReadLine(), out int newProductInStock);

                    Product newProduct = new Product
                    {

                        CarID = newProductID,
                        CarName = newProductName,
                        CarType = (CarCategory)newProducCategory,
                        CarPrice = newProductPrice,
                        CarAmount = newProductInStock
                    };

                    try
                    {

                        BL_func.BO_Product.AddProduct(newProduct);
                        Console.WriteLine("sucsess");
                    }

                    catch (AlreadyExistsException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    catch (InvalidInputException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                case TestProductFuncs.UpdateProduct:
                    Console.WriteLine("Please enter an ID number for update product");
                    int.TryParse(Console.ReadLine(), out int ProductUpdateID);

                    Console.WriteLine("Please enter the update name of the Product:");
                    string newUpdateProductName = Console.ReadLine();

                    Console.WriteLine("Enter the category of the new Product");
                    int.TryParse(Console.ReadLine(), out int newUpdateProducCategory);

                    Console.WriteLine("Enter the Price of the Product: ");
                    double.TryParse(Console.ReadLine(), out double newUpdateProductPrice);

                    Console.WriteLine("Enter the number of products left in stock: ");
                    int.TryParse(Console.ReadLine(), out int newUpdateProductInStock);

                    Product UpdateProduct = new Product
                    {
                        CarID = ProductUpdateID,
                        CarName = newUpdateProductName,
                        CarType = (CarCategory)newUpdateProducCategory,
                        CarPrice = newUpdateProductPrice,
                        CarAmount = newUpdateProductInStock
                    };

                    try
                    {
                        BL_func.BO_Product.UpdateProduct(UpdateProduct);
                        Console.WriteLine("sucsess");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;

                case TestProductFuncs.GetProduct:
                    Console.WriteLine(@"Please enter an ID number for the get Product: ");
                    int.TryParse(Console.ReadLine(), out int ProductID);

                    try
                    {
                        Console.WriteLine(BL_func.BO_Product.GetProduct(ProductID));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;

                case TestProductFuncs.GetProductForLists:
                    foreach (var item in BL_func.BO_Product.GetProductForLists())
                    {
                        Console.WriteLine(item);
                    }
                    break;

                case TestProductFuncs.GetProdctItem:
                    Console.WriteLine(@"Please enter an ID number for the get ProductItem: ");
                    int.TryParse(Console.ReadLine(), out int ProductItemID);

                    try
                    {
                        Console.WriteLine(BL_func.BO_Product.GetProdctItem(ProductItemID,cart)); //what cart to put here?
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                case TestProductFuncs.Delete:
                    Console.WriteLine(@"Please enter an ID number to delete the Product: ");
                    int.TryParse(Console.ReadLine(), out int delProductID);

                    try
                    {
                        BL_func.BO_Product.DeleteProduct(delProductID);
                        Console.WriteLine("secsess");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                default:
                    break;
            }
        } while (choice != 0);
    }
    static public void TestCart()
    {
        Cart cart = new Cart();

        Console.WriteLine("Please enter your name for your new cart");
        string Name = Console.ReadLine();

        Console.WriteLine("Please enter your email:");
        string Email = Console.ReadLine();

        Console.WriteLine("Please Enter your address");
        string Adress = Console.ReadLine();

        cart.CustomerName = Name;
        cart.CartAdress = Adress;
        cart.CartMail = Email;


        int choice;
        do
        {
            Console.WriteLine(@"
choose from the following options:
0. EXIT.
1. Add Product To Cart.
2. Test Update Product Amount.
3. Test EnterCart.
Your choice:");
            int.TryParse(Console.ReadLine(), out choice);

            switch ((TestCartFuncs)choice)
            {
                case TestCartFuncs.AddItemToCart:

                    Console.WriteLine("Enter your productID");
                    int.TryParse(Console.ReadLine(), out int id);

                    try
                    {
                        BL_func.BO_Cart.AddItemToCart(cart, id);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }

                    break;
                case TestCartFuncs.UpdateProductAmount:

                    Console.WriteLine("Enter the product id");
                    int.TryParse(Console.ReadLine(), out int ProductID);

                    Console.WriteLine("Enter the new amount");
                    int.TryParse(Console.ReadLine(), out int newAmount);

                    try
                    {
                        BL_func.BO_Cart.UpdateProductAmount(cart, ProductID, newAmount);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }

                    break;
                case TestCartFuncs.EnterCart:
                    try
                    {
                        cart = BL_func.BO_Cart.EnterCart(cart); // צריך להרחיב/
                        Console.WriteLine("Your cart details:");
                        Console.WriteLine(cart);
                    }
                    catch (InvalidInputException ex)
                    {
                        Console.WriteLine(ex);
                    }
                    catch (NotFoundException ex)
                    {
                        Console.WriteLine(ex);
                    }


                    break;
                default:
                    break;
            }

        } while (choice != 0);
    }

    static public void TestOrder()
    {
        int choice;
        do
        {
            Console.WriteLine(@"
choose from the following options:
0. EXIT.
1. Test Order List.
2. Test Order Details.
3. Test Update Send Deliver.
4. Test Update Order Deliver.
5. Test Order Track.
Your choice:");
            int.TryParse(Console.ReadLine(), out choice);

            switch ((TestOrderFuncs)choice)
            {
                case TestOrderFuncs.OrderList:

                    try
                    {
                        foreach (var item in BL_func.BO_Order.GetOrderForList())
                        {
                            Console.WriteLine(item);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }

                    break;
                case TestOrderFuncs.OrderDetails:

                    Console.WriteLine("Please enter an ID number for the get ProductItem: ");
                    int.TryParse(Console.ReadLine(), out int OrderID);

                    try
                    {
                        Console.WriteLine(BL_func.BO_Order.GetOrder(OrderID));
                    }
                    catch (NotFoundException ex)
                    {
                        Console.WriteLine(ex);
                    }

                    break;
                case TestOrderFuncs.UpdateSendDeliver:

                    Console.WriteLine("Please enter an order ID to update that order send: ");
                    int.TryParse(Console.ReadLine(), out int id);

                    try
                    {
                        Console.WriteLine(BL_func.BO_Order.UpdateOrder(id));
                    }
                    catch (NotFoundException ex)
                    {
                        Console.WriteLine(ex);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    break;
                case TestOrderFuncs.UpdateOrderDeliver:
                    Console.WriteLine("Please enter an order ID to update that order supply: ");
                    int.TryParse(Console.ReadLine(), out int supply_id);

                    try
                    {
                        Console.WriteLine(BL_func.BO_Order.UpdateOrderDeliver(supply_id));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }

                    break;
                case TestOrderFuncs.OrderTrack:
                    Console.WriteLine("Please enter an order ID to update that order supply: ");
                    int.TryParse(Console.ReadLine(), out int tracking_id);

                    try
                    {
                        //Console.WriteLine(BL_func.BO_Order.OrderTrack(tracking_id).Status);
                        Console.WriteLine(BL_func.BO_Order.OrderTrack(tracking_id));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    break;
                default:
                    break;
            }

        } while (choice != 0);
    }
    public static void Main(string[] args)
    {
        int choice;
        do
        {
            Console.WriteLine(@"
choose from the following options (type the selected number): 
0. EXIT
1. to test Product.
2. to test Cart.
3. to test Order.
Your choice: ");
            int.TryParse(Console.ReadLine(), out choice);

            switch ((EntityOptions)choice)
            {
                case EntityOptions.TestProduct:
                    TestProduct();
                    break;

                case EntityOptions.TestOrder:
                    TestCart();
                    break;

                case EntityOptions.TestOrderItem:
                    TestOrder();
                    break;
                default:
                    break;
            }
        } while (choice != 0);
    }
}
