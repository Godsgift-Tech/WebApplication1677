using Microsoft.AspNetCore.Mvc;
using WebApplication1677.Data;
using WebApplication1677.Entities;
using WebApplication1677.Models;
    
namespace WebApplication1677.Controllers
{

    public class ProductController : Controller
    {
        MyDbContext _db;
       

        public ProductController(MyDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            
            var products = _db.Products.ToList();
            var productmodellist= products.Select(x=> new ProductModel
            {
                
                Name = x.Name,
                Description = x.Description,
                Amount = x.Amount,
                Color = x.Color,

            }).ToList();

            return View(productmodellist);
            

            }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]

        public IActionResult Add(ProductModel productModel)
        {
            var productEntity = new Product { 
             Name = productModel.Name,
             Description = productModel.Description,
             Amount = productModel.Amount,
             Color = productModel.Color,
            
            };
            _db.Products.Add(productEntity);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View();
        }
        
    }
}
