﻿namespace DO;

public struct Order
{
    //fields
    public int ID { get; set; } //orders' id
    public string? CustomerName{ get; set; } // customer who placed the order/
    public string? CustomerEmail{ get; set; }//customer Email.
    public string? CustomerAdress{ get; set; }//customer adress
    public DateTime? OrderDate{ get; set; }//the time and date the order was placed.
    public DateTime? ShipDate { get; set; }//when the order is going to be shipped.
    public DateTime? DeliveryDate { get; set; }// when it will be delivered.
    #region Override Functions
    /// <summary>
    /// To string override function, to display an order info.
    /// </summary>
    /// <returns>A formated string of order info.</returns>
    public override string? ToString()
    {
        string Order_string = "";
        Order_string += "Order ID:" + String.Format("{0:000000}",ID)+ "\n";
        Order_string += "Customer Name:" + CustomerName + "\n";
        Order_string += "Customer Email:" + CustomerEmail + "\n";
        Order_string+="Customer Adress:"+ CustomerAdress + "\n";
        Order_string+="Date of Order:"+OrderDate.ToString() + "\n";
        if (ShipDate==DateTime.MinValue)
        {
            Order_string += "Shipment Date: yet to be shipped." + "\n";
        }
        else 
            Order_string+="Shipment Date:"+ShipDate.ToString() + "\n";
        if (DeliveryDate==DateTime.MinValue)
        {
            Order_string += "Delivery Date: yet to be deliverd" + "\n";
        }
        else
            Order_string+="Delivery Date:"+DeliveryDate.ToString() + "\n";
        return Order_string;
    }
    #endregion

}
