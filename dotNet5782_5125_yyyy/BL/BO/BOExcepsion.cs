using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;
//TODO: there is a lot of types of exceptions in the project discription.
//add them too!

/// <summary>
/// in case we try to either add item when there is no space for it.
/// as was defiend in project description.
/// </summary>
public class OutOfRangeEx : Exception
{
    public string message = "Exception thrown: insufficient space in that list.";
}
/// <summary>
/// in case we ask an object (to display/change etc) and it was not found in list.
/// </summary>
public class ObjectNotFoundEx : Exception
{
    public string message = "Exception thrown: Object was not found in list.";
}
/// <summary>
/// in case we want to create a new object with an excisting id.
/// </summary>
public class IdExistEx : Exception
{
    public string message = "Exception thrown: ID already excisit.";
}   

public class InvalidParamsEx : Exception
{
    public string message = "Object parameters were invalid";
}
/// <summary>
/// in case list was empty. (we are try to delete or get from an empty list)
/// </summary>
public class EmptyListEx : Exception
{
    public string message = "list was empty!.";
}
/// <summary>
/// in case we want to delete a product that is in an exicting order.
/// </summary>
public class ProductInOrderEx : Exception
{
    public string message = "Product is in an order therefor cannot be deleted.";
}
/// <summary>
/// product is out of stock exception
/// </summary>
public class OutOfStockEx : Exception
{
    public string message = "this product is out of stock, you cannot order it.";
}
public class InsufficientStockEx : Exception
{
    public string message = "Not inough item in stock";
}
public class CustomerDetailsEx : Exception
{
    public string message = "customer details (name,email,adress) were invalid.";
}
public class GeneralEx : Exception
{
    public GeneralEx(string? message) : base(message)
    {
    }
}