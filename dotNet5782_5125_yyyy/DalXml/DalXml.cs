using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    internal class DalXml : IDal
    {
        public static IDal Instance { get; }= new DalXml();

        public IProduct Product { get; } = new Dal.DoProduct();

        public IOrder Order { get; } = new Dal.DoOrder();

        public IOrderItem OrderItem { get; } = new Dal.DoOrderItem();

        #region ctor
        /// <summary>
        /// for singeltone design pattern.
        /// </summary>
        private DalXml()
        {
        }
        #endregion
    }
}
