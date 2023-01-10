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
            int exp = DataSource._orderlist.RemoveAll(or => order.ID == or?.ID);
            if (exp == 0)
                throw new DalApi.ObjectNotFoundEx();
            DataSource._orderlist.Add(order);
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
            int exp = DataSource._orderlist.RemoveAll(p => p?.ID == ID);
            if (exp == 0)
                throw new DalApi.ObjectNotFoundEx();
            return;
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
            return DataSource._orderlist.Where(or=> or!= null).ToList();
        }
        else
        {
            return DataSource._orderlist.Where(Select).ToList();
        }
    }
    /// <summary>
    /// special get function to retrive a certin order, that satisfy a condition.
    /// </summary>
    /// <param name="ID">of the order</param>
    /// <param name="Select">the condition implemented by a boolean function.</param>
    /// <returns></returns>
    public Order Get(int ID, Func<Order?, bool>? Select)
    {
        try
        {
            Order order = DataSource._orderlist.FirstOrDefault(p => p?.ID == ID) ?? throw new ObjectNotFoundEx();
            if (Select(order))
            {
                return order;
            }
            throw new ObjectNotFoundEx();
        }
        catch (ObjectNotFoundEx e)
        {
            throw e;
        }
    }
    #endregion
}
