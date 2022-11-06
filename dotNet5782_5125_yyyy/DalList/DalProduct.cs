using DO;

namespace Dal;

public class DalProduct //we are using wrapper design pattern.
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
    public void AddProduct()
    {
        try
        {   //first check if there is inough space.
            if (DataSource.Config.productIndex==DataSource.NUMBER_OF_PRODUCTS-1)
            {
                throw new Exception("insufficient space,product array is full");
            }
            //there is space.
            this.product.ID = DataSource.Config.product_Id;
            DataSource._productArray[DataSource.Config.productIndex] = new DalProduct(this.product.ID,
                this.product.Name,
                this.product.Price,
                this.product.Category,
                this.product.InStock);
            DataSource.Config.productIndex++;
            Console.WriteLine("product was added to products' list!");
            Console.WriteLine("your product ID is:" + String.Format("{0:000000}",this.product.ID));
            return;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return;
        }
    }

    public string DisplayProduct(int ID)
    {
        for (int i = 0; i < DataSource.Config.productIndex; i++)
        {
            if (DataSource._productArray[i].product.ID==ID)
            {
                return DataSource._productArray[i].product.ToString();
            }
        }
        return "product was not found!\n";
    }

    public void DeleteProduct(int iD)
    {
        try
        {
            if (DataSource.Config.productIndex==0)
            {
                throw new Exception("product array is empty!");
            }
            bool flag = true;//flag is true until we delete the product.
            for (int i = 0; i < DataSource.Config.productIndex; i++)
            {
                if (!flag)
                {
                    break;
                }
                if (DataSource._productArray[i].product.ID == iD)//product was found
                {
                    //start rearenging array.
                    for (int j = i; j < DataSource.Config.productIndex + 1; j++)
                    {
                        if (j < DataSource.Config.productIndex)
                        {
                            DataSource._productArray[j] = DataSource._productArray[j + 1];
                        }
                    }
                    flag = false;
                    DataSource.Config.productIndex--;
                }
            }
            if (flag)
                Console.WriteLine("product was not found.\n");
            else
                Console.WriteLine("product was deleted.");
            return;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        
    }

    public void UpdateProduct(int _ID, string _Name, double _Price, Enums.Category _Category, int _InStock)
    {
        for (int i = 0; i < DataSource.Config.productIndex; i++)//first find product.
        {
            if (DataSource._productArray[i].product.ID == _ID)//product was found
            {
                //start updateting product.
                DataSource._productArray[i].product.Name=_Name;
                DataSource._productArray[i].product.Price=_Price;
                DataSource._productArray[i].product.Category=_Category;
                DataSource._productArray[i].product.InStock=_InStock;
                Console.WriteLine("product was updated."); //letting user know update was successful.
                break;
            }
        }
        Console.WriteLine("product was not found.\n");//we didnt find the product id on list.
    }
   
    public DalProduct[] PrintProducts()
    {
        return DataSource._productArray;
    }
       
    #endregion
      
        
               
        

}
