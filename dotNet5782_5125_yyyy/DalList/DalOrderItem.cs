using DO;

namespace Dal;

public class DalOrderItem
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
        Item = new OrderItem();
        Item.ProductId = _ProductId;
        Item.OrderId = _OrderId;
        for (int i = 0; i < DataSource.Config.productIndex; i++)
        {
            if (DataSource._productArray[i].product.ID==_ProductId)
            {
                Item.Price = DataSource._productArray[i].product.Price;
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
    public void AddOrderItem()
    {
        bool flag = false;
        //we first search if there is an order with the same ID.
        for (int i = 0; i < DataSource.Config.orderindex; i++)
        {
            if (DataSource._orderArray[i].order.ID==this.Item.OrderId)
            {
                flag=true;
                break;
            }
        }
        if (!flag)
        {
            Console.WriteLine("order id dont match any order.");
            return;
        }
        DataSource._orderItemsArray[DataSource.Config.orderItemindex] = new DalOrderItem(this.Item.ProductId,
            this.Item.OrderId,
            this.Item.Amount);
        Console.WriteLine("order item was added to order successfully.");
        return;
    }
    public string DisplayOrderItem(int OrderID,int ProductID)
    {
        for (int i = 0; i < DataSource.Config.orderItemindex; i++)
        {
            if (DataSource._orderItemsArray[i].Item.OrderId==OrderID&& DataSource._orderItemsArray[i].Item.ProductId==ProductID)
            {
                return DataSource._orderItemsArray[i].ToString();
            }
        }
        return "item was not found! check order id or product id.";
    }
    public void UpdateOrderItem(int OrderID,int ProductID)
    {
        for (int i = 0; i < DataSource.Config.orderItemindex; i++)
        {
            if (DataSource._orderItemsArray[i].Item.OrderId == OrderID && DataSource._orderItemsArray[i].Item.ProductId == ProductID)
            {
                DataSource._orderItemsArray[i].Item.Amount = this.Item.Amount; //we can only change the amount in order.
                return;
            }
        }
        Console.WriteLine("item was not found in this order.");
    }
    public void DeleteOrderItem(int OrderID,int ProductID)
    {
        bool flag = true;
        for (int i = 0; i < DataSource.Config.orderItemindex; i++)
        {
            if (DataSource._orderItemsArray[i].Item.OrderId == OrderID && DataSource._orderItemsArray[i].Item.ProductId == ProductID)
            {
                for (int j = 0; j < DataSource.Config.orderItemindex; j++)
                {
                    if (j<DataSource.Config.orderItemindex)
                    {
                        DataSource._orderItemsArray[j] = DataSource._orderItemsArray[j +1];
                    }
                }
                flag = false;
                DataSource.Config.orderItemindex--;
                Console.WriteLine("item was deleted from order");
                return;
            }
        }
    }
    public DalOrderItem[] PrintOrderItems() //could add order id
    {
        /*DalOrderItem[] dalOrderItems=new DalOrderItem[DataSource.Config.orderItemindex];
        int index = 0;
        for (int i = 0; i < DataSource.Config.orderItemindex; i++)
        {
            if (DataSource._orderItemsArray[i].Item.OrderId==OrderID)
            {
                dalOrderItems[index++]=new DalOrderItem(DataSource._orderItemsArray[i].Item.ProductId,
                    DataSource._orderItemsArray[i].Item.OrderId,
                    DataSource._orderItemsArray[i].Item.Amount);
            }
        }
        return dalOrderItems;*/
        return DataSource._orderItemsArray;
    }
    #endregion
}
