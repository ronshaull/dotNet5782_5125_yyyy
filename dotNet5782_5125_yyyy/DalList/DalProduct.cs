using DO;
using DalApi;


namespace Dal;

internal class DalProduct : IProduct
{
    /*NOTICE: in this calss we use linq query and lambada expression for crud.
     * in other objects we use linq query using extenstion methods.
     */
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
            Product product =DataSource._productlist.FirstOrDefault(p => p?.ID == ID) ?? throw new DalApi.ObjectNotFoundEx();
            return product;
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
            int exp = DataSource._productlist.RemoveAll(p => p?.ID == ID);
            if (exp == 0)
                throw new DalApi.ObjectNotFoundEx();
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
            // we changed implemenation using linq to object query. and lambada expression.
            int exp=DataSource._productlist.RemoveAll(pr=> product.ID==pr?.ID);
            if (exp == 0)
                throw new DalApi.ObjectNotFoundEx();
            DataSource._productlist.Add(product);
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
                List<Product?> products = DataSource._productlist.Where(p=> p!=null).ToList();
                return products;
            }
            else
            {
                List<Product?> products = DataSource._productlist.Where(Select).ToList();
               /* foreach (Product product in DataSource._productlist)
                {
                    if (Select(product))
                    {
                        products.Add(product);
                    }
                }*/
                return products;
            }
        }
        catch (DalApi.EmptyListEx e)
        {
            throw;
        }
    }
    /// <summary>
    /// a special get function to retrive a certin Product, if it satisfy Select function.
    /// </summary>
    /// <param name="ID">of the product</param>
    /// <param name="Select">condition implemented by bollean function.</param>
    /// <returns></returns>
    /// <exception cref="DalApi.ObjectNotFoundEx"></exception>
    public Product Get(int ID, Func<Product?, bool>? Select)
    {
        try
        {
            Product p = DataSource._productlist.FirstOrDefault(p => p?.ID == ID) ?? throw new DalApi.ObjectNotFoundEx();
            if (Select(p))
                return p;
            else
                throw new DalApi.ObjectNotFoundEx();
        }
        catch (ObjectNotFoundEx e)
        {
            throw e;
        }
    }

    #endregion
}
