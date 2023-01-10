using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;

namespace BlImplementation;

internal class BOProduct : BlApi.IProduct
{
    //fields
    private IDal? dal = DalApi.Factory.Get();
    Func<DO.Product?, BO.ProductForList> Mydelegate;
    #region IProduct implementaion.
    /// <summary>
    /// function add a new product to product list in data layer.
    /// </summary>
    /// <param name="product">BO product witch will be translated to data layer instance.</param>
    /// <exception cref="BO.InvalidParamsEx">in case ID is negetive etc.</exception>
    /// <exception cref="BO.OutOfRangeEx">no more space in product list.</exception>
    public void Add(BO.Product product)
    {
        try
        {
            if (product.Name == "" || product.Price < 0 || product.InStock < 0) //invlaid params in BL object
            {
                throw new BO.InvalidParamsEx(); //will be catched in main function.
            }
            //TODO: name check?
            DO.Product add_product = new DO.Product()
            {
                Name = product.Name,
                Price = product.Price,
                Category = (DO.Enums.Category)product.Category,
                InStock = product.InStock
            };
            dal.Product.Add(add_product);
        }
        catch(DalApi.OutOfRangeEx e) //catches from data layer.
        {
            throw new BO.OutOfRangeEx();
        }
    }
    /// <summary>
    /// function delete a product from data layer.
    /// </summary>
    /// <param name="ID">of item to delete</param>
    /// <exception cref="BO.InvalidParamsEx">ID is invalid (negetive etc)</exception>
    /// <exception cref="BO.ProductInOrderEx">this product is in an order hterfore cannot be deleted.</exception>
    /// <exception cref="BO.ObjectNotFoundEx">there is no product with this ID</exception>
    /// <exception cref="BO.EmptyListEx">store has no proudcts!</exception>
    public void Delete(int ID)
    {
        try
        {
            if (ID < 0) //invalid ID
                throw new BO.InvalidParamsEx();
            if (ProductInOrder(ID)) //product is in an order, cannot be deleted yet.
            {
                throw new BO.ProductInOrderEx();
            }
            dal.Product.Delete(ID);
        }
        catch (DalApi.ObjectNotFoundEx e)
        {
            throw new BO.ObjectNotFoundEx(); //catch and throw it to main function to proceed handaling.
        }
        catch (DalApi.EmptyListEx e)
        {
            throw new BO.EmptyListEx();
        }
        catch(BO.ProductInOrderEx e)
        {
            throw e;
        }
    }
    /// <summary>
    /// returns a list of all products in store to display them.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="BO.EmptyListEx">no prudcuts in store.</exception>
    public IEnumerable<BO.ProductForList?> GetAll(Func<DO.Product?, bool>? select = null)
    {
        try
        {
            IEnumerable<DO.Product?> products = dal.Product.GetAll(select);
            Mydelegate= Convertor;
            return products.Select(Mydelegate).ToList();
            /*List<BO.ProductForList> BOProducts = new List<BO.ProductForList>();
            foreach (DO.Product product in products)
            {
                BOProducts.Add(new BO.ProductForList() { ID=product.ID,
                ProductName=product.Name,
                Price=product.Price,
                Category=(BO.Enums.Category)product.Category});
            }*/
        }
        catch (DalApi.EmptyListEx e)
        {
            throw new BO.EmptyListEx();
        }
    }
    /// <summary>
    /// for manager screen, returns product details fro data layer.
    /// </summary>
    /// <param name="ID">of product.</param>
    /// <returns></returns>
    public BO.Product Get(int ID) //TODO: what is this???
    {
        try
        {
            if (ID<0)
            {
                throw new BO.InvalidParamsEx();
            }
            DO.Product DOproduct = dal.Product.Get(ID);
            BO.Product BOProduct = new BO.Product() { ID= DOproduct.ID,
            Name= DOproduct.Name,
            Price= DOproduct.Price,
            Category=(BO.Enums.Category)DOproduct.Category,
            InStock= DOproduct.InStock
            };
            return BOProduct;
        }
        catch (DalApi.ObjectNotFoundEx e)
        {
            throw new BO.ObjectNotFoundEx();
        }
    }
    /// <summary>
    /// for customer screen, to display specific product details.
    /// </summary>
    /// <param name="ID">of the product</param>
    /// <param name="cart">customers cart.</param>
    /// <returns></returns>
    /// <exception cref="BO.InvalidParamsEx">ID is invalid.(negetive eetc.)</exception>
    /// <exception cref="BO.EmptyListEx">no product in store</exception>
    /// <exception cref="BO.ObjectNotFoundEx">no product with this ID.</exception>
    public BO.ProductItem Get(int ID, BO.Cart cart)
    {
        try
        {
            if (ID<0)
            {
                throw new BO.InvalidParamsEx();
            }
            DO.Product DOProduct=dal.Product.Get(ID);
            BO.ProductItem BOProduct=new BO.ProductItem() { ID= DOProduct.ID,
            Name= DOProduct.Name,
            Price= DOProduct.Price,
            Category= (BO.Enums.Category)DOProduct.Category,
            InStock= DOProduct.InStock==0? false:true, 
            };
            if (cart.Items!=null)
            {
                bool flag = false;
                foreach (BO.OrderItem product in cart.Items)
                {
                    if (product.ID == BOProduct.ID)
                    {
                        BOProduct.Amount = product.Amount;
                        break;
                    }
                }
                BOProduct.Amount=(flag==false)? 0 : BOProduct.Amount; //in case this prduct is NOT on cart, and we want to view it!.
                return BOProduct;
            }
            else
            {
                BOProduct.Amount = 0;
                return BOProduct;
            }
        }
        catch (DalApi.EmptyListEx e)
        {
            throw new BO.EmptyListEx();
        }
        catch (DalApi.ObjectNotFoundEx e)
        {
            throw new BO.ObjectNotFoundEx();
        }
    }
    /// <summary>
    /// to update an excisiting product in data layer.
    /// </summary>
    /// <param name="product">with all new params.</param>
    /// <exception cref="BO.InvalidParamsEx"></exception>
    /// <exception cref="BO.EmptyListEx"></exception>
    /// <exception cref="BO.ObjectNotFoundEx"></exception>
    public void Update(BO.Product product)
    {
        try
        {
            if (product.Name == "" || product.Price < 0 || product.InStock < 0) //invlaid params in BL object
            {
                throw new BO.InvalidParamsEx(); //will be catched in main function.
            }
            DO.Product DOProduct=new DO.Product() { ID=product.ID,
            Name=product.Name,
            Price=product.Price,
            InStock=product.InStock,
            Category=(DO.Enums.Category)product.Category};
            dal.Product.Update(DOProduct);
        }
        catch (DalApi.EmptyListEx e)
        {

            throw new BO.EmptyListEx();
        }
        catch(DalApi.ObjectNotFoundEx e)
        {
            throw new BO.ObjectNotFoundEx();
        }
    }
    #endregion
    #region assistent functions
    /// <summary>
    /// checks to see if this pproduct (with ID), is in any of the excisiting orders.
    /// </summary>
    /// <param name="ID">of the product.</param>
    /// <returns></returns>
    private bool ProductInOrder(int ID)
    {
        IEnumerable<DO.OrderItem?> orderItems=dal.OrderItem.GetAll();
        foreach (DO.OrderItem item in orderItems)
        {
            if (item.ProductId==ID)
            {
                return true; //this product is in at least one order.
            }
        }
        return false;
    }
    /// <summary>
    /// used as convetion function in Select query.
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    private BO.ProductForList Convertor(DO.Product? p)
    {
        BO.ProductForList productForList = new BO.ProductForList()
        {
            ID=p?.ID??0,
            Price=p?.Price ?? 0,
            Category=(BO.Enums.Category)p?.Category,
            ProductName=p?.Name
        };
        return productForList;
    }
    #endregion
}

