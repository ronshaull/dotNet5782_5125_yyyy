using BO;
using PL.Order;
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

namespace PL.Cart
{
    /// <summary>
    /// Interaction logic for CartWindow.xaml
    /// </summary>
    public partial class CartWindow : Window
    {
        string username;
        string adress;
        string email;
        BlApi.IBL bl;
        #region data binding
        BO.Cart MyCart = new BO.Cart();
        ObservableCollection<BO.OrderItem> cartItems
        {
            get { return (ObservableCollection<BO.OrderItem>)GetValue(CartProperty); }
            set { SetValue(CartProperty, value); }
        }
        public static readonly DependencyProperty CartProperty =
        DependencyProperty.Register("cartItems", typeof(ObservableCollection<BO.OrderItem>), typeof(CartWindow));

        public string TotalPrice
        {
            get { return (string)GetValue(TotalProperty); }
            set { SetValue(TotalProperty, value); }
        }
        public static readonly DependencyProperty TotalProperty =
        DependencyProperty.Register("TotalPrice", typeof(string), typeof(CartWindow));

        public ObservableCollection<ProductForList?> products
        {
            get { return (ObservableCollection<ProductForList>)GetValue(ProductProperty); }
            set { SetValue(ProductProperty, value); }
        }
        public static readonly DependencyProperty ProductProperty =
        DependencyProperty.Register("products", typeof(ObservableCollection<ProductForList>), typeof(CartWindow));

        public ObservableCollection<ProductForList> ProgramingLanguges { get; set; }
        public ObservableCollection<ProductForList> OS { get; set; }
        public ObservableCollection<ProductForList> ComputerArchitecture { get; set; }
        public ObservableCollection<ProductForList> CyberSecurity { get; set; }
        #endregion
        public CartWindow(string username, string adress, string email, BlApi.IBL bl)
        {
            products = new ObservableCollection<BO.ProductForList?>(bl.Product.GetAll());
            cartItems = new ObservableCollection<BO.OrderItem>(MyCart.Items);
            var grouping = from w in bl.Product.GetAll()
                           group w by w?.Category;
            foreach (var item in grouping)
            {
                switch (item.Key)
                {
                    case (BO.Enums.Category)0:
                        ProgramingLanguges = new ObservableCollection<ProductForList>(item);
                        break;
                    case (BO.Enums.Category)1:
                        OS = new ObservableCollection<ProductForList>(item);
                        break;
                    case (BO.Enums.Category)2:
                        ComputerArchitecture = new ObservableCollection<ProductForList>(item);
                        break;
                    case (BO.Enums.Category)3:
                        CyberSecurity = new ObservableCollection<ProductForList>(item);
                        break;
                    default:
                        break;
                }
            }
            TotalPrice = "Total: " + String.Format("{0:00.00}", MyCart.Total) + "$";
            InitializeComponent();
            this.username = username;
            this.adress = adress;
            this.email = email;
            this.bl = bl;
        }
        #region buy and add
        /// <summary>
        /// add to cart buttomn was cklicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddToCart(object sender, RoutedEventArgs e)
        {
            try
            {
                if (AmountBox.Text == "")//no amount.
                {
                    MessageBox.Show("Please enter Amount");
                }
                if (ProductIDBox.Text == "")//no product ID
                {
                    MessageBox.Show("Please enter product id");
                }
                int ID=0;
                int Amount=0;
                if (Int32.TryParse(ProductIDBox.Text, out ID) && Int32.TryParse(AmountBox.Text, out Amount))
                {
                    bl.Cart.Add(MyCart, ID);
                    bl.Cart.Update(MyCart, ID, Amount);
                    MessageBox.Show("Product number:" + ProductIDBox.Text + "\n was added to your cart\n your amount is:" + AmountBox.Text);
                    cartItems = new ObservableCollection<BO.OrderItem>(MyCart.Items);
                    TotalPrice = "Total: " + String.Format("{0:00.00}", MyCart.Total) + "$";
                }
                else
                {
                    throw new FormatException();
                }
            }
            catch (BO.ObjectNotFoundEx ex)
            {
                MessageBox.Show(ex.message+"\n"+"please check product id.");
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Bad ID or Amount! \n it is not a number!");
            }
            catch (BO.InsufficientStockEx ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch(BO.OutOfStockEx ex)
            {
                MessageBox.Show(ex.message);
            }
        }
        private void Buy_Cart(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.Cart.Buy(MyCart, username, email, adress);
                MessageBox.Show("your order was adeed!\n cart is now empty so you could buy more.\n yo can view your order by clicking the view my order button.");
                MyCart.Items.Clear();
                cartItems = new ObservableCollection<BO.OrderItem>(MyCart.Items);
                MyCart.Total = 0;
                TotalPrice = "Total: " + String.Format("{0:00.00}", MyCart.Total) + "$";
            }
            catch (BO.CustomerDetailsEx ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (BO.GeneralEx ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (BO.ObjectNotFoundEx ex)
            {
                MessageBox.Show(ex.message);
            }
        }
        #endregion
        #region tree lists double click.
        private void ProductListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.ProductForList product= (BO.ProductForList)((ListView)sender).SelectedItem;
            ProductIDBox.Text = product.ID.ToString();
            BO.Product p = bl.Product.Get(product.ID);
            CustomerProductView cpv =new CustomerProductView(bl, p);
            cpv.Show();
        }
        private void LangugesListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.ProductForList product = (BO.ProductForList)((ListView)sender).SelectedItem;
            ProductIDBox.Text = product.ID.ToString();
            BO.Product p = bl.Product.Get(product.ID);
            CustomerProductView cpv = new CustomerProductView(bl, p);
            cpv.Show();

        }
        private void OsListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.ProductForList product = (BO.ProductForList)((ListView)sender).SelectedItem;
            ProductIDBox.Text = product.ID.ToString();
            BO.Product p = bl.Product.Get(product.ID);
            CustomerProductView cpv = new CustomerProductView(bl, p);
            cpv.Show();

        }
        private void CyberSecurityListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.ProductForList product = (BO.ProductForList)((ListView)sender).SelectedItem;
            ProductIDBox.Text = product.ID.ToString();
            BO.Product p = bl.Product.Get(product.ID);
            CustomerProductView cpv = new CustomerProductView(bl, p);
            cpv.Show();

        }
        private void ComputerArchitectureListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.ProductForList product = (BO.ProductForList)((ListView)sender).SelectedItem;
            ProductIDBox.Text = product.ID.ToString();
            BO.Product p = bl.Product.Get(product.ID);
            CustomerProductView cpv = new CustomerProductView(bl, p);
            cpv.Show();

        }
        private void CartListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.OrderItem? CartItem =(BO.OrderItem?)((ListView)sender).SelectedItem;
            CartProductwindow cpw = new CartProductwindow(bl, CartItem, MyCart);
            cpw.ShowDialog();
            cartItems = new ObservableCollection<BO.OrderItem>(MyCart.Items);
            TotalPrice = "Total: " + String.Format("{0:00.00}", MyCart.Total) + "$";
        }
        #endregion
        private void ViewMyOrders_buttonClick(object sender, RoutedEventArgs e)
        {
            OrderListWindow olw = new OrderListWindow(bl, email);
            olw.Show();
        }
    }
}
