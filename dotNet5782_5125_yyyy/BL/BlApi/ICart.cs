using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;

public interface ICart
{
    /// <summary>
    /// function used to add a product to a specific cart.
    /// </summary>
    /// <param name="cart">to add the product to</param>
    /// <param name="ID">product ID to add to the cart</param>
    /// <returns></returns>
    public Cart Add(Cart cart, int ID);
    /// <summary>
    /// to update a product in the cart.
    /// you can add, subtruct or set its amount to 0.
    /// </summary>
    /// <param name="cart">that we want to update the product on.</param>
    /// <param name="ID">of the product that we want to upadte</param>
    /// <param name="amount">the new amount of the product in cart.</param>
    /// <returns></returns>
    public Cart Update(Cart cart, int ID, int amount);
    /// <summary>
    /// function used to buy the customer cart, and place the order.
    /// </summary>
    /// <param name="cart">that we want to buy.</param>
    /// <param name="name">of customer</param>
    /// <param name="email">of customer</param>
    /// <param name="adress">of customer.</param>
    /// <returns></returns>
    public Cart Buy(Cart cart,string name,string email,string adress);
}
