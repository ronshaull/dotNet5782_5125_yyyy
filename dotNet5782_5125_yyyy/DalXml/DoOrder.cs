using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DalApi;
using DO;
namespace Dal
{
    internal class DoOrder : IOrder
    {

        #region xml properties
        XElement OrderRoot;
        XElement ConfigRoot;
        string OrderPath = @"..\xml\Orders.xml";
        public DoOrder()
        {
            if (!File.Exists(OrderPath))
                CreateFiles();
            else
                LoadData();
        }
        private void CreateFiles()
        {
            OrderRoot = new XElement("Orders");
            OrderRoot.Save(OrderPath);
            ConfigRoot = XElement.Load(@"C:\Users\user\source\repos\ronshaull\dotNet5782_5125_yyyy\dotNet5782_5125_yyyy\config.xml");
        }
        private void LoadData()
        {
            try
            {
                OrderRoot = XElement.Load(OrderPath);
                ConfigRoot = XElement.Load(@"C:\Users\user\source\repos\ronshaull\dotNet5782_5125_yyyy\dotNet5782_5125_yyyy\config.xml");
            }
            catch
            {
                Console.WriteLine("File upload problem");
            }
        }
        #endregion
        public int Add(Order order)
        {
            int id=0;
            XElement OrderID =ConfigRoot.Element("OrderID");
            int.TryParse(OrderID.Value, out id);
            id++;
            OrderID.Value = id.ToString();
            ConfigRoot.Save(@"C:\Users\user\source\repos\ronshaull\dotNet5782_5125_yyyy\dotNet5782_5125_yyyy\config.xml");
            XElement ID = new XElement("ID", id);
            XElement CustomerName = new XElement("CustomerName", order.CustomerName);
            XElement CustomerAdress = new XElement("CustomerAdress", order.CustomerAdress);
            XElement CustomerEmail = new XElement("CustomerEmail", order.CustomerEmail);
            XElement ShipDate= new XElement("ShipDate", "");
            XElement OrderDate = new XElement("OrderDate", order.OrderDate.ToString());
            XElement DeliveryDate = new XElement("DeliveryDate", "");
            OrderRoot.Add(new XElement("Order", ID, CustomerName, CustomerAdress, CustomerEmail
                , OrderDate,ShipDate,DeliveryDate));
            OrderRoot.Save(OrderPath);
            return id;
        }

        public void Delete(int ID)
        {
            LoadData();
            XElement OrderElement;
            try
            {
                OrderElement = (from p in OrderRoot.Elements()
                                  where Convert.ToInt32(p.Element("ID").Value) == ID
                                  select p).FirstOrDefault();
                if (OrderElement == null)
                {
                    throw new ObjectNotFoundEx();
                }
                OrderElement.Remove();
                OrderRoot.Save(OrderPath);
            }
            catch (DalApi.ObjectNotFoundEx e)
            {
                throw e;
            }
        }

        public Order Get(int id)
        {
            LoadData();
            DO.Order? order;
            try
            {
                order = (from p in OrderRoot.Elements()
                           where Convert.ToInt32(p.Element("ID").Value) == id
                           select new DO.Order()
                           {
                               ID = Convert.ToInt32(p.Element("ID").Value),
                               CustomerName=p.Element("CustomerName").Value,
                               CustomerAdress=p.Element("CustomerAdress").Value,
                               CustomerEmail=p.Element("CustomerEmail").Value,
                               OrderDate = (p.Element("OrderDate").Value.ToString() == "") ? DateTime.MinValue :
                                            DateTime.Parse(p.Element("OrderDate").Value.ToString()),
                               ShipDate = (p.Element("ShipDate").Value.ToString() == "") ? DateTime.MinValue :
                                            DateTime.Parse(p.Element("ShipDate").Value.ToString()),
                               DeliveryDate = (p.Element("DeliveryDate").Value.ToString() == "") ? DateTime.MinValue :
                                            DateTime.Parse(p.Element("DeliveryDate").Value.ToString()),
                           }).FirstOrDefault();
                if (order == null)
                {
                    throw new ObjectNotFoundEx();
                }
                return (Order)order;
            }
            catch (ObjectNotFoundEx e)
            {

                throw e;
            }
        }

        public Order Get(int ID, Func<Order?, bool>? Select)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order?> GetAll(Func<Order?, bool>? Select = null)
        {
            try
            {
                LoadData();
                if (!OrderRoot.HasElements)
                {
                    throw new DalApi.EmptyListEx();
                }
                if (Select==null)
                {
                    List<Order> orders=(from p in OrderRoot.Elements()
                                        select  new Order()
                                        {
                                            ID = Convert.ToInt32(p.Element("ID").Value),
                                            CustomerName = p.Element("CustomerName").Value,
                                            CustomerAdress = p.Element("CustomerAdress").Value,
                                            CustomerEmail = p.Element("CustomerEmail").Value,
                                            OrderDate = (p.Element("OrderDate").Value=="")?DateTime.MinValue:
                                            DateTime.Parse(p.Element("OrderDate").Value.ToString()),
                                            ShipDate = (p.Element("ShipDate").Value=="")?DateTime.MinValue:
                                            DateTime.Parse(p.Element("ShipDate").Value.ToString()),
                                            DeliveryDate = (p.Element("DeliveryDate").Value=="")?DateTime.MinValue:
                                            DateTime.Parse(p.Element("DeliveryDate").Value.ToString()),
                                        }).ToList();
                    return orders.ConvertAll<Order?>(i => i).Distinct();
                }
                else
                {
                    List<Order> orders = (from p in OrderRoot.Elements()
                                          select new Order()
                                          {
                                              ID = Convert.ToInt32(p.Element("ID").Value),
                                              CustomerName = p.Element("CustomerName").Value,
                                              CustomerAdress = p.Element("CustomerAdress").Value,
                                              CustomerEmail = p.Element("CustomerEmail").Value,
                                              OrderDate = (p.Element("OrderDate").Value.ToString() == "") ? DateTime.MinValue :
                                              DateTime.Parse(p.Element("OrderDate").Value.ToString()),
                                              ShipDate = (p.Element("ShipDate").Value.ToString() == "") ? DateTime.MinValue :
                                              DateTime.Parse(p.Element("ShipDate").Value.ToString()),
                                              DeliveryDate = (p.Element("DeliveryDate").Value.ToString() == "") ? DateTime.MinValue :
                                              DateTime.Parse(p.Element("DeliveryDate").Value.ToString()),
                                          }).ToList();
                    return orders.ConvertAll<Order?>(i => i).Where(Select);
                }
            }
            catch (DalApi.EmptyListEx e)
            {
                throw;
            }
        }

        public void Update(Order order)
        {
            try
            {
                XElement OrderElement=(from p in OrderRoot.Elements()
                                       where Convert.ToInt32(p.Element("ID").Value) == order.ID
                                       select p).FirstOrDefault();
                if (OrderElement == null)
                    throw new ObjectNotFoundEx();
                OrderElement.Element("CustomerName").Value = order.CustomerName;
                OrderElement.Element("CustomerAdress").Value = order.CustomerAdress;
                OrderElement.Element("CustomerEmail").Value = order.CustomerEmail;
                OrderElement.Element("OrderDate").Value = order.OrderDate.ToString();
                OrderElement.Element("ShipDate").Value = order.ShipDate.ToString();
                OrderElement.Element("DeliveryDate").Value = order.DeliveryDate.ToString();
                OrderRoot.Save(OrderPath);
            }
            catch (ObjectNotFoundEx e)
            {

                throw e;
            }
        }
    }
}
