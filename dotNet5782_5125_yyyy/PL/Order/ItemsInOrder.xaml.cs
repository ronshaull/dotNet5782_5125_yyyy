using BlApi;
using BO;
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
    /// Interaction logic for ItemsInOrder.xaml
    /// </summary>
    public partial class ItemsInOrder : Window
    {
        BlApi.IBL bl;
        BO.Order Order;
        public ObservableCollection<BO.OrderItem?> Items { get; set; }
        public ItemsInOrder(BlApi.IBL bl,OrderForList order)
        {
            Order = bl.Order.Get(order.ID);
            Items = new ObservableCollection<BO.OrderItem>(Order.OrderItems);
            InitializeComponent();
            this.bl = bl;
        }
    }
}
