using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class ProductItem
{
    public int ID { get; set; }
    public string? Name { get; set; }
    public double Price { get; set; }
    public Enums.Category? Category { get; set; }
    public bool InStock { get; set; }
    public int Amount { get; set; } //in the cart.

    public override string? ToString()
    {
        string pi = "";
        pi += "ID: " + String.Format("{0:000000}", ID) + "\n";
        pi += "p.Name: " + Name + "\n";
        pi += "Price: " + String.Format("{0:00.00}", Price) + "\n";
        pi += "Category: " + Category.ToString() + "\n";
        pi += "In stock: " + new string(InStock == true ? "yes" : "no");
        pi += "Amount in cart: "+Amount + "\n";
        return pi;
    }
}

