using FishyBuisness_3.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

public class OrderController : Controller
{
    public IActionResult AddToOrder(int productId, string productName, int quantity, decimal price)
    {
        Order order;
        var orderJson = HttpContext.Request.Cookies["Order"];

        if (orderJson != null)
        {
            order = JsonConvert.DeserializeObject<Order>(orderJson);
        }
        else
        {
            order = new Order();
        }

        var existingItem = order.Items.FirstOrDefault(item => item.ProductId == productId);

        if (existingItem != null)
        {
            existingItem.Quantity += quantity;
        }
        else
        {
            var newItem = new OrderItem
            {
                ProductId = productId,
                ProductName = productName,
                Quantity = quantity,
                Price = price
            };

            order.Items.Add(newItem);
        }

        // Zapis zamówienia do ciasteczka
        var orderJsonString = JsonConvert.SerializeObject(order);
        HttpContext.Response.Cookies.Append("Order", orderJsonString);

        return RedirectToAction("Index", "Home"); // Przekierowanie gdziekolwiek chcesz
    }

    public IActionResult ViewOrder()
    {
        var orderJson = HttpContext.Request.Cookies["Order"];
        var order = JsonConvert.DeserializeObject<Order>(orderJson);

        // Możesz teraz pracować z zamówieniem (np. wyświetlić zawartość w widoku)

        return View(order);
    }
}