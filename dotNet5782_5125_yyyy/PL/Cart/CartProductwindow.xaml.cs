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

namespace PL.Cart
{
    /// <summary>
    /// Interaction logic for CartProductwindow.xaml
    /// </summary>
    public partial class CartProductwindow : Window
    {
        BlApi.IBL bl;
        BO.Cart MyCart;
        #region data binding
        public ProductItem ItemInCart
        {
            get { return (ProductItem)GetValue(ItemProperty); }
            set { SetValue(ItemProperty, value); }
        }
        public static readonly DependencyProperty ItemProperty =
        DependencyProperty.Register("ItemInCart", typeof(ProductItem), typeof(CartProductwindow));
        public string NewAmount
        {
            get { return (string)GetValue(NewAmountProperty); }
            set { SetValue(NewAmountProperty, value); }
        }
        public static readonly DependencyProperty NewAmountProperty =
        DependencyProperty.Register("NewAmount", typeof(string), typeof(CartProductwindow));
        #endregion
        public CartProductwindow(BlApi.IBL BL, BO.OrderItem Item,BO.Cart cart)
        {
            ItemInCart = BL.Product.Get(Item.ID, cart);
            NewAmount = "New Amount";
            InitializeComponent();
            this.bl = BL;
            MyCart = cart;
        }

        private void ChangeAmount(object sender, RoutedEventArgs e)
        {
            try
            {
                if (NewAmount == "")//no amount.
                {
                    MessageBox.Show("Please enter Amount");
                }
                int Amount = 0;
                if (int.TryParse(NewAmount, out Amount))
                {
                    bl.Cart.Update(MyCart,ItemInCart.ID, Amount);
                    MessageBox.Show("Product number:" + ItemInCart.ID.ToString() + " Amount has changed\n new amount is:" + NewAmount);
                    this.Close();
                }
            }
            catch (BO.ObjectNotFoundEx ex)
            {
                MessageBox.Show(ex.message);
            }
            catch (BO.InsufficientStockEx ex)
            {
                MessageBox.Show(ex.message);
            }
        }
    }
}
