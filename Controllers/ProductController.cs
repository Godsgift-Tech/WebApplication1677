using Microsoft.AspNetCore.Mvc;
using WebApplication1677.Models;

namespace WebApplication1677.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            List<ProductModel> products = new List<ProductModel>
            {
                new ProductModel
                {
                 Name = "Sagem mobile",
                 Description = "1 gig ram",
                 Amount = 123.6222m,
                 Color ="black"
                },

                 new ProductModel
                {
                 Name = "Samsung",
                 Description = "5gig ram",
                 Amount = 3145.7222m,
                 Color ="yellow"
                },
                  new ProductModel
                {
                 Name = "Tecno mobile",
                 Description = "2gig ram",
                 Amount = 334.6222m,
                 Color ="green"
                },
            };


            return View(products);
        }
    }
}
