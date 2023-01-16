using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml.Linq;
using DO;

namespace Dal;

internal class Tools
{
    public static void SaveListToXMLSerializer(List<DO.Product?> list, string entity) 
    {
        string s_dir = @"..\xml\";
        string filePath = $"{s_dir + entity}.xml";
        try
        {
            XElement Root = new XElement(entity);
            foreach (Product item in list)
            {
                XElement ID = new XElement("ID", item.ID);
                XElement Name = new XElement("Name", item.Name);
                XElement Price = new XElement("Price", item.Price);
                XElement InStock=new XElement("InStock",item.InStock);
                XElement Category=new XElement("Category",(int)item.Category);
                XElement Product=new XElement("Product",ID,Name, Price, InStock, Category);
                Root.Add(Product);
            }
            Root.Save(filePath);
        }
        catch (Exception ex)
        {
            // DO.XMLFileLoadCreateException(filePath, $"fail to create xml file: {filePath}", ex); 
            throw new Exception($"fail to create xml file: {filePath}", ex);
        }
    }


    public static void SaveListToXMLSerializer1(List<DO.Order?> list, string entity)
    {
        string s_dir = @"..\xml\";
        string filePath = $"{s_dir + entity}.xml";
        try
        {
            XElement Root = new XElement(entity);
            foreach (Order item in list)
            {
                XElement ID = new XElement("ID", item.ID);
                XElement CustomerName = new XElement("CustomerName", item.CustomerName);
                XElement CustomerAdress = new XElement("CustomerAdress", item.CustomerAdress);
                XElement CustomerEmail = new XElement("CustomerEmail", item.CustomerEmail);
                XElement OrderDate = new XElement("OrderDate",item.OrderDate.ToString());
                XElement ShipDate = new XElement("ShipDate", item.ShipDate.ToString());
                XElement DeliveryDate = new XElement("DeliveryDate", item.DeliveryDate.ToString());
                XElement Order = new XElement("Order", ID, CustomerName, CustomerAdress, CustomerEmail, OrderDate, ShipDate, DeliveryDate);
                Root.Add(Order);
            }
            Root.Save(filePath);
        }
        catch (Exception ex)
        {
            // DO.XMLFileLoadCreateException(filePath, $"fail to create xml file: {filePath}", ex); 
            throw new Exception($"fail to create xml file: {filePath}", ex);
        }
    }
    public static void SaveListToXMLSerializer2(List<DO.OrderItem?> list, string entity)
    {
        string s_dir = @"..\xml\";
        string filePath = $"{s_dir + entity}.xml";
        try
        {
            XElement Root = new XElement(entity);
            foreach (OrderItem item in list)
            {
                XElement ID = new XElement("ID", item.ID);
                XElement ProductId = new XElement("ProductId", item.ProductId);
                XElement OrderId = new XElement("OrderId", item.OrderId);
                XElement Price = new XElement("Price", item.Price);
                XElement Amount = new XElement("Amount", item.Amount);
                XElement OrderItem = new XElement("OrderItem", ID, ProductId, OrderId, Price, Amount);
                Root.Add(OrderItem);
            }
            Root.Save(filePath);
        }
        catch (Exception ex)
        {
            // DO.XMLFileLoadCreateException(filePath, $"fail to create xml file: {filePath}", ex); 
            throw new Exception($"fail to create xml file: {filePath}", ex);
        }
    }

}

