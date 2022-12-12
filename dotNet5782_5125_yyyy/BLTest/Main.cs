using BlApi;
using BO;
using Dal;
using System;

namespace BLTest
{
    internal class Program
    {
        static BlImplementation.BL BL = new BlImplementation.BL();
        static DalList dal = new DalList();
        static void Main(string[] args)
        {
            
            Console.WriteLine("Hello Customer!"+"\n"+"To create a new cart please enter following data:\n");
            string cname, cemail, cadress = "";
            Console.WriteLine("enter your name");
            cname=Console.ReadLine();
            Console.WriteLine("enter your email.");
            cemail=Console.ReadLine();
            Console.WriteLine("enter your adress");
            cadress=Console.ReadLine();
            BO.Cart cart = new BO.Cart() { CustomerName=cname,
            CustomerAdress=cadress,
            CustomerEmail=cemail};

            int choose = 1;
            while (choose != 0)
            {
                Console.WriteLine("To exit choose 0.\n" +
                "To make Product operations pick 1.\n" +
                "To make Order operations Pick 2.\n" +
                "To make Cart operations pick 3.");
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
                                "To display a product with serial number (manager screen) pick b.\n" +
                                "To display a product with serial number (customer screen) pick g.\n" +
                                "To display a list of all products pick c.\n" +
                                "To update a product pick d.\n" +
                                "To delete a product pick e.\n" +
                                "To exit pick f.");
                            string action = Console.ReadLine();
                        switch (action)
                        {
                                case "a":
                                    {
                                        try
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
                                            BO.Product p = new BO.Product()
                                            {
                                                Name = name,
                                                Category = (BO.Enums.Category)category,
                                                InStock = inStock,
                                                Price = price
                                            };
                                            BL.Product.Add(p);
                                            break;

                                        }
                                        catch (BO.GeneralEx e)
                                        {
                                            Console.WriteLine(e.Message + "\n" + e.GetType());
                                            break;
                                        }
                                    }
                                case "b":
                                    { 
                                        try
                                        {
                                            Console.WriteLine("please enter product ID to display.");
                                            int ID;
                                            if (!int.TryParse(Console.ReadLine(), out ID))
                                            {
                                                throw new BO.InvalidParamsEx();
                                            }
                                            Console.WriteLine(BL.Product.Get(ID));
                                            break;
                                        }
                                        catch (BO.InvalidParamsEx e)
                                        {
                                            Console.WriteLine(e.message+"\n"+e.GetType());
                                            break;
                                        }
                                        catch(BO.ObjectNotFoundEx e)
                                        {
                                            Console.WriteLine(e.message + "\n" + e.GetType());
                                            break;
                                        }
                                    }
                                case "c":
                                    {
                                        try
                                        {
                                            IEnumerable<BO.ProductForList> Products = BL.Product.GetAll();
                                            foreach (BO.ProductForList Product in Products)
                                            {
                                                Console.WriteLine(Product);
                                            }
                                        }
                                        catch (BO.EmptyListEx e)
                                        {
                                            Console.WriteLine(e.Message+"\n"+e.GetType());
                                            break;
                                        }
                                        
                                        break;
                                    }
                                case "d":
                                    {
                                        try
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
                                            BO.Product p = new BO.Product()
                                            {
                                                ID = ID,
                                                Name = name,
                                                Category = (BO.Enums.Category)category,
                                                InStock = inStock,
                                                Price = price
                                            };
                                            BL.Product.Update(p);
                                            break;
                                        }
                                        catch (BO.EmptyListEx e)
                                        {
                                            Console.WriteLine(e.message+"\n"+e.GetType());
                                            break;
                                        }
                                        catch(BO.ObjectNotFoundEx e)
                                        {
                                            Console.WriteLine(e.message+"\n"+e.GetType());
                                            break;
                                        }
                                    }
                                case "e":
                                    {
                                        try
                                        {
                                            Console.WriteLine("enter product ID to delete.");
                                            int ID;
                                            int.TryParse(Console.ReadLine(), out ID);
                                            BL.Product.Delete(ID);
                                            break;
                                        }
                                        catch (BO.ObjectNotFoundEx e)
                                        {
                                            Console.WriteLine(e.message + "\n" + e.GetType());
                                            break;
                                        }
                                        catch (BO.EmptyListEx e)
                                        {
                                            Console.WriteLine(e.message + "\n" + e.GetType());
                                            break;
                                        }
                                        catch (BO.ProductInOrderEx e)
                                        {
                                            Console.WriteLine(e.message + "\n" + e.GetType());
                                            break;
                                        }
                                    }
                                case "f":
                                    break;
                                case "g":
                                    {
                                        try
                                        {
                                            Console.WriteLine("please enter product ID to display.");
                                            int ID;
                                            if (!int.TryParse(Console.ReadLine(), out ID))
                                            {
                                                throw new BO.InvalidParamsEx();
                                            }
                                            Console.WriteLine(BL.Product.Get(ID,cart));
                                            break;
                                        }
                                        catch (BO.InvalidParamsEx e)
                                        {
                                            Console.WriteLine(e.message + "\n" + e.GetType());
                                            break;
                                        }
                                        catch (BO.ObjectNotFoundEx e)
                                        {
                                            Console.WriteLine(e.message + "\n" + e.GetType());
                                            break;
                                        }
                                    }
                                default:
                                    Console.WriteLine("invalid action.");
                                    break;
                            }
                            break;
                        }
                    case 2:
                        {
                            Console.WriteLine("please pick an action:\n" +
                           "To display a order with serial number(manager and customer screen) pick b.\n" +
                           "To display a list of all orders pick (manager screen) c.\n" +
                           "To update a order shipment date pick d.\n" +
                           "To update a order delivery date pick g.\n" +
                           "To track an order (manager screen) pick e.\n" +
                           "To exit pick f.");
                            string action = Console.ReadLine();
                            switch (action)
                            {
                                case "b":
                                    {
                                        try
                                        {
                                            Console.WriteLine("please enter order ID to display.");
                                            int ID;
                                            int.TryParse(Console.ReadLine(), out ID);
                                            Console.WriteLine(BL.Order.Get(ID));
                                            break;
                                        }
                                        catch (BO.ObjectNotFoundEx e)
                                        {
                                            Console.WriteLine(e.message+"\n"+e.GetType());
                                            break;
                                        }
                                    }
                                case "c":
                                    {
                                        IEnumerable<BO.OrderForList> Orders = BL.Order.GetAll();
                                        foreach (OrderForList order in Orders)
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
                                        Console.WriteLine(BL.Order.UpdateShipment(ID));
                                        break;
                                    }
                                case "e":
                                    {
                                        try
                                        {
                                            Console.WriteLine("please enter order ID to track.");
                                            int ID;
                                            int.TryParse(Console.ReadLine(), out ID);
                                            Console.WriteLine(BL.Order.OrderTracking(ID));
                                            break;
                                        }
                                        catch (BO.ObjectNotFoundEx e)
                                        {
                                            Console.WriteLine(e.message + "n" + e.GetType());
                                            break;
                                        }
                                        catch (BO.EmptyListEx e)
                                        {
                                            Console.WriteLine(e.message + "n" + e.GetType());
                                            break;
                                        }
                                    }
                                case "f":
                                    break;
                                case "g":
                                    {
                                        try
                                        {
                                            Console.WriteLine("enter order id to update delivery date.");
                                            int ID;
                                            int.TryParse(Console.ReadLine(), out ID);
                                            Console.WriteLine(BL.Order.UpdateDelivery(ID));
                                            break;
                                        }
                                        catch (BO.ObjectNotFoundEx e)
                                        {
                                            Console.WriteLine(e.message+"n"+e.GetType());
                                            break;
                                        }
                                        catch (BO.EmptyListEx e)
                                        {
                                            Console.WriteLine(e.message + "n" + e.GetType());
                                            break;
                                        }
                                    }
                                default:
                                    Console.WriteLine("invalid input");
                                    break;
                            }
                            break;
                        }
                    case 3:
                        {
                            Console.WriteLine("please pick an action:\n" +
                           "To add a Product to Cart pick a.\n" +
                           "TO update amount of a product in Cart pick b.\n" +
                           "To place an order by cart pick c.\n" +
                           "To exit pick f.");
                            string action = Console.ReadLine();
                            switch (action)
                            {
                                case "a":
                                    {
                                        try
                                        {
                                            Console.WriteLine("Enter product ID to add to cart.");
                                            int ID;
                                            int.TryParse(Console.ReadLine(), out ID);
                                            Console.WriteLine(BL.Cart.Add(cart, ID));
                                            break;
                                        }
                                        catch (BO.ObjectNotFoundEx e)
                                        {
                                            Console.WriteLine(e.message+"\n"+e.GetType);
                                            break;
                                        }
                                    }
                                case "b":
                                    {
                                        try
                                        {
                                            Console.WriteLine("Enter product ID to update its amount on the cart.");
                                            int ID;
                                            int.TryParse(Console.ReadLine(), out ID);
                                            Console.WriteLine("Enter new amount");
                                            int amount;
                                            int.TryParse(Console.ReadLine(), out amount);
                                            Console.WriteLine(BL.Cart.Update(cart,ID,amount));
                                            break;
                                        }
                                        catch (BO.ObjectNotFoundEx e)
                                        {
                                            Console.WriteLine(e.message + "\n" + e.GetType);
                                            break;
                                        }
                                        catch(BO.InsufficientStockEx e)
                                        {
                                            Console.WriteLine(e.message + "\n" + e.GetType);
                                            break;
                                        }
                                    }
                                case "c":
                                    {
                                        try
                                        {

                                            Console.WriteLine("attempting to place an order.");
                                            Console.WriteLine(BL.Cart.Buy(cart, cart.CustomerName, cart.CustomerEmail, cart.CustomerAdress));
                                            Console.WriteLine("Order was placed succsefully.");
                                            break;
                                        }
                                        catch (BO.GeneralEx e)
                                        {
                                            Console.WriteLine(e.Message+"\n"+e.GetType);
                                            break;
                                        }
                                        catch (BO.ObjectNotFoundEx e)
                                        {
                                            Console.WriteLine(e.Message + "\n" + e.GetType);
                                            break;      
                                        }
                                        catch(BO.CustomerDetailsEx e)
                                        {

                                            Console.WriteLine("itmes "+e.Message + "\n" + e.GetType);
                                            break;
                                        }
                                        catch (DalApi.EmptyListEx e)
                                        {
                                            Console.WriteLine(e.Message + "\n" + e.GetType);
                                            break;
                                        }
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