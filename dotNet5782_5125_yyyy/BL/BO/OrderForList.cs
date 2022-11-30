using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class OrderForList
{
    public int ID { get; set; }
    public string CustomerName { get; set; }
    public Enums.Status Status { get; set; }
    public int ItemAmount { get; set; }
    public double Total { get; set; }

    public override string? ToString()
    {
        string ofl = "";
        ofl+= "ID: " + String.Format("{0:000000}",ID)+"\n";
        ofl +="Customer Name: "+CustomerName+"\n";
        ofl +="Status: "+Status.ToString()+"\n";
        ofl +="Items Amount: "+ItemAmount+"\n";
        ofl += "Total Price: " + String.Format("{0:00.00}", Total) + "$\n";
        return ofl;
    }
}
