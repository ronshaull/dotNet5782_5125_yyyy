using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;

sealed internal class DalList : IDal
{
    #region fields
    /// <summary>
    /// for singeltone design pattern!
    /// intance will be created only once in the whole program.
    /// </summary>
    public static IDal Instance { get; } = new DalList();

    public IProduct Product => new DalProduct();

    public IOrder Order => new DalOrder();

    public IOrderItem OrderItem => new DalOrderItem();
    #endregion
    #region ctor
    /// <summary>
    /// for singeltone design pattern.
    /// </summary>
    private DalList()
    {
    }
    #endregion
}
