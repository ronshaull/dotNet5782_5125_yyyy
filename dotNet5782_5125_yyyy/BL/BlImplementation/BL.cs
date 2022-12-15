using BlApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlImplementation;


/// <summary>
/// this class is used to access all primary BO.Objects functioality from main program.
/// </summary>
sealed internal class BL : IBL
{
    public IProduct Product => new BOProduct();

    public ICart Cart => new BOCart();

    public IOrder Order => new BOOrder();
}
