using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class OrderTracking
{
    public int ID { get; set; }
    public Enums.Status? Status { get; set; }
    public List<(DateTime?, string? status)> Progress { get; set; }=new List<(DateTime?, string)>();

    public override string? ToString()
    {
        string ot = "";
        ot += "ID: " + String.Format("{0:000000}", ID) + "\n";
        ot +="Status: "+Status.ToString()+"\n";
        //TODO: how do we add the list of dates and progress?
        ot += "order was:\n";
        for (int i = 0; i < 3; i++)
        {
            if (this.Progress[i].Item1!=DateTime.MinValue)
            {
                ot += Progress[i].Item2 + "\n";
            }
        }
        return ot;
    }
}
