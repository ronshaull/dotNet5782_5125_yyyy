using PL.Cart;
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
    /// Interaction logic for AddProduct.xaml
    /// </summary>
    public partial class AddProduct : Window
    {
        BlApi.IBL bl;
        #region data binding
        public string Addname {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }
        public static readonly DependencyProperty NameProperty =
        DependencyProperty.Register("Addname", typeof(string), typeof(AddProduct));
        public string AddPrice
        {
            get { return (string)GetValue(PriceProperty); }
            set { SetValue(PriceProperty, value); }
        }
        public static readonly DependencyProperty PriceProperty =
        DependencyProperty.Register("AddPrice", typeof(string), typeof(AddProduct));
        public string AddInStock
        {
            get { return (string)GetValue(InStockProperty); }
            set { SetValue(InStockProperty, value); }
        }
        public static readonly DependencyProperty InStockProperty =
        DependencyProperty.Register("AddInStock", typeof(string), typeof(AddProduct));
        public Array EnumsValue
        {   get;
            set;
        }
        public BO.Enums.Category CategorySelected;
        #endregion
        /// <summary>
        /// ctor for add product window.
        /// </summary>
        /// <param name="_bl"></param>
        public AddProduct(BlApi.IBL _bl)
        {
            bl = _bl;
            EnumsValue= Enum.GetValues(typeof(BO.Enums.Category));
            InitializeComponent();
        }
        /// <summary>
        /// choosing an id, not functional, id is generated in dal layer. from datasource to prevent doubles.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IDBox_TextChanged(object sender, TextChangedEventArgs e)
        {
           
        }
        /// <summary>
        /// choosing name for new product.
        /// cannnot be empty.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }
        /// <summary>
        /// choose price for this product.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PriceBox_TextChanged(object sender, TextChangedEventArgs e)
        {
        }
        /// <summary>
        /// how much in stock.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InStockBox_TextChanged(object sender, TextChangedEventArgs e)
        {
          
        }
        /// <summary>
        /// chaning a category for new prduct.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CategorySelected = (BO.Enums.Category)((ComboBox)sender).SelectedItem;
        }
        /// <summary>
        /// clicking the add button event trigers this function.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_Product(object sender, RoutedEventArgs e)
        {
            try
            {
                int InStock;
                if (!int.TryParse(AddInStock, out InStock))
                    throw new Exception("In stock is not a number!");
                //int.TryParse(InStockBox.Text, out InStock);
                string Name = Addname!=""?Addname:throw new Exception("Name is empty!");
                double Price;
                if (!double.TryParse(AddPrice, out Price))
                    throw new Exception("Price is not a number!");
                BO.Product p = new BO.Product()
                {
                    Category = CategorySelected,
                    InStock = InStock,
                    Name = Addname,
                    Price = Price
                };
                bl.Product.Add(p);
                MessageBox.Show("Product added to store!.");
                this.Close();
            }
            catch (BO.OutOfRangeEx ex)
            {
                MessageBox.Show(ex.message);
            }
            catch(BO.InvalidParamsEx ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
