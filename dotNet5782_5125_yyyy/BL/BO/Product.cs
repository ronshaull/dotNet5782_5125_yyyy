using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class Product
{
    //fields
    public int ID { get; set; } //product serial number.
    public string Name { get; set; } //product name.
    public double Price { get; set; } // product price in us dollars.
    public BO.Enums.Category Category { get; set; } // product section.
    public int InStock { get; set; } // is it in stock for sale?
    /// <summary>
    /// An override for to string function to display product info
    /// </summary>
    /// <returns>Formated string of product info.</returns>
    public override string? ToString()
    {
        string Prudct_discription = "";
        Prudct_discription += "Name:" + Name + "\n";
        Prudct_discription += "ID:" + String.Format("{0:000000}", ID) + "\n";
        Prudct_discription += "Price:" + String.Format("{0:0.00}", Price) + "$" + "\n";
        Prudct_discription += "Category:" + Category.ToString() + "\n";
        return Prudct_discription;

    }
}
