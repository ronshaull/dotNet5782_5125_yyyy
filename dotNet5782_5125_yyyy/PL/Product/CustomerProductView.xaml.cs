using System;
using System.Collections.Generic;
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

namespace PL.Product
{
    /// <summary>
    /// Interaction logic for CustomerProductView.xaml
    /// </summary>
    public partial class CustomerProductView : Window
    {
        BlApi.IBL bl;
        public CustomerProductView(BlApi.IBL bl, BO.Product product)
        {
            InitializeComponent();
            this.bl = bl;
            List<BO.Product> tmp=new List<BO.Product>();
            tmp.Add(product);
            ProductListView.ItemsSource = tmp;
        }
    }
}
