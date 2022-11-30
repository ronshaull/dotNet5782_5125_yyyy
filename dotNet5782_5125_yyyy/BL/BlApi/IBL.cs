using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;
/// <summary>
/// main interface that includes all primary BO.Objects interfaces.
/// </summary>
public interface IBL
{
    public IProduct Product { get;  }
    public ICart Cart { get; }
    public IOrder Order { get; }
}
