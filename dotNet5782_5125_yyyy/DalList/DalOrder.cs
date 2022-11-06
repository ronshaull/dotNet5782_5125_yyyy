
using DO;

namespace Dal;

public class DalOrder
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
    public DalOrder(int _ID, string _CustomerName, string _CustomerEmail, string _CustomerAdress,DateTime _DeliveryDate)
    {
        order = new Order();
        order.ID = _ID;
        order.CustomerName = _CustomerName;
        order.CustomerEmail = _CustomerEmail;
        order.CustomerAdress = _CustomerAdress;
        order.DeliveryDate = _DeliveryDate;
    }
    #endregion
    #region Override Functions
    public override string? ToString()
    {
        return order.ToString();
    }
    #endregion
    #region CRUD functions
    public void AddOrder()
    {
        try
        {
            if (DataSource.Config.orderindex==DataSource.NUMBER_OF_ORDERS-1)
            {
                throw new Exception("insufficient space,order array is full");
            }
            this.order.ID = DataSource.Config.order_Id;
            DataSource._orderArray[DataSource.Config.orderindex] = new DalOrder(this.order.ID,
                this.order.CustomerName,
                this.order.CustomerEmail,
                this.order.CustomerAdress);
            DataSource.Config.orderindex++;
            Console.WriteLine("order was added to order list.");
            Console.WriteLine("your order ID is:"+this.order.ID);
            return;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return;
        }
    }
    public string DisplayOrder(int ID)
    {
        for (int i = 0; i < DataSource.Config.orderindex; i++)
        {
            if (DataSource._orderArray[i].order.ID==ID)
            {
                return DataSource._orderArray[i].ToString();
            }
        }
        return "order was not found on list!";
    }
    public void UpdateOrder()
    {
        for (int i = 0; i < DataSource.Config.orderindex; i++)
        {
            if (DataSource._orderArray[i].order.ID==this.order.ID)
            {
                DataSource._orderArray[i].order.CustomerName = this.order.CustomerName;
                DataSource._orderArray[i].order.CustomerEmail = this.order.CustomerEmail;
                DataSource._orderArray[i].order.CustomerAdress = this.order.CustomerAdress;
                DataSource._orderArray[i].order.OrderDate = this.order.OrderDate;
                Console.WriteLine("order was updated!");
                return;
            }
        }
        Console.WriteLine("order was'nt found, check order ID!");
    }
    public void DeleteOrder(int ID)
    {
        bool flag=true;// we use flag to avoid index violation.
        for (int i = 0; i < DataSource.Config.orderItemindex; i++)
        {
            if (!flag)
            {
                break;
            }
            if (DataSource._orderArray[i].order.ID==ID)//we found the order to delete.
            {
                for (int j = i; j < DataSource.Config.orderindex; j++)
                {
                    if (j<DataSource.Config.orderindex)
                    {
                        DataSource._orderArray[j] = DataSource._orderArray[j + 1];
                    }
                }
                flag=false;
                DataSource.Config.orderindex--;
                Console.WriteLine("order was deleted.");
                return;
            }
        }
    }
    public DalOrder[] PrintOrders()
    {
        return DataSource._orderArray;
    }
    #endregion
    
}
