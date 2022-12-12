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
    /// Interaction logic for AddProduct.xaml
    /// </summary>
    public partial class AddProduct : Window
    {
        BlApi.IBL bl;
        /// <summary>
        /// ctor for add product window.
        /// </summary>
        /// <param name="_bl"></param>
        public AddProduct(BlApi.IBL _bl)
        {
            bl = _bl;
            InitializeComponent();
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));
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
        }
        /// <summary>
        /// clicking the add button event trigers this function.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int ID;
                int.TryParse(IDBox.Text, out ID);
                int InStock;
                if (!int.TryParse(InStockBox.Text, out InStock))
                    throw new Exception("In stock is not a number!");
                //int.TryParse(InStockBox.Text, out InStock);
                string Name = NameBox.Text!=""?NameBox.Text:throw new Exception("Name is empty!");
                double Price;
                if (!double.TryParse(PriceBox.Text, out Price))
                    throw new Exception("Price is not a number!");
                BO.Enums.Category category = (BO.Enums.Category)CategorySelector.SelectedItem;
                BO.Product p = new BO.Product()
                {
                    Category = category,
                    ID = ID,
                    InStock = InStock,
                    Name = Name,
                    Price = Price
                };
                bl.Product.Add(p);
                MessageBox.Show("Product added to store!.");
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
