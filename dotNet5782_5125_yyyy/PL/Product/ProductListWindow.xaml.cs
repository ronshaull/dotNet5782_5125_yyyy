using BO;
using PL.Order;
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

namespace PL.Product
{
    /// <summary>
    /// Interaction logic for ProductListWindow.xaml
    /// </summary>
    public partial class ProductListWindow : Window
    { 
        BlApi.IBL bl;

        public ObservableCollection<ProductForList?> products
        {
            get { return (ObservableCollection<ProductForList>)GetValue(ProductProperty); }
            set { SetValue(ProductProperty, value); }
        }
        public ObservableCollection<BO.OrderForList?> orders
        {
            get { return (ObservableCollection<OrderForList>)GetValue(OrderProperty); }
            set { SetValue(OrderProperty, value); }
        }
        public static readonly DependencyProperty ProductProperty=
            DependencyProperty.Register("products",typeof(ObservableCollection<ProductForList>),typeof(ProductListWindow));
        public static readonly DependencyProperty OrderProperty =
            DependencyProperty.Register("orders", typeof(ObservableCollection<OrderForList>), typeof(ProductListWindow));
        public Array EnumsValue
        {
            get;
            set;
        }
        /// <summary>
        /// ctor for list view window.
        /// </summary>
        /// <param name="bl">we get the same bl forom main window, singeltone.</param>
        public ProductListWindow(BlApi.IBL bl)
        {
            products = new ObservableCollection<ProductForList?>(bl.Product.GetAll());
            orders = new ObservableCollection<BO.OrderForList?>(bl.Order.GetAll());
            EnumsValue = Enum.GetValues(typeof(BO.Enums.Category));
            InitializeComponent();
            this.bl = bl;
        }
        
        /// <summary>
        /// changes the list by filtering it by book type.
        /// to see all back agin choose all.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TypeSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BO.Enums.Category category=(BO.Enums.Category)((ComboBox)sender).SelectedItem; 
            switch (category)
            {
                case BO.Enums.Category.Programming_languages:
                    products = new ObservableCollection<ProductForList?>(bl.Product.GetAll(delegate (DO.Product? p) { return p?.Category == (DO.Enums.Category)0; }));
                    break;
                case BO.Enums.Category.Operating_systems:
                    products = new ObservableCollection<ProductForList?>(bl.Product.GetAll(delegate (DO.Product? p) { return p?.Category == (DO.Enums.Category)1; }));
                    break;
                case BO.Enums.Category.Computer_architecture:
                    products = new ObservableCollection<ProductForList?>(bl.Product.GetAll(delegate (DO.Product? p) { return p?.Category == (DO.Enums.Category)2; }));
                    break;
                case BO.Enums.Category.Cyber_security:
                    products = new ObservableCollection<ProductForList?>(bl.Product.GetAll(delegate (DO.Product? p) { return p?.Category == (DO.Enums.Category)3; }));
                    break;
                case BO.Enums.Category.All:
                    products = new ObservableCollection<ProductForList?>(bl.Product.GetAll());
                    break;
            }
           
        }
        /// <summary>
        /// the add button lets us add a new product to database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddProduct(object sender, RoutedEventArgs e)
        {
            AddProduct AP=new AddProduct(bl);
            AP.ShowDialog();
            products = new ObservableCollection<ProductForList?>(bl.Product.GetAll());
            
        }
        /// <summary>
        /// an Item double clicked event coused by double clicking an item in list view.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProductListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.ProductForList p = (BO.ProductForList)((ListView)sender).SelectedItem;
               //BO.ProductForList p = (BO.ProductForList)ProductListView.SelectedItem
            if (p==null)
            {
                return;
            }
            else
            {
                ProductUpdate pu = new ProductUpdate(bl,p);
                pu.ShowDialog();
                products = new ObservableCollection<ProductForList?>(bl.Product.GetAll());
            }
        }
        /// <summary>
        /// takes us to order update window for admin, there you could update shippment date
        /// and deliverd date.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OrderListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.OrderForList ofl = (BO.OrderForList)((ListView)sender).SelectedItem;
            OrderUpdateWindow ouw = new OrderUpdateWindow(bl,ofl);
            ouw.ShowDialog();
            orders = new ObservableCollection<OrderForList?>(bl.Order.GetAll());
        }
    }
}
