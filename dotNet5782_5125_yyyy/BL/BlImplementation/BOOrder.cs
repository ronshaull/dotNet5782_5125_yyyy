using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BO;
using DalApi;

namespace BlImplementation;

internal class BOOrder : BlApi.IOrder
{
    //fields
    private IDal? dal = DalApi.Factory.Get();


    #region IOrder implementation
    /// <summary>
    /// function used to claim an order to be deliverd.
    /// </summary>
    /// <param name="ID">that was deliverd.</param>
    /// <returns></returns>
    public BO.Order Deliverd(int ID)
    {
        try
        {
            DO.Order order = dal.Order.Get(ID);
            order.DeliveryDate = DateTime.Now;
            dal.Order.Update(order);
            BO.Order BOOrder = new BO.Order() {ID=order.ID,
            CustomerAdress=order.CustomerAdress,
            CustomerEmail=order.CustomerEmail,
            CustomerName=order.CustomerName,
            OrderDate=order.OrderDate,
            ShipDate=order.ShipDate,
            DeliveryDate=order.DeliveryDate};
            (double, IEnumerable<OrderItem>) tuple = ProductOnOrder(ID);
            BOOrder.Total = tuple.Item1;
            BOOrder.OrderItems =(List<OrderItem>) tuple.Item2;
            return BOOrder; 
        }
        catch (DalApi.ObjectNotFoundEx e)
        { 
            throw new BO.ObjectNotFoundEx();
        }
        catch (DalApi.EmptyListEx)
        {
            throw new BO.EmptyListEx();
        }
    }
    /// <summary>
    /// function used for getting a list of specific orders.
    /// </summary>
    /// <returns></returns>
    public IEnumerable<BO.OrderForList?> GetAll()
    {
        IEnumerable<DO.Order?> orders = dal.Order.GetAll();
        List<BO.OrderForList> BLOrders = new List<BO.OrderForList>();
        foreach (DO.Order order in orders)
        {
            BO.OrderForList tmp = new BO.OrderForList()
            {
                ID = order.ID,
                CustomerName = order.CustomerName,
            };
            tmp.Status =( order.ShipDate != null )? (BO.Enums.Status)1 : (BO.Enums.Status)0;
            tmp.Status = (order.DeliveryDate != null) ? (BO.Enums.Status)2 :tmp.Status;
            IEnumerable<DO.OrderItem?> items = dal.OrderItem.GetAll();
            double total = 0;
            int amount = 0;
            foreach (DO.OrderItem item in items) // we calculate by hand the total and amount.
            {
                amount += item.OrderId == order.ID ? item.Amount : 0;
                total += item.OrderId == order.ID ? (double)item.Amount * item.Price : 0;
            }
            tmp.Total = total;
            tmp.ItemAmount = amount;
            BLOrders.Add(tmp);
        }
        return BLOrders;
    }
    /// <summary>
    /// function used to get details of specific order.
    /// </summary>
    /// <param name="ID">that we want to retreive.</param>
    /// <returns></returns>
    public BO.Order Get(int ID)
    {
        try
        {
            if (ID < 0) //invalid ID.
            {
                throw new BO.InvalidParamsEx(); //catch in main.
            }
            //try and get it from data layer.
            DO.Order order = dal.Order.Get(ID);
            //creating BO order to return, and initialize its fields.
            BO.Order BLOrder = new BO.Order()
            {
                ID = order.ID,
                CustomerAdress = order.CustomerAdress,
                CustomerEmail = order.CustomerEmail,
                CustomerName = order.CustomerName,
                DeliveryDate = order.DeliveryDate,
                OrderDate = order.OrderDate,
                ShipDate = order.ShipDate,
            };
            // some fields require another type of data.
            IEnumerable<DO.OrderItem?> items = dal.OrderItem.GetAll();
            double total = 0;
            foreach (DO.OrderItem item in items)
            {
                if (item.OrderId == BLOrder.ID)// this item bellongs in this order.
                {
                    BO.OrderItem tmp = new BO.OrderItem() //create a new order item to add to this order list of items.
                    {
                        ID = item.ID,
                        Amount = item.Amount,
                        OrderId = BLOrder.ID,
                        Price = item.Price,
                        ProductId = item.ProductId,
                        ProductName = dal.Product.Get(item.ProductId).Name
                    };
                    total += item.OrderId == order.ID ? (double)item.Amount * item.Price : 0; //update the total.
                    BLOrder.OrderItems.Add(tmp); //add it to the order instance.
                }
                else
                    continue; //this item does not bellong in this order.
            }
            BLOrder.Total = total; //lastly update the total field.
            return BLOrder; //return the object.
        }
        catch (DalApi.ObjectNotFoundEx e)
        {
            throw new BO.ObjectNotFoundEx();
        }

    }
    /// <summary>
    /// used for manager user, to track an order.
    /// </summary>
    /// <param name="ID">that we want to track.</param>
    /// <returns></returns>
    public BO.OrderTracking OrderTracking(int ID)
    {
        try
        {
            DO.Order order = dal.Order.Get(ID);//also check does order excist.
            BO.OrderTracking orderTracking = new OrderTracking() { ID=order.ID,
            };
            orderTracking.Status = (order.ShipDate != DateTime.MinValue) ? (BO.Enums.Status)1: (BO.Enums.Status)0;
            orderTracking.Status = (order.DeliveryDate != DateTime.MinValue) ? (BO.Enums.Status)2 : orderTracking.Status;
            orderTracking.Progress.Add(new(order.OrderDate, ((BO.Enums.Status)0).ToString()));
            orderTracking.Progress.Add(new(order.ShipDate, ((BO.Enums.Status)1).ToString()));
            orderTracking.Progress.Add(new(order.DeliveryDate, ((BO.Enums.Status)2).ToString()));
            return orderTracking;
        }
        catch (DalApi.EmptyListEx e)
        {

            throw new BO.EmptyListEx();
        }
        catch (DalApi.ObjectNotFoundEx e)
        {

            throw new BO.ObjectNotFoundEx();
        }
    }
    /// <summary>
    /// for Customer screen, updating the delivery time.
    /// </summary>
    /// <param name="ID">that we want to update.</param>
    /// <returns></returns>
    public BO.Order UpdateDelivery(int ID)
    {
        try
        {
            if (ID < 0)
                throw new BO.InvalidParamsEx();
            DO.Order order = dal.Order.Get(ID); //if not exict an exception will be thrown, so safe to asume that in next line order
            //was found.
            if (order.DeliveryDate != null)
            {
                throw new BO.GeneralEx("this order alreay was deliverd!.");
            }
            order.DeliveryDate = DateTime.Now;
            dal.Order.Update(order);
            BO.Order BOOrder = new Order()
            {
                ID = order.ID,
                CustomerAdress = order.CustomerAdress,
                CustomerEmail = order.CustomerEmail,
                CustomerName = order.CustomerName,
                DeliveryDate = order.DeliveryDate,
                OrderDate = order.OrderDate,
                ShipDate = order.ShipDate,
            };
            (double, IEnumerable<BO.OrderItem>) tuple = ProductOnOrder(ID);
            BOOrder.OrderItems = (List<BO.OrderItem>)tuple.Item2;
            BOOrder.Total = tuple.Item1;
            return BOOrder;
        }
        catch (DalApi.ObjectNotFoundEx e)
        {
            throw new BO.ObjectNotFoundEx();
        }
        catch (DalApi.EmptyListEx e)
        {
            throw new BO.EmptyListEx();
        }
    }
    /// <summary>
    /// for manager screen to update shipment.
    /// </summary>
    /// <param name=""></param>
    /// <returns></returns>
    public BO.Order UpdateShipment(int ID)
    {
        try
        {
            if(ID<0)
                throw new BO.InvalidParamsEx(); 
            DO.Order order = dal.Order.Get(ID); //if not exict an exception will be thrown, so safe to asume that in next line order
            //was found.
            if (order.ShipDate!=null)
            {
                throw new BO.GeneralEx("this order alreay was shipped!.");
            }
            order.ShipDate = DateTime.Now;//update DO object
            dal.Order.Update(order);//ask data layer to update.
            
            //careate a BO Object updated.
            BO.Order BOOrder=new Order() { ID=order.ID,
            CustomerAdress=order.CustomerAdress,
            CustomerEmail=order.CustomerEmail,
            CustomerName=order.CustomerName,
            DeliveryDate=order.DeliveryDate,
            OrderDate=order.OrderDate,
            ShipDate=order.ShipDate,
            };
            (double, IEnumerable<BO.OrderItem>) tuple = ProductOnOrder(ID);
            BOOrder.OrderItems = (List<BO.OrderItem>)tuple.Item2;
            BOOrder.Total=tuple.Item1;
            //return it.
            return BOOrder;
        }
        catch (DalApi.ObjectNotFoundEx e)
        {
            throw new BO.ObjectNotFoundEx();
        }
    }
    #endregion
    #region assistment functions
    /// <summary>
    /// function is used to cllect all order items in a specific order.
    /// plus it already calculate this order total.
    /// </summary>
    /// <param name="ID">of the order that we want to collect its order items.  </param>
    /// <returns></returns>
    private (Double,IEnumerable<BO.OrderItem>) ProductOnOrder(int ID)
    {
        List<DO.OrderItem?> orderItems = (List<DO.OrderItem?>)dal.OrderItem.GetAll();
        List<BO.OrderItem> productItems = new List<BO.OrderItem>();
        double Total = 0;
        foreach (DO.OrderItem item in orderItems)
        {
            if (item.OrderId==ID)
            {
                productItems.Add(new BO.OrderItem() { ID=item.ID,
                Amount=item.Amount,
                OrderId=ID,
                Price=item.Price,
                ProductId=item.ProductId,
                ProductName=dal.Product.Get(item.ProductId).Name});
                Total+=item.Price*item.Amount;
            }
        }
        return (Total,productItems);
    }
    #endregion
}
