using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class OrderItem
{
    //fields
    public int ID { get; set; } //order item id
    public int ProductId { get; set; }//from product itself.
    public string ProductName { get; set; }
    public int OrderId { get; set; }//genereted.
    public double Price { get; set; }//Product price when it was purchased.
    public int Amount { get; set; }//amount of item in this order.
    public double Total
    {
        get { return Price*Amount; }
    }

    /// <summary>
    /// To string override of order type, to display an item that is in an
    /// order info.
    /// </summary>
    /// <returns>formated string of an item in a order info.</returns>
    public override string? ToString()
    {
        string orderItem_string = "";
        orderItem_string += "Order Item ID: " + String.Format("{0:000000}", ID) + "\n";
        orderItem_string += "Product ID:" + String.Format("{0:000000}", ProductId) + "\n";
        orderItem_string += "Product name: " + ProductName + "\n";
        orderItem_string += "Order ID:" + String.Format("{0:000000}", OrderId) + "\n";
        orderItem_string += "Price:" + String.Format("{0:00.00}" + "$", Price) + "\n";
        orderItem_string += "Amount:" + Amount.ToString() + "\n";
        orderItem_string += "Total price of product: " + String.Format("{0:00.00}" + "$", Total) + "\n";
        return orderItem_string;
    }
}
