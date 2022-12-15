using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BO;
using DalApi;

namespace BlImplementation;

internal class BOCart : ICart
{
    //fields
    private IDal? dal=DalApi.Factory.Get();
    #region ICart implementation
    /// <summary>
    /// function used to add a product to a specific cart.
    /// </summary>
    /// <param name="cart">to add the product to</param>
    /// <param name="ID">product ID to add to the cart</param>
    /// <returns></returns>
    public BO.Cart Add(BO.Cart cart, int ID)
    {
        try
        {
            // product is already in cart.
            int i = ProductOnCart(cart, ID);
            if (i!=-1)
            {
                cart.Items[i].Amount++;
                UpdateTotal(cart);
                return cart;

            }
            //product is NOT in this cart.
            DO.Product product=dal.Product.Get(ID); //if product exist. (exception thrown if not.
            if (product.InStock==0)
            {
                throw new BO.OutOfStockEx(); //catch in main.
            }
            cart.Items.Add(new BO.OrderItem()
            {
                ID = product.ID,
                ProductName = product.Name,
                Amount = 1,
                Price = product.Price,
                ProductId = product.ID,
                OrderId = -1    
            });
            UpdateTotal(cart);
            return cart;
        }
        catch (DalApi.ObjectNotFoundEx e)
        {
            throw new BO.ObjectNotFoundEx();
        }
    }
    /// <summary>
    /// function used to buy the customer cart, and place the order.
    /// </summary>
    /// <param name="cart">that we want to buy.</param>
    /// <param name="name">of customer</param>
    /// <param name="email">of customer</param>
    /// <param name="adress">of customer.</param>
    /// <returns></returns>
    public BO.Cart Buy(BO.Cart cart, string name, string email, string adress)
    {
        try
        {
            //first we check currection of details.
            if (name == "" || email == "" || adress == "" || !IsValidEmail(email))
            {
                throw new BO.CustomerDetailsEx();//catch in main.
            }
            if (cart.Items.Count==0)
            {
                throw new BO.EmptyListEx();
            }
            foreach (OrderItem item in cart.Items)
            {
                if (item.Amount <= 0)
                {
                    throw new BO.GeneralEx("Amount was negetive in this cart!.");
                }
                if (!Sufficientstock(item.ID, item.Amount))
                {
                    throw new BO.GeneralEx("insufficient stock in item ID: " + String.Format("{0:000000}", item.ID));
                }
                if (!ProductExcist(item.ID))
                {
                    throw new BO.ObjectNotFoundEx();
                }
            }
            DO.Order order = new DO.Order() { CustomerName=name,
                CustomerAdress=adress,
                CustomerEmail=email,
                OrderDate = DateTime.Now };
            int ID = dal.Order.Add(order);
            foreach (OrderItem item in cart.Items)
            {
                DO.OrderItem orderItem = new DO.OrderItem()
                {
                    Amount = item.Amount,
                    OrderId = ID,
                    Price = item.Price,
                    ProductId = item.ProductId
                };
                dal.OrderItem.Add(orderItem);
            }
            return cart;
        }
        catch (BO.GeneralEx e)
        {
            throw e;
        }
        catch(BO.ObjectNotFoundEx e)
        {
            throw e;
        }
        catch(DalApi.OutOfRangeEx e)
        {
            throw new BO.GeneralEx(e.message); 
        }
        catch(DalApi.EmptyListEx e)
        {
            throw new BO.EmptyListEx();
        }
    }
    /// <summary>
    /// to update a product in the cart.
    /// you can add, subtruct or set its amount to 0.
    /// </summary>
    /// <param name="cart">that we want to update the product on.</param>
    /// <param name="ID">of the product that we want to upadte</param>
    /// <param name="amount">the new amount of the product in cart.</param>
    /// <returns></returns>
    public BO.Cart Update(BO.Cart cart, int ID, int amount)
    {
        try
        {
            int i = ProductOnCart(cart, ID);
            if (i == -1) //item was not found.
            {
                throw new BO.ObjectNotFoundEx(); //product is not on cart! catch in main.
            }
            if (amount == 0)
            {
                cart.Items.RemoveAt(i);
                UpdateTotal(cart);
                return cart;
            }
            if (amount > cart.Items[i].Amount) // amount is greater then what is in cart.
            {
                DO.Product product = dal.Product.Get(ID); //possiable exception.
                if (product.InStock < amount) //not enough items in stock.
                {
                    BO.InsufficientStockEx e = new InsufficientStockEx();
                    throw new BO.InsufficientStockEx(); //catch in main.
                }
                //there is enough in stock
                cart.Items[i].Amount = amount;
                UpdateTotal(cart);
                return cart;
            }
            if (amount < cart.Items[i].Amount)
            {
                cart.Items[i].Amount = amount;
                UpdateTotal(cart);
                return cart;
            }
            throw new Exception("someting went wrong.");
        }
        catch (DalApi.ObjectNotFoundEx e)
        {
            throw new BO.ObjectNotFoundEx();
        }
        catch(BO.InsufficientStockEx e)
        {
            throw e;
        }
    }
    #endregion
    #region assitent functions
    private int ProductOnCart(BO.Cart cart,int ID)
    {
        for (int i = 0; i < cart.Items.Count; i++)
        {
            if (cart.Items[i].ID == ID)
                return i;
        }
        return -1;
    }
    private void UpdateTotal(Cart cart)
    {
        double total= 0;
        foreach (OrderItem item in cart.Items)
        {
            total += item.Price * item.Amount;
        }
        cart.Total = total;
    }
    bool IsValidEmail(string email)
    {
        var trimmedEmail = email.Trim();

        if (trimmedEmail.EndsWith("."))
        {
            return false; 
        }
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == trimmedEmail;
        }
        catch
        {
            return false;
        }
    }
    bool Sufficientstock(int ID, int amount)
    {
        DO.Product product=dal.Product.Get(ID);
        return product.InStock >= amount;
    }
    bool ProductExcist(int ID)
    {
        try
        {
            dal.Product.Get(ID);
            return true;
        }
        catch (DalApi.ObjectNotFoundEx e)
        {
            return false;
        }
    }
    #endregion
}
