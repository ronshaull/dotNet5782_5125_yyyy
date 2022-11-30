using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class ProductForList
{
    public int ID { get; set; }
    public string ProductName { get; set; }
    public double Price { get; set; }
    public Enums.Category Category { get; set; }

    public override string? ToString()
    {
        string pfl = "";
        pfl += "ID: " + String.Format("{0:000000}", ID) + "\n";
        pfl +="Name: "+ ProductName + "\n";
        pfl += "Price: " + String.Format("{0:00.00}", Price) + "\n";
        pfl +="Category"+Category.ToString() + "\n";    
        return pfl;
    }
}
