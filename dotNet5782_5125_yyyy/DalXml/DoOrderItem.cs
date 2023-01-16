using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DalApi;
using DO;
namespace Dal
{
    internal class DoOrderItem : IOrderItem
    {
        #region xml properties
        XElement OrderItemRoot;
        XElement ConfigRoot;
        string OrderItemPath = @"..\xml\OrderItems.xml";
        public DoOrderItem()
        {
            if (!File.Exists(OrderItemPath))
                CreateFiles();
            else
                LoadData();
        }
        private void CreateFiles()
        {
            OrderItemRoot = new XElement("OrderItems");
            OrderItemRoot.Save(OrderItemPath);
            ConfigRoot = XElement.Load(@"C:\Users\user\source\repos\ronshaull\dotNet5782_5125_yyyy\dotNet5782_5125_yyyy\config.xml");
        }
        private void LoadData()
        {
            try
            {
                OrderItemRoot = XElement.Load(OrderItemPath);
                ConfigRoot = XElement.Load(@"C:\Users\user\source\repos\ronshaull\dotNet5782_5125_yyyy\dotNet5782_5125_yyyy\config.xml");
            }
            catch
            {
                Console.WriteLine("File upload problem");
            }
        }
        #endregion
        public int Add(OrderItem orderitem)
        {
            LoadData();
            int id = 0;
            XElement OrderItemID = ConfigRoot.Element("OrderItemID");
            int.TryParse(ConfigRoot.Element("OrderItemID").Value, out id);
            id++;
            OrderItemID.Value = id.ToString();
            ConfigRoot.Save(@"C:\Users\user\source\repos\ronshaull\dotNet5782_5125_yyyy\dotNet5782_5125_yyyy\config.xml");
            XElement ID = new XElement("ID", id);
            XElement ProductId = new XElement("ProductId", orderitem.ProductId);
            XElement OrderId = new XElement("OrderId", orderitem.OrderId);
            XElement Price = new XElement("Price", orderitem.Price);
            XElement Amount = new XElement("Amount", orderitem.Amount);
            OrderItemRoot.Add(new XElement("orderitem",ID,ProductId,OrderId, Price,Amount));
            OrderItemRoot.Save(OrderItemPath);
            return 0;
        }

        public void Delete(int ID)
        {
            LoadData();
            XElement OrderItemElement;
            try
            {
                OrderItemElement = (from p in OrderItemRoot.Elements()
                                  where Convert.ToInt32(p.Element("ID").Value) == ID
                                  select p).FirstOrDefault();
                if (OrderItemElement == null)
                {
                    throw new ObjectNotFoundEx();
                }
                OrderItemElement.Remove();
                OrderItemRoot.Save(OrderItemPath);
            }
            catch (DalApi.ObjectNotFoundEx e)
            {
                throw e;
            }
        }

        public OrderItem Get(int id)
        {
            LoadData();
            DO.OrderItem? orderitem;
            try
            {
                orderitem = (from p in OrderItemRoot.Elements()
                           where Convert.ToInt32(p.Element("ID").Value) == id
                           select new DO.OrderItem()
                           {
                               ID = Convert.ToInt32(p.Element("ID").Value),
                               ProductId=Convert.ToInt32(p.Element("ProductId").Value),
                               OrderId= Convert.ToInt32(p.Element("OrderId").Value),
                               Amount= Convert.ToInt32(p.Element("Amount").Value),
                               Price= Convert.ToDouble(p.Element("Price").Value),

                           }).FirstOrDefault();
                if (orderitem == null)
                {
                    throw new ObjectNotFoundEx();
                }
                return (OrderItem)orderitem;
            }
            catch (ObjectNotFoundEx e)
            {

                throw e;
            }
        }

        public OrderItem Get(int ID, Func<OrderItem?, bool>? Select)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrderItem?> GetAll(Func<OrderItem?, bool>? Select = null)
        {
            try
            {
                LoadData();
                if (!OrderItemRoot.HasElements)
                {
                    throw new DalApi.EmptyListEx();
                }
                if (Select == null)
                {
                    List<OrderItem> orderitems = (from p in OrderItemRoot.Elements()
                                              select new OrderItem()
                                              {
                                                  ID = Convert.ToInt32(p.Element("ID").Value),
                                                  ProductId = Convert.ToInt32(p.Element("ProductId").Value),
                                                  OrderId = Convert.ToInt32(p.Element("OrderId").Value),
                                                  Amount = Convert.ToInt32(p.Element("Amount").Value),
                                                  Price = Convert.ToDouble(p.Element("Price").Value),
                                              }).ToList();
                    return orderitems.ConvertAll<OrderItem?>(i => i);
                }
                else
                {
                    List<OrderItem> orderitems = (from p in OrderItemRoot.Elements()
                                              select new OrderItem()
                                              {
                                                  ID = Convert.ToInt32(p.Element("ID").Value),
                                                  ProductId = Convert.ToInt32(p.Element("ProductId").Value),
                                                  OrderId = Convert.ToInt32(p.Element("OrderId").Value),
                                                  Amount = Convert.ToInt32(p.Element("Amount").Value),
                                                  Price = Convert.ToDouble(p.Element("Price").Value),
                                              }).ToList();
                    return orderitems.ConvertAll<OrderItem?>(i => i).Where(Select);

                }
            }
            catch (DalApi.EmptyListEx e)
            {
                throw e;
            }
        }

        public void Update(OrderItem orderitem)
        {
            try
            {
                XElement OrderItemElement = (from p in OrderItemRoot.Elements()
                                           where Convert.ToInt32(p.Element("ID").Value) == orderitem.ID
                                           select p).FirstOrDefault();
                if (OrderItemElement == null)
                    throw new ObjectNotFoundEx();
                OrderItemElement.Element("ProductId").Value = orderitem.ProductId.ToString();
                OrderItemElement.Element("Price").Value = orderitem.Price.ToString();
                OrderItemElement.Element("OrderId").Value = orderitem.OrderId.ToString();
                OrderItemElement.Element("Amount").Value = orderitem.Amount.ToString();
                OrderItemRoot.Save(OrderItemPath);
            }
            catch (ObjectNotFoundEx e)
            {
                throw e;
            }
        }
    }
}
