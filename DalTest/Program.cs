using Dal;
using DO;
using DalApi;
using static DO.Enums;



namespace Test;

enum EntityOptions { EXIT, TestProduct, TestOrder, TestOrderItem }
enum TestFuncOption { EXIT, TestAdd, TestUpdate, TestGet, TestGetList, TestDelete }

class Program
{
    //public static Dal.DalList dalList = new Dal.DalList();
    static IDal dalList = DalApi.Factory.Get();


    static public void TestProduct()
    {
        int choice;
        do
        {
            Console.WriteLine(@"
choose from the following options:
0. EXIT.
1. Test Add.
2. Test Update.
3. Test Get.
4. Test Get List.
5. Test Delete
Your choice:");
            int.TryParse(Console.ReadLine(), out choice);

            switch ((TestFuncOption)choice)
            {
                case TestFuncOption.TestAdd:
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
                        dalList.Product.Add(newProduct);
                        Console.WriteLine("secsess");
                    }
                    catch (AlreadyExistsException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    break;
                case TestFuncOption.TestUpdate:
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

                    Product newUpdateProduct = new Product
                    {
                        CarID = ProductUpdateID,
                        CarName = newUpdateProductName,
                        CarType = (CarCategory)newUpdateProducCategory,
                        CarPrice = newUpdateProductPrice,
                        CarAmount = newUpdateProductInStock
                    };

                    try
                    {
                        dalList.Product.Update(newUpdateProduct);
                        Console.WriteLine("secsess");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }


                    break;
                case TestFuncOption.TestGet:
                    Console.WriteLine(@"Please enter an ID number for the get Product: ");
                    int.TryParse(Console.ReadLine(), out int ProductID);

                    try
                    {
                        Console.WriteLine(dalList.Product.Get(ProductID));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                case TestFuncOption.TestGetList:
                    foreach (var item in dalList.Product.GetAll(item => item.CarID > 0))
                    {
                        Console.WriteLine(item);
                    }
                    break;
                case TestFuncOption.TestDelete:


                    Console.WriteLine(@"Please enter an ID number to delete the Product: ");
                    int.TryParse(Console.ReadLine(), out int delProductID);

                    try
                    {
                        dalList.Product.Delete(delProductID);
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

    static public void TestOrder()
    {
        int choice;
        do
        {
            Console.WriteLine(@"
choose from the following options:
0. EXIT.
1. Test Add.
2. Test Update.
3. Test Get.
4. Test Get List.
5. Test Delete
Your choice:");
            int.TryParse(Console.ReadLine(), out choice);

            switch ((TestFuncOption)choice)
            {
                case TestFuncOption.TestAdd:
                    Console.WriteLine(@"
You have selected to add a new Order.
Please enter an ID number for the new Order: ");
                    int.TryParse(Console.ReadLine(), out int newOrderUniqCode);

                    Console.WriteLine("Please enter the name of the Order:");
                    string newOrderCustomerName = Console.ReadLine();

                    Console.WriteLine("Enter the Email of the new Order");
                    string newOrderCustomerEmail = Console.ReadLine();

                    Console.WriteLine("Enter the Price of the Order: ");
                    double.TryParse(Console.ReadLine(), out double newOrderPrice);

                    Console.WriteLine("Enter the address of the customer: ");
                    string newOrderCustomerAddress = Console.ReadLine();

                    Order newOrder = new Order
                    {
                        OrderUniqCode = newOrderUniqCode,
                        OrderCustomerName = newOrderCustomerName,
                        OrderCustomerEmail = newOrderCustomerEmail,
                        OrderCustomerAddress = newOrderCustomerAddress,
                        OrderCreationDate = DateTime.Now,
                        OrderShipDate = DateTime.Now,
                        OrderDeliveryDate = DateTime.Now,
                    };

                    try
                    {
                        dalList.Order.Add(newOrder);
                        Console.WriteLine("secsess");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    break;
                case TestFuncOption.TestUpdate:
                    Console.WriteLine("Please enter an ID number for update product");
                    int.TryParse(Console.ReadLine(), out int UpdateOrderUniqCode);

                    Console.WriteLine("Please enter new name of customer:");
                    string UpdateOrderCustomerName = Console.ReadLine();

                    Console.WriteLine("Enter the Email of the new customer");
                    string UpdateOrderCustomerEmail = Console.ReadLine();

                    Console.WriteLine("Enter the Price of the new Order: ");
                    double.TryParse(Console.ReadLine(), out double UpdateOrderPrice);

                    Console.WriteLine("Enter new address for the customer: ");
                    string UpdateOrderCustomerAddress = Console.ReadLine();

                    Order newUpdateProduct = new Order
                    {
                        OrderUniqCode = UpdateOrderUniqCode,
                        OrderCustomerName = UpdateOrderCustomerName,
                        OrderCustomerEmail = UpdateOrderCustomerEmail,
                        OrderCustomerAddress = UpdateOrderCustomerAddress,
                        OrderCreationDate = DateTime.Now,
                        OrderShipDate = DateTime.Now,
                        OrderDeliveryDate = DateTime.Now,
                    };

                    try
                    {
                        dalList.Order.Update(newUpdateProduct);
                        Console.WriteLine("secsessfully updated");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }


                    break;
                case TestFuncOption.TestGet:
                    Console.WriteLine(@"
Please enter an ID number for the get Order: ");
                    int.TryParse(Console.ReadLine(), out int OrderUniqCode);

                    try
                    {
                        Console.WriteLine(dalList.Order.Get(OrderUniqCode));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                case TestFuncOption.TestGetList:
                    foreach (var item in dalList.Order.GetAll())
                    {
                        Console.WriteLine(item);
                    }
                    break;
                case TestFuncOption.TestDelete:

                    Console.WriteLine(@"Please enter an ID number for the get Order: ");
                    int.TryParse(Console.ReadLine(), out int delOrderID);

                    try
                    {
                        dalList.Order.Delete(delOrderID);
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

    static public void TestOrderItem()
    {
        int choice;
        int.TryParse(Console.ReadLine(), out choice);
        do
        {
            Console.WriteLine(@"
choose from the following options:
0. EXIT.
1. Test Add.
2. Test Update.
3. Test Get.
4. Test Get List.
5. Test Delete
Your choice:");
            int.TryParse(Console.ReadLine(), out choice);

            switch ((TestFuncOption)choice)
            {
                case TestFuncOption.TestAdd:
                    Console.WriteLine(@"
You have selected to add a new OrderItem.
Please enter an ID number for the new OrderItem: ");
                    //int.TryParse(Console.ReadLine(), out int newOrderItemCode);

                    Console.WriteLine("Enter the product ID of the new OrderItem");
                    int.TryParse(Console.ReadLine(), out int newOrderID);

                    Console.WriteLine("Enter the product ID of the new OrderItem");
                    int.TryParse(Console.ReadLine(), out int newProductID);

                    Console.WriteLine("Enter the Price of the OrderItem: ");
                    double.TryParse(Console.ReadLine(), out double newOrderProductPrice);

                    Console.WriteLine("Enter the amount of this OrderItem: ");
                    int.TryParse(Console.ReadLine(), out int newOrderItemAmount);

                    OrderItem newOrderItem = new OrderItem
                    {

                        OrderID = newOrderID,
                        ProductID = newProductID,
                        OrderProductPrice = newOrderProductPrice,
                        OrderItemAmount = newOrderItemAmount
                    };

                    try
                    {
                        dalList.OrderItem.Add(newOrderItem);
                        Console.WriteLine("secsess");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    break;
                case TestFuncOption.TestUpdate:
                    Console.WriteLine("Please enter an ID number for update OrderItem");
                    int.TryParse(Console.ReadLine(), out int UpdateOrderID);

                    Console.WriteLine("Please enter the ID of the OrderItem:");
                    int.TryParse(Console.ReadLine(), out int UpdateOrderItemCode);

                    Console.WriteLine("Enter the product ID of the new OrderItem");
                    int.TryParse(Console.ReadLine(), out int UpdateProductID);

                    Console.WriteLine("Enter the Price of the OrderItem: ");
                    double.TryParse(Console.ReadLine(), out double UpdateOrderProductPrice);

                    Console.WriteLine("Enter the amount of this OrderItem: ");
                    int.TryParse(Console.ReadLine(), out int UpdateOrderItemAmount);

                    OrderItem UpdateOrderItem = new OrderItem
                    {
                        OrderItemUniqCode = UpdateOrderItemCode,
                        OrderID = UpdateOrderID,
                        ProductID = UpdateProductID,
                        OrderProductPrice = UpdateOrderProductPrice,
                        OrderItemAmount = UpdateOrderItemAmount,
                    };

                    try
                    {
                        dalList.OrderItem.Update(UpdateOrderItem);
                        Console.WriteLine("secsess");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }


                    break;
                case TestFuncOption.TestGet:
                    Console.WriteLine(@"
Please enter an ID number for the get OrderItem: ");
                    int.TryParse(Console.ReadLine(), out int OrderItemUniqCode);

                    try
                    {
                        Console.WriteLine(dalList.OrderItem.Get(OrderItemUniqCode));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                case TestFuncOption.TestGetList:
                    foreach (var item in dalList.OrderItem.GetAll())
                    {
                        Console.WriteLine(item);
                    }
                    break;
                case TestFuncOption.TestDelete:

                    Console.WriteLine(@"Please enter an ID number for delete orderItem: ");
                    int.TryParse(Console.ReadLine(), out int delOrderItemID);

                    try
                    {
                        dalList.OrderItem.Delete(delOrderItemID);
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

    static void Main(string[] args)
    {
        int choice;
        do
        {
            Console.WriteLine(@"
choose from the following options (type the selected number): 
0. EXIT
1. to test Product.
2. to test Order.
3. to test OrderItem.
Your choice: ");
            int.TryParse(Console.ReadLine(), out choice);

            switch ((EntityOptions)choice)
            {
                case EntityOptions.TestProduct:
                    TestProduct();
                    break;

                case EntityOptions.TestOrder:
                    TestOrder();
                    break;

                case EntityOptions.TestOrderItem:
                    TestOrderItem();
                    break;
                default:
                    break;
            }
        } while (choice != 0);
    }
}

