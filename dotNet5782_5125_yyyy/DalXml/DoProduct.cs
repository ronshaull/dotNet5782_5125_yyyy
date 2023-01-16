using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DalApi;
using DO;   
namespace Dal
{
    internal class DoProduct : IProduct
    {
        #region xml properties
        XElement ProductRoot;
        XElement ConfigRoot;
        string ProductPath = @"..\xml\Products.xml";
        public DoProduct()
        {
            if (!File.Exists(ProductPath))
                CreateFiles();
            else
                LoadData();
        }
        private void CreateFiles()
        {
            ProductRoot = new XElement("Products");
            ProductRoot.Save(ProductPath);
            ConfigRoot = XElement.Load(@"C:\Users\user\source\repos\ronshaull\dotNet5782_5125_yyyy\dotNet5782_5125_yyyy\config.xml");
        }
        private void LoadData()
        {
            try
            {
                ProductRoot = XElement.Load(ProductPath);
                ConfigRoot = XElement.Load(@"C:\Users\user\source\repos\ronshaull\dotNet5782_5125_yyyy\dotNet5782_5125_yyyy\config.xml");
            }
            catch
            {
                Console.WriteLine("File upload problem");
            }
        }
        #endregion
        public int Add(Product product)
        {
            LoadData();
            int id = 0;
            XElement ProductID = ConfigRoot.Element("ProductID");
            int.TryParse(ConfigRoot.Element("ProductID").Value, out id);
            id++;
            ProductID.Value = id.ToString();
            ConfigRoot.Save(@"C:\Users\user\source\repos\ronshaull\dotNet5782_5125_yyyy\dotNet5782_5125_yyyy\config.xml");
            XElement ID = new XElement("ID", id);
            XElement Name = new XElement("Name", product.Name);
            XElement Price = new XElement("Price", product.Price);
            XElement Category = new XElement("Category",(int)product.Category);
            XElement InStock = new XElement("InStock", product.InStock);
            ProductRoot.Add(new XElement("Product",ID,Name,Price,Category,InStock));
            ProductRoot.Save(ProductPath);
            return 0;
        }

        public void Delete(int ID)
        {
            {
                XElement ProductElement;
                try
                {
                    ProductElement = (from p in ProductRoot.Elements()
                                      where Convert.ToInt32(p.Element("ID").Value) == ID
                                      select p).FirstOrDefault();
                    if (ProductElement==null)
                    {
                        throw new ObjectNotFoundEx();
                    }
                    ProductElement.Remove();
                    ProductRoot.Save(ProductPath);
                }
                catch (DalApi.ObjectNotFoundEx e)
                {
                    throw e; 
                }

            }
        }

        public Product Get(int id)
        {
            LoadData();
            DO.Product? product;
            try
            {
                product = (from p in ProductRoot.Elements()
                           where Convert.ToInt32(p.Element("ID").Value) == id
                           select new DO.Product()
                           {
                               ID = Convert.ToInt32(p.Element("ID").Value),
                               Category = (DO.Enums.Category)Convert.ToInt32(p.Element("Category").Value),
                               InStock = Convert.ToInt32(p.Element("InStock").Value),
                               Name = p.Element("Name").Value,
                               Price = Convert.ToDouble(p.Element("Price").Value)
                           }).FirstOrDefault();
                if (product==null)
                {
                    throw new ObjectNotFoundEx();
                }
                return (Product)product;
            }
            catch (ObjectNotFoundEx e)
            {

                throw e;
            }

        }

        public Product Get(int ID, Func<Product?, bool>? Select)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product?> GetAll(Func<Product?, bool>? Select = null)
        {
            try
            {
                LoadData();
                if (!ProductRoot.HasElements)
                {
                    throw new DalApi.EmptyListEx();
                }
                if (Select == null)
                {
                    List<Product> products = (from p in ProductRoot.Elements()
                                               select new Product()
                                               {
                                                   ID = Convert.ToInt32(p.Element("ID").Value),
                                                   Category = (DO.Enums.Category)Convert.ToInt32(p.Element("Category").Value),
                                                   InStock = Convert.ToInt32(p.Element("InStock").Value),
                                                   Name = p.Element("Name").Value,
                                                   Price = Convert.ToDouble(p.Element("Price").Value)
                                               }).ToList();
                    return products.ConvertAll<Product?>(i => i);
                }
                else
                {
                    List<Product> products= (from p in ProductRoot.Elements()
                                              select new Product()
                                              {
                                                  ID = Convert.ToInt32(p.Element("ID").Value),
                                                  Category = (DO.Enums.Category)Convert.ToInt32(p.Element("Category").Value),
                                                  InStock = Convert.ToInt32(p.Element("InStock").Value),
                                                  Name = p.Element("Name").Value,
                                                  Price = Convert.ToDouble(p.Element("Price").Value)
                                              }).ToList();
                    return products.ConvertAll<Product?>(i => i).Where(Select);
                    
                }
            }
            catch (DalApi.EmptyListEx e)
            {
                throw;
            }
        }

        public void Update(Product product)
        {
            try
            {
                XElement ProductElement = (from p in ProductRoot.Elements()
                                           where Convert.ToInt32(p.Element("ID").Value) == product.ID
                                           select p).FirstOrDefault();
                if (ProductElement == null)
                    throw new ObjectNotFoundEx();
                ProductElement.Element("Name").Value = product.Name;
                ProductElement.Element("Price").Value = product.Price.ToString();
                ProductElement.Element("Category").Value = ((int)product.Category).ToString();
                ProductElement.Element("InStock").Value = product.InStock.ToString();   
                ProductRoot.Save(ProductPath);
            }
            catch (ObjectNotFoundEx e)
            {
                throw e;
            }
        }
    }
}
