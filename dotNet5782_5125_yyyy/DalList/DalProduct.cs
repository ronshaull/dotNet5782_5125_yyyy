using DO;
using DalApi;

namespace Dal;

internal class DalProduct : IProduct
{
    //fields
    public Product product;

    #region ctors
    /// <summary>
    /// deafault ctor for creating access to data source object.
    /// </summary>
    public DalProduct()
    {
        this.product = new Product();
    }
    /// <summary>
    /// explicit values of product. 
    /// in case we ant to create a a new product.
    /// </summary>
    /// <param name="_ID"></param>
    /// <param name="_Name"></param>
    /// <param name="_Price"></param>
    /// <param name="_Category"></param>
    /// <param name="_InStock"></param>
    public DalProduct(int _ID, string _Name, double _Price, Enums.Category _Category, int _InStock)
    {
        product= new Product();
        product.ID = _ID;   
        product.Name = _Name;
        product.Price = _Price;
        product.Category = _Category;
        product.InStock = _InStock;
    }
    #endregion
    #region Override Functions
    public override string ToString()
    {
        return product.ToString();
    }
    #endregion
    #region CRUD functions
    public int Add(Product product)
    {
        try
        {  
            //first check if there is inough space.
            if (DataSource._productlist.Count==DataSource.NUMBER_OF_PRODUCTS)
            {
                throw new OutOfRangeEx();
            }
            //there is space.
            product.ID = DataSource.Config.product_Id;
            DataSource._productlist.Add( new DalProduct(product.ID,
                product.Name,
                product.Price,
                product.Category,
                product.InStock));
            return product.ID;
        }
        catch (DalApi.OutOfRangeEx e)
        {
            throw;
        }
    }

    public Product Get(int ID) 
    {
        try
        {
            for (int i = 0; i < DataSource._productlist.Count; i++)
            {
                if (DataSource._productlist[i].product.ID == ID)
                {
                    Product get_product=new Product();

                    get_product.ID = DataSource._productlist[i].product.ID;
                    get_product.Name = DataSource._productlist[i].product.Name;
                    get_product.Price = DataSource._productlist[i].product.Price;
                    get_product.Category = DataSource._productlist[i].product.Category;
                    get_product.InStock = DataSource._productlist[i].product.InStock;
                    return get_product;
                }
            }
            throw new ObjectNotFoundEx();
        }
        catch (ObjectNotFoundEx e)
        {
            throw e;
        }
        
    }

    public void Delete(int ID)
    {
        try
        {
            if (DataSource._productlist.Count==0)
            {
                throw new EmptyListEx();
            }
            bool flag = true;//flag is true until we delete the product.
            for (int i = 0; i < DataSource._productlist.Count; i++)
            {
                if (!flag)
                {
                    break;
                }
                if (DataSource._productlist[i].product.ID ==ID)//product was found
                {
                    DataSource._productlist.RemoveAt(i);
                    flag = false;
                }
            }
            if (flag)
            {
                throw new DalApi.ObjectNotFoundEx();
            }
            else
                Console.WriteLine("product was deleted.");
            return;
        }
        catch(DalApi.ObjectNotFoundEx e)
        {
            throw;
        }
    }

    public void Update(Product product)
    {
        try
        {
            if (DataSource._productlist.Count==0)
            {
                throw new EmptyListEx();
            }
            for (int i = 0; i < DataSource._productlist.Count; i++)//first find product.
            {
                if (DataSource._productlist[i].product.ID == product.ID)//product was found
                {
                    //start updateting product.
                    DataSource._productlist[i].product.Name = product.Name;
                    DataSource._productlist[i].product.Price = product.Price;
                    DataSource._productlist[i].product.Category = product.Category;
                    DataSource._productlist[i].product.InStock = product.InStock;
                    Console.WriteLine("product was updated."); //letting user know update was successful.
                    return;
                }
            }
            throw new DalApi.ObjectNotFoundEx();
        }
        catch(DalApi.EmptyListEx e)
        {
            throw e;
        }
        catch (DalApi.ObjectNotFoundEx e)
        {
            throw e;
        }
    }
     
    public IEnumerable<Product> GetAll()
    {
        try
        {
            if (DataSource._productlist.Count==0)
            {
                throw new DalApi.EmptyListEx();
            }
            List<Product> products = new List<Product>();
            foreach (DalProduct product in DataSource._productlist)
            {
                Product curr = new Product();
                curr.ID = product.product.ID;
                curr.Name = product.product.Name;
                curr.Price = product.product.Price;
                curr.Category = product.product.Category;
                curr.InStock = product.product.InStock;
                products.Add(curr);
            }
            return products;
        }
        catch (DalApi.EmptyListEx e)
        {
            throw;
        }
    }
       
    #endregion
}
