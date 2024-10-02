using _6DostawsPizza.Models;
using Microsoft.AspNetCore.Mvc;


public class PizzaOrderController : Controller
{
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Register(User user)
    {
        
        if (user.Age < 16)
        {
            ViewData["ErrorMessage"] = "Вам має бути більше 16 років.";
            return View(user);
        }

       
        if (user.ProductCount <= 0)
        {
            ViewData["ErrorMessage"] = "Кількість товарів має бути більше нуля.";
            return View(user);
        }

       
        return RedirectToAction("OrderProducts", new { productCount = user.ProductCount });
    }

    [HttpGet]
    public IActionResult OrderProducts(int productCount)
    {
        ViewBag.ProductCount = productCount;
        return View();
    }

    [HttpPost]
    public IActionResult ConfirmOrder(List<Product> products)
    {
        
        if (products.All(p => string.IsNullOrEmpty(p.Name) || p.Quantity <= 0))
        {
            ModelState.AddModelError("", "Список продуктів не може бути пустим або мати недійсні значення.");
            return View("OrderProducts"); 
        }

        return View("OrderSummary", products);
    }

}
