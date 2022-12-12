using DO;
using DalApi;
namespace Dal;

internal class DalOrderItem : IOrderItem
{
    #region CRUD functions
    /// <summary>
    /// to add a new order item to data base.
    /// </summary>
    /// <param name="orderItem">that we want to add.</param>
    /// <returns></returns>
    public int Add(OrderItem orderItem)
    {
        try
        {
            if (DataSource._orderItemslist.Count == DataSource.NUMBER_OF_ORDERITEMS)
                throw new DalApi.OutOfRangeEx(); //no more space in list.
            if (DataSource._orderlist.Count == 0)
                throw new DalApi.EmptyListEx();
            //we first search if there is an order with the same ID.
            Order order = DataSource._orderlist.FirstOrDefault(p => p?.ID == orderItem.OrderId) ?? throw new DalApi.ObjectNotFoundEx();
            orderItem.ID = DataSource.Config.OrderItem_ID;
            DataSource._orderItemslist.Add(orderItem);
            return orderItem.ID;
        }
        catch (DalApi.ObjectNotFoundEx e)
        {
            throw;
        }
        catch(DalApi.EmptyListEx e)
        {
            throw;
        }
    }
    /// <summary>
    /// to update a certin order item from data base.
    /// </summary>
    /// <param name="orderItem">holds all new fields.</param>
    /// <exception cref="DalApi.ObjectNotFoundEx"></exception>
    public void Update(OrderItem orderItem)
    {
        try
        {
            if (DataSource._orderItemslist.Count==0)
            {
                throw new DalApi.EmptyListEx();
            }
            for (int i = 0; i < DataSource._orderItemslist.Count; i++)
            {
                if (DataSource._orderItemslist[i]?.ID==orderItem.ID)
                {
                    DataSource._orderItemslist[i] = orderItem;
                    return; 
                }
            }
            throw new DalApi.ObjectNotFoundEx();
        }
        catch (ObjectNotFoundEx e)
        {
            throw e;
        }
    }
    /// <summary>
    /// to delete a certin order item from data base.
    /// </summary>
    /// <param name="ID">of the order item we want to delete.</param>
    /// <exception cref="DalApi.ObjectNotFoundEx"></exception>
    public void Delete(int ID)
    {
        try
        {
            bool flag = true;
            for (int i = 0; i < DataSource._orderItemslist.Count; i++)
            {
                if (!flag)
                {
                    break;
                }
                if (DataSource._orderItemslist[i]?.ID==ID)
                {
                    DataSource._orderItemslist.RemoveAt(i);
                    flag = false;
                    Console.WriteLine("item was deleted from order");
                    return;
                }
            }
            throw new DalApi.ObjectNotFoundEx();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

    }
    /// <summary>
    /// to get all order items from data base.
    /// </summary>
    /// <returns></returns>
    public IEnumerable<OrderItem?> GetAll(Func<OrderItem?, bool>? Select = null)
    {
        if (Select==null) //no filter.
        {
            List<OrderItem?> orderItems = new List<OrderItem?>();
            foreach (OrderItem orderItem in DataSource._orderItemslist)
            {
                orderItems.Add(orderItem);
            }
            return orderItems;
        }
        else
        {
            List<OrderItem?> orderItems = new List<OrderItem?>();
            foreach (OrderItem orderItem in DataSource._orderItemslist)
            {
             
                if (Select(orderItem))
                {
                    orderItems.Add(orderItem);
                }
            }
            return orderItems;
        }
    }
    /// <summary>
    /// to retrive a certin order item from data base
    /// </summary>
    /// <param name="orderID">of the order item we want.</param>
    /// <returns></returns>
    /// <exception cref="DalApi.ObjectNotFoundEx"></exception>
    public OrderItem Get(int ID)
    {
        try
        {
            OrderItem orderItem=DataSource._orderItemslist.FirstOrDefault(p=> p?.ID==ID)?? throw new DalApi.ObjectNotFoundEx();
            return orderItem;
        }
        catch (ObjectNotFoundEx e)
        {
            throw e;
        }
    }
    /// <summary>
    /// a special get function to retrive a certin order item, if it satisfy Select function.
    /// </summary>
    /// <param name="ID">of the object.</param>
    /// <param name="Select">the condition implemented by a boolean function.</param>
    /// <returns></returns>
    /// <exception cref="ObjectNotFoundEx"></exception>
    public OrderItem Get(int ID, Func<OrderItem?, bool>? Select)
    {
        try
        {
            OrderItem orderItem = DataSource._orderItemslist.FirstOrDefault(p => p?.ID == ID) ?? throw new ObjectNotFoundEx();
            if (Select(orderItem))
            {
                return orderItem;
            }
            throw new ObjectNotFoundEx();
        }
        catch (DalApi.ObjectNotFoundEx e)
        {
            throw e;
        }
    }
    #endregion
}
