using Dal;
using System.Data.Common;

namespace Main // Note: actual namespace depends on the project name.
{
   
    public class DalMain
    {
        static DalProduct product=new DalProduct();
        static DalOrder order=new DalOrder();
        static DalOrderItem item=new DalOrderItem();
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Customer!\nplease choose one of the options below:");
            int choose=1;
            while (choose!=0)
            {
                Console.WriteLine("To exit choose 0.\n" +
                "To make Product operations pick 1.\n" +
                "To make Order operations Pick 2.\n" +
                "To make Order Item operations pick 3.");
                int.TryParse(Console.ReadLine(), out choose);
                switch (choose)
                {
                    case 0:
                        Console.WriteLine("GoodBye!");
                        break;
                    case 1:
                        {
                            Console.WriteLine("please pick an action:\n" +
                                "To add a Product pick a.\n" +
                                "To display a product with serial number pick b.\n" +
                                "To display a list of all products pick c.\n" +
                                "To update a product pick d.\n" +
                                "To delete a product pick e.\n" +
                                "To exit pick f.");
                            string action = Console.ReadLine();
                        switch (action)
                        {
                                case "a":
                                    {
                                        Console.WriteLine("please enter new product name");
                                        string name = Console.ReadLine();
                                        Console.WriteLine("enter prodcut price");
                                        double price;
                                        double.TryParse(Console.ReadLine(), out price);
                                        Console.WriteLine("please enter category:\n" +
                                            "1-Programming_languages,\n " +
                                            "2-Operating_systems,\n" +
                                            "3-Computer_architecture,\n" +
                                            "4-Cyber_security");
                                        int category;
                                        int.TryParse(Console.ReadLine(), out category);
                                        int inStock;
                                        Console.WriteLine("enter amount in stock.");
                                        int.TryParse(Console.ReadLine(), out inStock);
                                        DalMain.product= new DalProduct(0, name, price, (DO.Enums.Category)category, inStock);
                                        product.AddProduct();
                                        break;
                                    }
                                case "b":
                                    {
                                        Console.WriteLine("please enter product ID to display.");
                                        int ID;
                                        int.TryParse(Console.ReadLine(), out ID);
                                        product = new DalProduct();
                                        Console.WriteLine(product.DisplayProduct(ID));
                                        break;
                                    }
                                case "c":
                                    {
                                        DalProduct[] Products = new DalProduct().PrintProducts();
                                        foreach (DalProduct Product in Products)
                                        {
                                            if (Product!=null)
                                            {
                                                Console.WriteLine(Product);
                                            }
                                            else
                                            {
                                                break;
                                            }
                                        }                   
                                        break;
                                    }
                                case "d":
                                    {
                                        Console.WriteLine("please enter product id to update.");
                                        int ID;
                                        int.TryParse(Console.ReadLine(), out ID);
                                        Console.WriteLine("please enter new product name");
                                        string name = Console.ReadLine();
                                        Console.WriteLine("enter prodcut price");
                                        double price;
                                        double.TryParse(Console.ReadLine(), out price);
                                        Console.WriteLine("please enter category:\n" +
                                            "1-Programming_languages,\n " +
                                            "2-Operating_systems,\n" +
                                            "3-Computer_architecture,\n" +
                                            "4-Cyber_security\n");
                                        int category;
                                        int.TryParse(Console.ReadLine(), out category);
                                        int inStock;
                                        Console.WriteLine("enter amount in stock.");
                                        int.TryParse(Console.ReadLine(), out inStock);
                                        product = new DalProduct(0, name, price, (DO.Enums.Category)category, inStock);
                                        product.UpdateProduct(ID, name, price, (DO.Enums.Category)category, inStock);
                                        break;

                                    }
                                case "e":
                                    {
                                        Console.WriteLine("enter product ID to delete.");
                                        int ID;
                                        int.TryParse(Console.ReadLine(),out ID);
                                        product=new DalProduct();
                                        product.DeleteProduct(ID);
                                        break;
                                    }
                                case "f":
                                    break;
                                default:
                                    Console.WriteLine("invalid action.");
                                    break;
                            }
                            break; 
                        }
                    case 2:
                        {
                            Console.WriteLine("please pick an action:\n" +
                           "To add an order pick a.\n" +
                           "To display a order with serial number pick b.\n" +
                           "To display a list of all orders pick c.\n" +
                           "To update a ordeer pick d.\n" +
                           "To delete a order pick e.\n" +
                           "To exit pick f.");
                            string action = Console.ReadLine();
                            switch (action)
                            {
                                case "a":
                                    {
                                        Console.WriteLine("please enter customer name.");
                                        string name=Console.ReadLine();
                                        Console.WriteLine("please enter customer adress.");
                                        string adress=Console.ReadLine();
                                        Console.WriteLine("please enter customer email.");
                                        string email=Console.ReadLine();
                                        order=new DalOrder(0,name,email,adress);
                                        order.AddOrder();
                                        break;


                                    }
                                case "b":
                                    {
                                        Console.WriteLine("please enter order ID to display.");
                                        int ID;
                                        int.TryParse(Console.ReadLine(),out ID);
                                        Console.WriteLine(order.DisplayOrder(ID));
                                        break;
                                    }
                                case "c":
                                    {
                                        DalOrder[] Orders=new DalOrder().PrintOrders();
                                        foreach (DalOrder order in Orders)
                                        {
                                            if (order!=null)
                                            {
                                                Console.WriteLine(order);
                                            }
                                            else
                                            {
                                                break;
                                            }
                                        }
                                        
                                        break;
                                    }
                                case "d":
                                    {
                                        Console.WriteLine("please enter order Id to update.");
                                        int ID;
                                        int.TryParse(Console.ReadLine(), out ID);
                                        Console.WriteLine("please enter customer name.");
                                        string name = Console.ReadLine();
                                        Console.WriteLine("please enter customer adress.");
                                        string adress = Console.ReadLine();
                                        Console.WriteLine("please enter customer email.");
                                        string email = Console.ReadLine();
                                        order = new DalOrder(ID, name, email, adress);
                                        order.UpdateOrder();
                                        break;
                                    }
                                case "e":
                                    {
                                        Console.WriteLine("please enter order ID to delete.");
                                        int ID;
                                        int.TryParse(Console.ReadLine(), out ID);
                                        order.DeleteOrder(ID);
                                        break;
                                    }
                                case "f":
                                    break;
                                default:
                                    Console.WriteLine("invalid input");
                                    break;
                            }
                            break;
                        }
                    case 3:
                        {
                            Console.WriteLine("please pick an action:\n" +
                           "To add a Order Item pick a.\n" +
                           "To display a Order Item with serial number pick b.\n" +
                           "To display all Order Items in an order, pick c.\n" +
                           "To update an Order Item pick d.\n" +
                           "To delete an Order Item pick e.\n" +
                           "To exit pick f.");
                            string action = Console.ReadLine();
                            switch (action)
                            {
                                case "a":
                                    {
                                        Console.WriteLine("please enter product Id to purchase.");
                                        int ProductID;
                                        int.TryParse(Console.ReadLine(), out ProductID);
                                        Console.WriteLine("please enter order Id to add this item to");
                                        int OrderID;
                                        int.TryParse(Console.ReadLine(), out OrderID);
                                        Console.WriteLine("please enter amount.");
                                        int Amount;
                                        int.TryParse(Console.ReadLine(), out Amount);
                                        item= new DalOrderItem(ProductID, OrderID, Amount);
                                        item.AddOrderItem();
                                        break;
                                    }
                                case "b":
                                    {
                                        Console.WriteLine("please enter product Id to purchase.");
                                        int ProductID;
                                        int.TryParse(Console.ReadLine(), out ProductID);
                                        Console.WriteLine("please enter order Id to add this item to");
                                        int OrderID;
                                        int.TryParse(Console.ReadLine(), out OrderID);
                                        Console.WriteLine(item.DisplayOrderItem(OrderID,ProductID));
                                        break;
                                    }
                                case "c":
                                    {
                                        //TODO: when we can add logic we only return the specipic order items.
                                        Console.WriteLine("please enter order ID to display its items.");
                                        int OrderID;
                                        int.TryParse(Console.ReadLine(), out OrderID);
                                        DalOrderItem[] Items = item.PrintOrderItems();
                                        foreach (DalOrderItem item in Items)
                                        {
                                            if (item == null)
                                            {
                                                break;
                                            }
                                            if (item.Item.OrderId==OrderID)
                                            {
                                                Console.WriteLine(item);
                                            }
                                        }
                                        break;     
                                    }
                                case "d":
                                    {
                                        Console.WriteLine("please enter product Id to update.");
                                        int ProductID;
                                        int.TryParse(Console.ReadLine(), out ProductID);
                                        Console.WriteLine("please enter order Id of this item.");
                                        int OrderID;
                                        int.TryParse(Console.ReadLine(), out OrderID);
                                        Console.WriteLine("please enter amount.");
                                        int Amount;
                                        int.TryParse(Console.ReadLine(), out Amount);
                                        item = new DalOrderItem(ProductID, OrderID, Amount);
                                        item.UpdateOrderItem(OrderID,ProductID);
                                        break;
                                    }
                                case "e":
                                    {
                                        Console.WriteLine("please enter product Id to purchase.");
                                        int ProductID;
                                        int.TryParse(Console.ReadLine(), out ProductID);
                                        Console.WriteLine("please enter order Id to add this item to");
                                        int OrderID;
                                        int.TryParse(Console.ReadLine(), out OrderID);
                                        item.DeleteOrderItem(OrderID,ProductID);
                                        break;
                                    }
                                case "f":
                                    break;
                                default:
                                    break;
                            }
                            break;
                        }
                    default:
                        Console.WriteLine("invalid input.");
                        break;
                }
            }
        }
    }
}