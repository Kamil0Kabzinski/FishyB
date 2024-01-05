using FishyBuisness_3.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

public class CartController : Controller
{
    public IActionResult AddToCart(int productId, string productName, int quantity, decimal price)
    {
        Cart cart;
        var cartJson = HttpContext.Request.Cookies["Cart"];

        if (cartJson != null)
        {
            cart = JsonConvert.DeserializeObject<Cart>(cartJson);
        }
        else
        {
            cart = new Cart();
        }

        var existingItem = cart.Items.FirstOrDefault(item => item.ProductId == productId);

        if (existingItem != null)
        {
            existingItem.Quantity += quantity;
        }
        else
        {
            var newItem = new CartItem
            {
                ProductId = productId,
                ProductName = productName,
                Quantity = quantity,
                Price = price
            };

            cart.Items.Add(newItem);
        }

        // Zapis koszyka do ciasteczka
        var cartJsonString = JsonConvert.SerializeObject(cart);
        HttpContext.Response.Cookies.Append("Cart", cartJsonString);

        return RedirectToAction("Index", "Home"); // Przekierowanie gdziekolwiek chcesz
    }

    public IActionResult ViewCart()
    {
        var cartJson = HttpContext.Request.Cookies["Cart"];
        var cart = JsonConvert.DeserializeObject<Cart>(cartJson);

        // Możesz teraz pracować z koszykiem (np. wyświetlić zawartość w widoku)

        return View(cart);
    }
}