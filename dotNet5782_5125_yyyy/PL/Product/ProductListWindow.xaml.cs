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
    /// Interaction logic for ProductListWindow.xaml
    /// </summary>
    public partial class ProductListWindow : Window
    { 
        BlApi.IBL bl;
        /// <summary>
        /// ctor for list view window.
        /// </summary>
        /// <param name="bl">we get the same bl forom main window, singeltone.</param>
        public ProductListWindow(BlApi.IBL bl)
        {
            this.bl = bl;
            InitializeComponent();
            ProductListView.ItemsSource = bl.Product.GetAll();
            //ProductListView_SelectionChanged(this, null);
            TypeSelector.Items.Clear();
            TypeSelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));
        }
        /// <summary>
        /// changes the list by filtering it by book type.
        /// to see all back agin choose all.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TypeSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BO.Enums.Category category=(BO.Enums.Category)TypeSelector.SelectedItem; 
            switch (category)
            {
                case BO.Enums.Category.Programming_languages:
                    {
                        ProductListView.ItemsSource= bl.Product.GetAll(delegate(DO.Product? p){ return p?.Category == 0; });
                    }
                    break;
                case BO.Enums.Category.Operating_systems:
                    ProductListView.ItemsSource = bl.Product.GetAll(delegate (DO.Product? p) { return p?.Category ==(DO.Enums.Category)1; });
                    break;
                case BO.Enums.Category.Computer_architecture:
                    ProductListView.ItemsSource = bl.Product.GetAll(delegate (DO.Product? p) { return p?.Category == (DO.Enums.Category)2; });
                    break;
                case BO.Enums.Category.Cyber_security:
                    ProductListView.ItemsSource = bl.Product.GetAll(delegate (DO.Product? p) { return p?.Category == (DO.Enums.Category)3; });
                    break;
                case BO.Enums.Category.All:
                    ProductListView.ItemsSource = bl.Product.GetAll();
                    break;
            }
           
        }
        /// <summary>
        /// the add button lets us add a new product to database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddProduct AP=new AddProduct(bl);
            AP.Show();
        }
        /// <summary>
        /// an Item double clicked event coused by double clicking an item in list view.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProductListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.ProductForList p = (BO.ProductForList)ProductListView.SelectedItem;
            if (p==null)
            {
                return;
            }
            else
            {
                ProductUpdate pu = new ProductUpdate(bl,p);
                pu.Show();
            }
        }

    }
}
