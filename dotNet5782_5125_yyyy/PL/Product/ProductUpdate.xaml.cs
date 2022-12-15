using BO;
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
    /// Interaction logic for ProductUpdate.xaml
    /// </summary>
    public partial class ProductUpdate : Window
    {
        BlApi.IBL bl;
        /// <summary>
        /// ctor for update product window.
        /// notice we use same bl instance for singletone.
        /// </summary>
        /// <param name="_bl">as we get it from prevouis windows.</param>
        /// <param name="product"></param>
        public ProductUpdate(BlApi.IBL _bl,BO.ProductForList product)
        {
            bl= _bl;
            InitializeComponent();
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));
            IDLabel.Content = product.ID.ToString();
            NameBox.Text=product.ProductName;
            PriceBox.Text=product.Price.ToString();
            InStockBox.Text = bl.Product.Get(product.ID).InStock.ToString();
            CategorySelector.Text=product.Category.ToString();
        }
        /// <summary>
        /// clicking the update button, couses bl to ask dal to upadte datasource with the new arguments.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="Exception"></exception>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int ID;
                int.TryParse(IDLabel.Content.ToString(), out ID);
                int InStock;
                if (!int.TryParse(InStockBox.Text, out InStock))
                    throw new Exception("In stock is not a number!");
                //int.TryParse(InStockBox.Text, out InStock);
                string Name = NameBox.Text != "" ? NameBox.Text : throw new Exception("Name is empty!");
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
                bl.Product.Update(p);
                MessageBox.Show("Product was updated!.");
            }
            catch (BO.OutOfRangeEx ex)
            {
                MessageBox.Show(ex.message);
            }
            catch (BO.InvalidParamsEx ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (BO.ObjectNotFoundEx ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// chanigng the category of this item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CategorySelector.Text=CategorySelector.Text.ToString();
        }
        /// <summary>
        /// changing the name of this item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            NameBox.Text = NameBox.Text.Trim();
        }
        /// <summary>
        /// chaning he price of this number.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PriceBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            PriceBox.Text = PriceBox.Text.Trim();
        }
        /// <summary>
        /// chaning amount in stock for this item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InStockBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            InStockBox.Text = InStockBox.Text.Trim();
        }
    }
}
