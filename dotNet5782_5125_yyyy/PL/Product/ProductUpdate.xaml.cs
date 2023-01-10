using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
        #region data binding
        public string name
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }
        public static readonly DependencyProperty NameProperty =
        DependencyProperty.Register("name", typeof(string), typeof(AddProduct));
        public string price
        {
            get { return (string)GetValue(PriceProperty); }
            set { SetValue(PriceProperty, value); }
        }
        public static readonly DependencyProperty PriceProperty =
        DependencyProperty.Register("price", typeof(string), typeof(AddProduct));
        public string instock
        {
            get { return (string)GetValue(InStockProperty); }
            set { SetValue(InStockProperty, value); }
        }
        public static readonly DependencyProperty InStockProperty =
        DependencyProperty.Register("instock", typeof(string), typeof(AddProduct));
        public string id
        {
            get { return (string)GetValue(IDProperty); }
            set { SetValue(IDProperty, value); }
        }
        public static readonly DependencyProperty IDProperty =
        DependencyProperty.Register("id", typeof(string), typeof(AddProduct));
        public Array EnumsValue
        {
            get;
            set;
        }
        public BO.Enums.Category CategorySelected;
        #endregion
    
        /// <summary>
        /// ctor for update product window.
        /// notice we use same bl instance for singletone.
        /// </summary>
        /// <param name="_bl">as we get it from prevouis windows.</param>
        /// <param name="product"></param>
        public ProductUpdate(BlApi.IBL _bl,BO.ProductForList product)
        {
            bl= _bl;
            EnumsValue=Enum.GetValues(typeof(BO.Enums.Category));
            id = String.Format("{0:000000}",product.ID);
            name = product.ProductName;
            price =String.Format("{0:00.00}",product.Price)+"$";
            instock = String.Format("{0:00}", bl.Product.Get(product.ID).InStock);
            InitializeComponent();
        }
        /// <summary>
        /// clicking the update button, couses bl to ask dal to upadte datasource with the new arguments.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="Exception"></exception>
        private void UpdateProduct(object sender, RoutedEventArgs e)
        {
            try
            {
                int ID;
                int.TryParse(id, out ID);
                int InStock;
                if (!int.TryParse(instock, out InStock))
                    throw new Exception("In stock is not a number!");
                //int.TryParse(InStockBox.Text, out InStock);
                string Name = name != "" ? name : throw new Exception("Name is empty!");
                double Price;
                if (!double.TryParse(price, out Price))
                    throw new Exception("Price is not a number!");
                BO.Enums.Category category =CategorySelected;
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
                this.Close();
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
            catch (Exception ex)
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
            CategorySelected = (BO.Enums.Category)((ComboBox)sender).SelectedItem;
        }

        private void DeleteProduce(object sender, RoutedEventArgs e)
        {
            try
            {
                int ID;
                int.TryParse(id, out ID);
                bl.Product.Delete(ID);
                MessageBox.Show("Product was deleted from store!");
                this.Close();
            }
            catch (BO.ObjectNotFoundEx ex)
            {
                MessageBox.Show(ex.message); 
            }
            catch (BO.EmptyListEx ex)
            {
                MessageBox.Show(ex.message);
            }
            catch (BO.ProductInOrderEx ex)
            {
                MessageBox.Show(ex.message);
            }
        }
    }
}
