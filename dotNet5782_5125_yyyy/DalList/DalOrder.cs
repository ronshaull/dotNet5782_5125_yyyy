using DO;
using DalApi;
namespace Dal;

internal class DalOrder : IOrder
{
    public Order order;

    #region ctors
    public DalOrder()
    {
        this.order = new Order();
    }
    public DalOrder(int _ID, string _CustomerName, string _CustomerEmail, string _CustomerAdress)
    {
        order = new Order();
        order.ID = _ID;
        order.CustomerName = _CustomerName;
        order.CustomerEmail = _CustomerEmail;
        order.CustomerAdress = _CustomerAdress;
        order.OrderDate = DateTime.Now.AddDays(new Random().Next(-4,0));
        if (new Random().Next(0,10000)<8000)//80% of getting a shipment value.
        {
            order.ShipDate = order.OrderDate.AddDays(new Random().Next(2, 5));
            order.DeliveryDate = DateTime.MinValue;// as above.
            return;
        }
        //if it yet to be shippted.
        order.ShipDate =DateTime.MinValue;// for now we use min value.
        order.DeliveryDate = DateTime.MinValue;// as above.
    }
    /// <summary>
    /// this ctor is used to initielize an update object, so we can update customers' info,
    /// and report delivery arrival.
    /// </summary>
    /// <param name="_ID"></param>
    /// <param name="_CustomerName"></param>
    /// <param name="_CustomerEmail"></param>
    /// <param name="_CustomerAdress"></param>
    /// <param name="_DeliveryDate"></param>
    public DalOrder(int _ID, string _CustomerName, string _CustomerEmail, string _CustomerAdress,DateTime _OrderDate)
    {
        order = new Order();
        order.ID = _ID;
        order.CustomerName = _CustomerName;
        order.CustomerEmail = _CustomerEmail;
        order.CustomerAdress = _CustomerAdress;
        order.OrderDate = _OrderDate;
        order.ShipDate = DateTime.MinValue;
        order.DeliveryDate = DateTime.MinValue;
    }
    #endregion
    #region Override Functions
    public override string? ToString()
    {
        return order.ToString();
    }
    #endregion
    #region CRUD functions
    public int Add(Order order)
    {
        try
        {
            if (DataSource._orderlist.Count==DataSource.NUMBER_OF_ORDERS)
            {
                throw new DalApi.OutOfRangeEx();
            }
            order.ID = DataSource.Config.order_Id;
            DataSource._orderlist.Add( new DalOrder(order.ID,
                order.CustomerName,
                order.CustomerEmail,
                order.CustomerAdress,
                DateTime.Now));
            return order.ID;
        }
        catch (DalApi.OutOfRangeEx e)
        {
            throw;
        }
    }
    public Order Get(int ID)
    {
        try
        {
            for (int i = 0; i < DataSource._orderlist.Count; i++)
            {
                if (DataSource._orderlist[i].order.ID == ID)
                {
                    Order get_order = new Order();
                    get_order.ID = DataSource._orderlist[i].order.ID;
                    get_order.CustomerName = DataSource._orderlist[i].order.CustomerName;
                    get_order.CustomerEmail = DataSource._orderlist[i].order.CustomerEmail;
                    get_order.CustomerAdress = DataSource._orderlist[i].order.CustomerAdress;
                    get_order.OrderDate = DataSource._orderlist[i].order.OrderDate;
                    get_order.ShipDate = DataSource._orderlist[i].order.ShipDate;
                    get_order.DeliveryDate = DataSource._orderlist[i].order.DeliveryDate;
                    return get_order;
                }
            }
            throw new DalApi.ObjectNotFoundEx();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return new Order();
        }
       
    }
    public void Update(Order order)
    {
        try
        {
            for (int i = 0; i < DataSource._orderlist.Count; i++)
            {
                if (DataSource._orderlist[i].order.ID == order.ID)
                {
                    DataSource._orderlist[i].order.CustomerName = order.CustomerName;
                    DataSource._orderlist[i].order.CustomerEmail = order.CustomerEmail;
                    DataSource._orderlist[i].order.CustomerAdress = order.CustomerAdress;
                    DataSource._orderlist[i].order.OrderDate = order.OrderDate;
                    DataSource._orderlist[i].order.ShipDate = order.ShipDate;
                    DataSource._orderlist[i].order.DeliveryDate = order.DeliveryDate;
                    Console.WriteLine("order was updated!");
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
                if (DataSource._orderlist[i].order.ID == ID)//we found the order to delete.
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
    public IEnumerable<Order> GetAll()
    {
        List<Order> orders = new List<Order>();
        foreach (DalOrder order in DataSource._orderlist)
        {
            Order curr = new Order();
            curr.ID = order.order.ID;
            curr.OrderDate = order.order.OrderDate;
            curr.CustomerAdress = order.order.CustomerAdress;
            curr.CustomerEmail = order.order.CustomerEmail;
            curr.ShipDate = order.order.ShipDate;
            curr.CustomerName = order.order.CustomerName;
            curr.DeliveryDate= order.order.DeliveryDate;
            orders.Add(curr);
        }
        return orders;
    }
    #endregion
    
}
