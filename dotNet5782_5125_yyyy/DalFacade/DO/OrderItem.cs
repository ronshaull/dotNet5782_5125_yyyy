
namespace DO;

public struct OrderItem
{
    //fields
    public int ProductId { get; set; }//from product itself.
    public int OrderId { get; set; }//genereted.
    public double Price { get; set; }//Product price when it was purchased.
    public int Amount { get; set; }//amount of item in this order.
    /// <summary>
    /// To string override of order type, to display an item that is in an
    /// order info.
    /// </summary>
    /// <returns>formated string of an item in a order info.</returns>
    public override string? ToString()
    {
        string orderItem_string = "";
        orderItem_string += "Product ID:" + String.Format("{0:000000}",ProductId) + "\n";
        orderItem_string+="Order ID:" + String.Format("{0:000000}", OrderId) + "\n";
        orderItem_string+="Price:" + String.Format("{0:00.00}"+"$", Price) + "\n";
        orderItem_string+="Amount:"+Amount.ToString() + "\n";  
        return orderItem_string;
    }
}
