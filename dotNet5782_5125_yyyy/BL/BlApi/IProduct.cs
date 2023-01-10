using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;

public interface IProduct
{
    /// <summary>
    /// fuction used to retrive a list of products.
    /// </summary>
    /// <returns></returns>
    public IEnumerable<ProductForList?> GetAll(Func<DO.Product?, bool>? select = null);
    /// <summary>
    /// function used to get details of specific product.
    /// for manager screen.
    /// </summary>
    /// <param name="ID">that we want to retrive.</param>
    /// <returns></returns>
    public BO.Product Get(int ID);
    /// <summary>
    /// function used to get detail of specific product.
    /// for customer screen.
    /// </summary>
    /// <param name="ID">product ID that we want to retrive</param>
    /// <param name="cart">of the customer.</param>
    /// <returns></returns>
    public BO.ProductItem Get(int ID,Cart cart);
    /// <summary>that we want to add to store.
    /// for manager screen.
    /// adding a product to the store.
    /// </summary>
    /// <param name="product"></param>
    public void Add(BO.Product product);
    /// <summary>
    /// for manager screen.
    /// to delete a product from store.
    /// must check that prudct is in no order before deleting.
    /// </summary>
    /// <param name="ID"></param>
    public void Delete(int ID);
    /// <summary>
    /// for manager screen.
    /// to update a certin product in store.
    /// using BO product instance.
    /// </summary>
    /// <param name="product">contains all the values to update.</param>
    public void Update(BO.Product product);
}
