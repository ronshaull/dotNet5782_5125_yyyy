using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class Cart
{
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public string CustomerAdress { get; set; }
    public List<OrderItem> Items { get; set; } = new List<OrderItem>();//items in this cart. if order is placed, and this items are in stock it will be in the order.
    public double Total { get; set; } //total price of the whole cart.

    public override string? ToString()
    {
        string cart_s = "";
        cart_s += "Customer name: " + CustomerName + "\n";
        cart_s += "Email: " + CustomerEmail + "\n";
        cart_s += "Adress: "+CustomerAdress + "\n";
        cart_s += "Total price:" + String.Format("{0:00.00}", Total) + "$\n";
        return cart_s;
    }
}
