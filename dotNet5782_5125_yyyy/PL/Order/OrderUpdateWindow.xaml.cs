using BO;
using PL.Product;
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
        public BO.OrderTracking OrderTracking
        {
            get { return (OrderTracking)GetValue(TrackingProperty); }
            set { SetValue(TrackingProperty, value); }
        }
        public static readonly DependencyProperty TrackingProperty =
        DependencyProperty.Register("OrderTracking", typeof(BO.OrderTracking), typeof(OrderUpdateWindow));
        public OrderUpdateWindow(BlApi.IBL bl,BO.OrderForList order)
        {
            OrderTracking = bl.Order.OrderTracking(order.ID);
            InitializeComponent();
            this.bl = bl;
            
        }

        private void UpdateShippment_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.Order.UpdateShipment(OrderTracking.ID);
                OrderTracking = bl.Order.OrderTracking(OrderTracking.ID);
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
                bl.Order.UpdateDelivery(OrderTracking.ID);
                OrderTracking = bl.Order.OrderTracking(OrderTracking.ID);
                MessageBox.Show("Delivery date was updated.");
            }
            catch (BO.GeneralEx ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
    /*
     *  <ListView Grid.Row="1" ItemsSource="{Binding}" HorizontalAlignment="Center" >
            <ListView.View>
                <GridView>
                    <GridViewColumn/>
                </GridView>
            </ListView.View>
        </ListView>
     */
}
