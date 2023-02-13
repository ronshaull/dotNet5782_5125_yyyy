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
        /// <summary>
        /// the read only ensure this field caould be initialize only once!
        /// 
        /// </summary>
        public readonly static IDal instance = new DalXml();

        public IProduct Product { get; } = new Dal.DoProduct();

        public IOrder Order { get; } = new Dal.DoOrder();

        public IOrderItem OrderItem { get; } = new Dal.DoOrderItem();

        #region ctor
        /// <summary>
        /// for singeltone design pattern.
        /// private ctro insures that instances of this class could be
        /// created only from within the class (instace)
        /// </summary>
        private DalXml()
        {
        }
        /// <summary>
        /// for lazy careation, we use a static empty ctor
        /// this insure that the instance of this class would be created 
        /// only when its used inside the code, and not on program load.
        /// </summary>
        static DalXml()
        {

        }
        public static IDal Instance
        {
            get { return instance; }
        }
        #endregion
    }
}
