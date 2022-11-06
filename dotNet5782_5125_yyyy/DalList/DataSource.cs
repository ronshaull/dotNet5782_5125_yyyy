using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using DO;

namespace Dal;


internal static class DataSource
{
    //we use global static variables so we can change the if necessery with ease.
    public static int NUMBER_OF_PRODUCTS = 50;
    public static int NUMBER_OF_ORDERS = 100;
    public static int NUMBER_OF_ORDERITEMS = 200;
    #region fields-arrays of dalobjects.
    static readonly Random random = new Random();
    internal static DalProduct[] _productArray = new DalProduct[NUMBER_OF_PRODUCTS];
    internal static DalOrder[] _orderArray = new DalOrder[NUMBER_OF_ORDERS];
    internal static DalOrderItem[] _orderItemsArray=new DalOrderItem[NUMBER_OF_ORDERITEMS];
    #endregion
    #region Config inner class
    internal static class Config
    {
        //next open slot in each array indexers.
        internal static int orderindex=0;
        internal static int orderItemindex = 0;
        internal static int productIndex = 0;
        //IDs generator.
        private static int productId=0; //TODO: later change it to 0, and used formated string to print it as 6 figures.

        public static int product_Id
        {
            get { return productId++; }
            
        }

        private static int orderId=0;

        public static int order_Id
        {
            get { return orderId++; }
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

            _productArray[Config.productIndex] = new DalProduct(Config.product_Id, line,
                random.NextDouble() + random.Next(10, 80), (Enums.Category)2, random.Next(0, 100) % 5);
            Config.productIndex++;
        }
        string[] CyberSecurity = {"Cybersecurity: The Beginner's Guide",
                               "CompTIA Security+Get Certified Get Ahead",
                                "Cybersecurity For Dummies",
                                "The Art of Invisibility",
                                "Cybersecurity Essentials"};
        foreach (string line in CyberSecurity)
        {

            _productArray[Config.productIndex] = new DalProduct(Config.product_Id, line,
                random.NextDouble() + random.Next(10, 80), (Enums.Category)3, random.Next(0, 100) % 5);
            Config.productIndex++;
        }
        string[] OS = {"Operating Systems: Three Easy Pieces",
                          "Modern Operating Systems",
            "Understanding Operating Systems",
            "Operating Systems: Principles and Practice",
            "Operating System Concep"};
        foreach (string line in OS)
        {

            _productArray[Config.productIndex] = new DalProduct(Config.product_Id, line,
                random.NextDouble() + random.Next(10, 80), (Enums.Category)1, random.Next(0, 100) % 5);
            Config.productIndex++;
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

            _productArray[Config.productIndex] = new DalProduct(Config.product_Id, line,
                random.NextDouble() + random.Next(10, 80), (Enums.Category)0, random.Next(0, 100) % 5);
            Config.productIndex++;
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
            _orderArray[Config.orderindex] = new DalOrder(Config.order_Id,
                Customer,
                Email,
                "israel");
            Config.orderindex++;
        }
    }
    private static void createOrderItemArray()
    {
        for (int i = 0; i < Config.orderindex; i++)//first we set a product for each order.
        {
            int Product_Index=random.Next(0,Config.productIndex-1);//we first randomize a product.
            _orderItemsArray[Config.orderItemindex] = new DalOrderItem(_productArray[Product_Index].product.ID,
                i,
                random.Next(1,5)
                );
            Config.orderItemindex++;
        }
        for (int i = 0; i < random.Next(10,20); i++)//we randomly add items to exisiting orders.
        {
            int Product_Index = random.Next(0, Config.productIndex-1);
            int Order_Id = random.Next(0, Config.orderindex-1);
            _orderItemsArray[Config.orderItemindex] = new DalOrderItem(_productArray[Product_Index].product.ID,
                Order_Id,
                random.Next(1, 5)
                );
            Config.orderItemindex++;
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
