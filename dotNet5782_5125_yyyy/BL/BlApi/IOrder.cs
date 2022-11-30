using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;

public interface IOrder
{
    /// <summary>
    /// function used for getting a list of specific orders.
    /// </summary>
    /// <returns></returns>
    public IEnumerable<OrderForList> GetAll();
    /// <summary>
    /// function used to get details of specific order.
    /// </summary>
    /// <param name="ID">that we want to retreive.</param>
    /// <returns></returns>
    public BO.Order Get(int ID);
    /// <summary>
    /// for Customer screen, updating the delivery time.
    /// </summary>
    /// <param name="ID">that we want to update.</param>
    /// <returns></returns>
    public BO.Order UpdateDelivery(int ID);
    /// <summary>
    /// for manager screen to update shipment.
    /// </summary>
    /// <param name=""></param>
    /// <returns></returns>
    public BO.Order UpdateShipment(int ID);
    /// <summary>
    /// function used to claim an order to be deliverd.
    /// </summary>
    /// <param name="ID">that was deliverd.</param>
    /// <returns></returns>
    public BO.Order Deliverd(int ID);
    /// <summary>
    /// used for manager user, to track an order.
    /// </summary>
    /// <param name="ID">that we want to track.</param>
    /// <returns></returns>
    public BO.OrderTracking OrderTracking(int ID); 
}
