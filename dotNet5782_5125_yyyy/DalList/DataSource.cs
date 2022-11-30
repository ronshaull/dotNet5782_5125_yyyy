using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using DO;
using System.Collections.Generic;

namespace Dal;


internal static class DataSource
{
    //we use global static variables so we can change the if necessery with ease.
    public static int NUMBER_OF_PRODUCTS = 50;
    public static int NUMBER_OF_ORDERS = 100;
    public static int NUMBER_OF_ORDERITEMS = 200;
    #region fields-arrays of dalobjects.
    static readonly Random random = new Random();
    internal static List<DalProduct> _productlist = new List<DalProduct>();
    internal static List<DalOrder> _orderlist =new List<DalOrder>();
    internal static List<DalOrderItem> _orderItemslist=new List<DalOrderItem>();
    #endregion
    #region Config inner class
    internal static class Config
    {
        //IDs generator.
        private static int productId=0;

        public static int product_Id
        {
            get { return productId++; }
            
        }

        private static int orderId=0;

        public static int order_Id
        {
            get { return orderId++; }
        }

        private static int OrderItemID=0;

        public static int OrderItem_ID
        {
            get { return OrderItemID++; }
        }


    }
    #endregion
    #region Creating arrays functions
    /// <summary>
    /// function used to initialize all product type array.
    /// we are using txt files for products names, each product has a specific 
    /// </summary>
    private static void createProductArray()
    {
        string[] ComputerArchitecture = {"Digital Design and Computer Architecture",
                             "Schaum's Outline of Computer Architecture",
                             "Learning Computer Architecture with Raspberry Pi",
                             "Computer Architecture: A Quantitative Ap"};
        foreach (string line in ComputerArchitecture)
        {

            _productlist.Add( new DalProduct(Config.product_Id, line,
                random.NextDouble() + random.Next(10, 80), (Enums.Category)2, random.Next(0, 100) % 5));
        }
        string[] CyberSecurity = {"Cybersecurity: The Beginner's Guide",
                               "CompTIA Security+Get Certified Get Ahead",
                                "Cybersecurity For Dummies",
                                "The Art of Invisibility",
                                "Cybersecurity Essentials"};
        foreach (string line in CyberSecurity)
        {

            _productlist.Add(new DalProduct(Config.product_Id, line,
                random.NextDouble() + random.Next(10, 80), (Enums.Category)3, random.Next(0, 100) % 5));
        }   
        string[] OS = {"Operating Systems: Three Easy Pieces",
                          "Modern Operating Systems",
            "Understanding Operating Systems",
            "Operating Systems: Principles and Practice",
            "Operating System Concep"};
        foreach (string line in OS)
        {
            _productlist.Add( new DalProduct(Config.product_Id, line,
                random.NextDouble() + random.Next(10, 80), (Enums.Category)1, random.Next(0, 100) % 5));
        }
        string[] ProgrammingLanguges = {"Learn python",
            "Python for beginners",
            "Advanced Python",
            "Intro to C++","C++ guide",
            "C# and dotnet",
            "A tour of C++",
            "HTML & CSS"};
        foreach (string line in ProgrammingLanguges)
        {

            _productlist.Add( new DalProduct(Config.product_Id, line,
                random.NextDouble() + random.Next(10, 80), (Enums.Category)0, random.Next(0, 100) % 5));
        }

    }
    private static void createOrderArray()
    {
        for (int i = 0; i < random.Next(20,40); i++)
        {
            string Customer = "CustomerNo.";
            Customer += random.Next(0, 400).ToString();
            string Email = "";
            Email += Customer + "@gmail.com";
            _orderlist.Add( new DalOrder(Config.order_Id,
                Customer,
                Email,
                "israel"));
        }
    }
    private static void createOrderItemArray()
    {
        for (int i = 0; i < _orderlist.Count; i++)//first we set a product for each order.
        {
            int Product_Index=random.Next(0,_productlist.Count);//we first randomize a product.
            _orderItemslist.Add( new DalOrderItem(_productlist[Product_Index].product.ID,
                i,
                random.Next(1,5)
                ));
        }
        for (int i = 0; i < random.Next(10,20); i++)//we randomly add items to exisiting orders.
        {
            int Product_id = random.Next(0, _productlist.Count);
            int Order_Id = random.Next(0, _orderlist.Count);
            _orderItemslist.Add( new DalOrderItem(_productlist[Product_id].product.ID,
                Order_Id,
                random.Next(1, 5)
                ));
        }
    }           
    #endregion               
    #region ctor
    /// <summary>
    /// static ctor, witch will be called only once, and it will invoke s_Initialize() function
    /// that is responsiable for creating all the data.
    /// </summary>
    static DataSource()
    {
        s_Initialize();
    }
    /// <summary>
    /// function calles all the creating of data function.
    /// </summary>
    public static void s_Initialize()
    {
        createProductArray();
        createOrderArray();
        createOrderItemArray();
        }
    #endregion
}
