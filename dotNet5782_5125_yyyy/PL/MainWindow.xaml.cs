using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BL;
using PL.Cart;
using PL.Order;
using PL.Product;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BlApi.IBL? bl = BlApi.Factory.Get();
        #region data binding
        public string name
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }
        public static readonly DependencyProperty NameProperty =
        DependencyProperty.Register("name", typeof(string), typeof(MainWindow));
        public string adress
        {
            get { return (string)GetValue(AdressProperty); }
            set { SetValue(AdressProperty, value); }
        }
        public static readonly DependencyProperty AdressProperty =
        DependencyProperty.Register("adress", typeof(string), typeof(AddProduct));
        public string email
        {
            get { return (string)GetValue(EmailProperty); }
            set { SetValue(EmailProperty, value); }
        }
        public static readonly DependencyProperty EmailProperty =
        DependencyProperty.Register("email", typeof(string), typeof(AddProduct));
        #endregion
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Admin_click(sender, e);
        }
        void Admin_click(object sender, RoutedEventArgs e)
        {
            ProductListWindow p = new ProductListWindow(bl);
            //p.ProductListView_SelectionChanged(sender, null);
            p.Show();
           
        }

        private void Lets_buy_click(object sender, RoutedEventArgs e)
        {
            try
            {
                UserInfoCheck(name, adress,email);
                CartWindow cartWindow = new CartWindow(name, adress, email, bl);
                cartWindow.Show();
            }
            catch (BO.CustomerDetailsEx ex)
            {
                MessageBox.Show(ex.Message + "\nyour name\\email\\adress in invalid.");
            }
           
        }
        /// <summary>
        /// in order to prevent user to log in with invalid username adress etc, 
        /// we check it right here even though this is pl.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="adress"></param>
        /// <param name="email"></param>
        /// <exception cref="BO.CustomerDetailsEx"></exception>
        public void UserInfoCheck(string name, string adress, string email)
        {
            if (name == "" || email == "" || adress == "" || !IsValidEmail(email))
            {
                throw new BO.CustomerDetailsEx();//catch in main.
            }
        }
        bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false;
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }

        private void MyOrders(object sender, RoutedEventArgs e)
        {
            try
            {
                UserInfoCheck(name, adress, email);
                OrderListWindow olw = new OrderListWindow(bl,email);
                olw.Show();
            }
            catch (BO.CustomerDetailsEx ex)
            {
                MessageBox.Show(ex.Message + "\nyour name\\email\\adress in invalid.");
            }
            
        }
    }
}
