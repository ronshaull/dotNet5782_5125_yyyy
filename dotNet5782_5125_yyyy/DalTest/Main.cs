using Dal;
using DalApi;
using DO;
using System.Data.Common;

namespace Main // Note: actual namespace depends on the project name.
{
   
    public class DalMain
    {
        static void Main(string[] args)
        {
            DalApi.IDal? dal = DalApi.Factory.Get();
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
                                        Product p=new Product();
                                        p.Name = name;
                                        p.InStock=inStock;
                                        p.Category = (Enums.Category)category;
                                        dal?.Product.Add(p);
                                        break;
                                    }
                                case "b":
                                    {
                                        Console.WriteLine("please enter product ID to display.");
                                        int ID;
                                        int.TryParse(Console.ReadLine(), out ID);
                                        Console.WriteLine(dal?.Product.Get(ID));
                                        break;
                                    }
                                case "c":
                                    {
                                        IEnumerable<Product?> Products =(IEnumerable<Product?>)dal?.Product.GetAll();
                                        foreach (Product Product in Products)
                                        {
                                            Console.WriteLine(Product);
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
                                        Product p = new Product();
                                        p.ID = ID;
                                        p.Name = name;
                                        p.InStock = inStock;
                                        p.Category = (Enums.Category)category;
                                        dal?.Product.Update(p);
                                        break;

                                    }
                                case "e":
                                    {
                                        Console.WriteLine("enter product ID to delete.");
                                        int ID;
                                        int.TryParse(Console.ReadLine(),out ID);
                                        dal?.Product.Delete(ID);
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
                                        Order o = new Order();
                                        o.CustomerName = name;
                                        o.CustomerEmail = email;
                                        o.CustomerAdress = adress;
                                        dal?.Order.Add(o);
                                        break;


                                    }
                                case "b":
                                    {
                                        Console.WriteLine("please enter order ID to display.");
                                        int ID;
                                        int.TryParse(Console.ReadLine(),out ID);
                                        Console.WriteLine(dal?.Order.Get(ID));
                                        break;
                                    }
                                case "c":
                                    {
                                        IEnumerable<Order?> Orders =(IEnumerable<Order?>)dal?.Order.GetAll();
                                        foreach (Order order in Orders)
                                        {
                                            Console.WriteLine(order);
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
                                        Order o = new Order();
                                        o.CustomerName = name;
                                        o.CustomerEmail = email;
                                        o.CustomerAdress = adress;
                                        dal?.Order.Update(o);
                                        break;
                                    }
                                case "e":
                                    {
                                        Console.WriteLine("please enter order ID to delete.");
                                        int ID;
                                        int.TryParse(Console.ReadLine(), out ID);
                                        dal?.Order.Delete(ID);
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
                                        OrderItem oi = new OrderItem();
                                        oi.OrderId=OrderID;
                                        oi.ProductId=ProductID;
                                        dal?.OrderItem.Add(oi);
                                        break;
                                    }
                                case "b":
                                    {/* NOTICE: we used to use 2 ids, but the ICRUD forced us to use first one and after that 
                                      * get the second one in inner function.
                                        Console.WriteLine("please enter product Id to purchase.");
                                        int ProductID;
                                        int.TryParse(Console.ReadLine(), out ProductID);*/
                                        Console.WriteLine("please enter order Id to add this item to");
                                        int OrderID;
                                        int.TryParse(Console.ReadLine(), out OrderID);
                                        Console.WriteLine(dal?.OrderItem.Get(OrderID));
                                        break;
                                    }
                                case "c":
                                    {
                                        /*
                                        //TODO: when we can add logic we only return the specipic order items.
                                        Console.WriteLine("please enter order ID to display its items.");
                                        int OrderID;
                                        int.TryParse(Console.ReadLine(), out OrderID);*/
                                        IEnumerable <OrderItem?> Items =(IEnumerable<OrderItem?>) dal?.OrderItem.GetAll();
                                        foreach (OrderItem item in Items)
                                        {
                                            Console.WriteLine(item);
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
                                        OrderItem oi = new OrderItem();
                                        oi.ProductId = ProductID;
                                        oi.OrderId = OrderID;
                                        oi.Amount=Amount;
                                        dal?.OrderItem.Update(oi);
                                        break;
                                    }
                                case "e":
                                    {
                                        Console.WriteLine("please enter Order item id to delete");
                                        int ID;
                                        int.TryParse(Console.ReadLine(), out ID);
                                        dal?.OrderItem.Delete(ID);
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