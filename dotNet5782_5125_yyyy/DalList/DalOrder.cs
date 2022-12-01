using DO;
using DalApi;
namespace Dal;

internal class DalOrder : IOrder
{
    #region CRUD functions
    /// <summary>
    /// adding new order to data layer
    /// </summary>
    /// <param name="order">new order to add</param>
    /// <returns></returns>
    public int Add(Order order)
    {
        try
        {
            if (DataSource._orderlist.Count==DataSource.NUMBER_OF_ORDERS)
            {
                throw new DalApi.OutOfRangeEx();
            }
            order.ID = DataSource.Config.order_Id;
            DataSource._orderlist.Add(order);
            return order.ID;
        }
        catch (DalApi.OutOfRangeEx e)
        {
            throw;
        }
    }
    /// <summary>
    /// to retrive a certin order details from data layer.
    /// </summary>
    /// <param name="ID">of wanted order.</param>
    /// <returns></returns>
    /// <exception cref="DalApi.EmptyListEx"></exception>
    public Order Get(int ID)
    {
        try
        {
            if (DataSource._orderlist.Count==0)
            {
                throw new DalApi.EmptyListEx();
            }
            Order order = DataSource._orderlist.FirstOrDefault(p => p?.ID == ID) ?? throw new DalApi.ObjectNotFoundEx();
            return order;
        }
        catch (DalApi.ObjectNotFoundEx e)
        {
            throw e;
        }
       
    }
    /// <summary>
    /// to update certin order in data layer.
    /// </summary>
    /// <param name="order">contains all new parameters</param>
    /// <exception cref="EmptyListEx"></exception>
    public void Update(Order order)
    {
        try
        {
            if (DataSource._productlist.Count == 0)
            {
                throw new EmptyListEx();
            }
            for (int i = 0; i < DataSource._orderlist.Count; i++)//first find product.
            {
                if (DataSource._orderlist[i]?.ID == order.ID)//product was found
                {
                    //start updateting product.
                    DataSource._orderlist[i] = order;
                    Console.WriteLine("product was updated."); //letting user know update was successful.
                    return;
                }
            }
            throw new DalApi.ObjectNotFoundEx();
        }
        catch (DalApi.EmptyListEx e)
        {
            throw e;
        }
        catch (DalApi.ObjectNotFoundEx e)
        {
            throw e;
        }
    }
    /// <summary>
    /// to dlete a certin order from data layer.
    /// </summary>
    /// <param name="ID">of order we wish to delete.</param>
    public void Delete(int ID)
    {
        try
        {
            if (DataSource._orderlist.Count==0)
            {
                throw new DalApi.EmptyListEx();
            }
            bool flag = true;// we use flag to avoid index violation.
            for (int i = 0; i < DataSource._orderlist.Count; i++)
            {
                if (!flag)
                {
                    break;
                }
                if (DataSource._orderlist[i]?.ID == ID)//we found the order to delete.
                {
                    DataSource._orderlist.RemoveAt(i);
                    flag = false;
                    Console.WriteLine("order was deleted.");
                    return;
                }
            }
            throw new DalApi.ObjectNotFoundEx();
        }
        catch (DalApi.EmptyListEx e)
        {
            throw;
        }
        catch(DalApi.ObjectNotFoundEx e)
        {
            throw;
        }
    }
    /// <summary>
    /// to retrive all orders from data layer, they can be filterd.
    /// </summary>
    /// <param name="Select">a delegate that either hold filter function (bool) or null when we wish to see all orders.</param>
    /// <returns></returns>
    public IEnumerable<Order?> GetAll(Func<Order?, bool>? Select = null)
    {
        if (Select==null)
        {
            List<Order?> orders = new List<Order?>();
            foreach (Order order in DataSource._orderlist)
            {
                orders.Add(order);
            }
            return orders;
        }
        else
        {
            List<Order?> orders = new List<Order?>();
            foreach (Order order in DataSource._orderlist)
            {
                if (Select(order))
                {
                    orders.Add(order);
                }
            }
            return orders;
        }
    }
    #endregion
}
