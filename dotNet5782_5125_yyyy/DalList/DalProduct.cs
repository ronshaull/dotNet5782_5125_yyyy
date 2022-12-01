using DO;
using DalApi;

namespace Dal;

internal class DalProduct : IProduct
{
    #region CRUD functions
    /// <summary>
    /// addding product to data source.
    /// </summary>
    /// <param name="product">of the product that we want to add.</param>
    /// <returns></returns>
    /// <exception cref="OutOfRangeEx">no more space in product list</exception>
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
            DataSource._productlist.Add(product);
            return product.ID;
        }
        catch (DalApi.OutOfRangeEx e)
        {
            throw;
        }
    }
    /// <summary>
    /// to retrive a product that we need.
    /// </summary>
    /// <param name="ID">of the wanted product.</param>
    /// <returns></returns>
    public Product Get(int ID) 
    {
        try
        {
            Product product=DataSource._productlist.FirstOrDefault(p => p?.ID == ID) ?? throw new DalApi.ObjectNotFoundEx();
            return product;
            /*
            for (int i = 0; i < DataSource._productlist.Count; i++)
            {
                if (DataSource._productlist[i]?.ID == ID)
                {
                    return (Product)DataSource._productlist[i];
                }
            }
            throw new ObjectNotFoundEx();*/
        }
        catch (ObjectNotFoundEx e)
        {
            throw e;
        }
        
    }
    /// <summary>
    /// to delete a certin product form data base.
    /// </summary>
    /// <param name="ID">of the product that we want to delete.</param>
    /// <exception cref="EmptyListEx"></exception>
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
                if (DataSource._productlist[i]?.ID ==ID)//product was found
                {
                    DataSource._productlist.RemoveAt(i);
                    //TODO: maybe short writing.
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
    /// <summary>
    /// to update a certin prudct.
    /// </summary>
    /// <param name="product">that holds all new fields.</param>
    /// <exception cref="EmptyListEx"></exception>
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
                if (DataSource._productlist[i]?.ID == product.ID)//product was found
                {
                    //start updateting product.
                    DataSource._productlist[i]=product;
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
    /// <summary>
     /// to retrive all products from data base.
     /// </summary>
     /// <returns></returns>
    public IEnumerable<Product?> GetAll(Func<Product?, bool>? Select = null)
    {
        try
        {
            if (DataSource._productlist.Count==0)
            {
                throw new DalApi.EmptyListEx();
            }
            if (Select==null)
            {
                List<Product?> products = new List<Product?>();
                foreach (Product product in DataSource._productlist)
                {
                    products.Add(product);
                }
                return products;
            }
            else
            {
                List<Product?> products = new List<Product?>();
                foreach (Product product in DataSource._productlist)
                {
                    if (Select(product))
                    {
                        products.Add(product);
                    }
                }
                return products;
            }
        }
        catch (DalApi.EmptyListEx e)
        {
            throw;
        }
    }
       
    #endregion
}
