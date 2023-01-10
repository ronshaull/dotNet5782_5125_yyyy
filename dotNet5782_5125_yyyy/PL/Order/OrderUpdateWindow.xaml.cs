using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL.Order
{
    /// <summary>
    /// Interaction logic for OrderUpdateWindow.xaml
    /// </summary>
    public partial class OrderUpdateWindow : Window
    {
        BlApi.IBL bl;
        BO.Order Order;
        ObservableCollection<BO.Order> MyOrder;
        public OrderUpdateWindow(BlApi.IBL bl,BO.OrderForList order)
        {
            InitializeComponent();
            this.bl = bl;
            List<BO.Order> tmp = new List<BO.Order>();
            tmp.Add(bl.Order.Get(order.ID));
            Order = tmp[0];
            MyOrder = new ObservableCollection<BO.Order>(tmp);
            this.DataContext = MyOrder;
            
        }

        private void UpdateShippment_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.Order.UpdateShipment(Order.ID);
                MyOrder[0]=bl.Order.Get(Order.ID);
                MessageBox.Show("Shippment date was updated.");
            }
            catch (BO.GeneralEx ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpdateDelivery_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.Order.UpdateDelivery(Order.ID);
                MyOrder[0] = bl.Order.Get(Order.ID);
                MessageBox.Show("Delivery date was updated.");
            }
            catch (BO.GeneralEx ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
