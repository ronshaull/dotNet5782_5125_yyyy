using DO;
using DalApi;
namespace Dal;

internal class DalOrderItem : IOrderItem
{
    //fields.
    public OrderItem Item;
    #region ctors
    /// <summary>
    /// deafault ctor to craete access to data source object.
    /// </summary>
    public DalOrderItem()
    {
        Item = new OrderItem();
    }

    /// <summary>
    /// ctor using explicit fields to create a new Dal layer order item object.
    /// </summary>
    /// <param name="_ProductId"></param>
    /// <param name="_OrderId"></param>
    /// <param name="_Price"></param>
    /// <param name="_Amount"></param>
    public DalOrderItem(int _ProductId, int _OrderId,int _Amount)
    {
        Item.ID = DataSource.Config.OrderItem_ID;
        Item = new OrderItem();
        Item.ProductId = _ProductId;
        Item.OrderId = _OrderId;
        for (int i = 0; i < DataSource._productlist.Count; i++)
        {
            if (DataSource._productlist[i].product.ID==_ProductId)
            {
                Item.Price = DataSource._productlist[i].product.Price;
            }
        }
        Item.Amount = _Amount;
    }
    #endregion
    #region Override Functions
    public override string? ToString()
    {
        return Item.ToString(); 
    }
    #endregion
    #region CRUD functions
    public int Add(OrderItem orderItem)
    {
        try
        {
            if (DataSource._orderlist.Count == 0)
                throw new DalApi.EmptyListEx();
            bool flag = false;
            //we first search if there is an order with the same ID.
            for (int i = 0; i < DataSource._orderlist.Count; i++)
            {
                if (DataSource._orderlist[i].order.ID == orderItem.OrderId)
                {
                    flag = true;
                    break;
                }
            }
            if (!flag)
            {   //now order matches this id.
                throw new DalApi.ObjectNotFoundEx();
            }
            DataSource._orderItemslist.Add(new DalOrderItem(orderItem.ProductId,
                orderItem.OrderId,
                orderItem.Amount));
            return 1;
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
    public void Update(OrderItem orderItem)
    {
        try
        {
            for (int i = 0; i < DataSource._orderItemslist.Count; i++)
            {
                if (DataSource._orderItemslist[i].Item.OrderId == orderItem.OrderId &&
                    DataSource._orderItemslist[i].Item.ProductId == orderItem.ProductId)
                {
                    DataSource._orderItemslist[i].Item.Amount = orderItem.Amount; //we can only change the amount in order.
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
                if (DataSource._orderItemslist[i].Item.ID==ID)
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
    public IEnumerable<OrderItem> GetAll() //could add order id
    {

        List<OrderItem> orderItems=new List<OrderItem>();
        foreach (DalOrderItem orderItem in DataSource._orderItemslist)
        {
            OrderItem curr = new OrderItem();
            curr.OrderId=orderItem.Item.OrderId;
            curr.ProductId=orderItem.Item.ProductId;
            curr.Amount=orderItem.Item.Amount;
            curr.Price=orderItem.Item.Price;
            orderItems.Add(curr);
        }
        return orderItems;  
    }
    public OrderItem Get(int orderID)
    {
        try
        {
            for (int i = 0; i < DataSource._orderItemslist.Count; i++)
            {
                if (DataSource._orderItemslist[i].Item.ID==orderID)
                {
                    return DataSource._orderItemslist[i].Item;
                }
            }
            throw new DalApi.ObjectNotFoundEx();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return new OrderItem();
        }
    }
    #endregion
}
