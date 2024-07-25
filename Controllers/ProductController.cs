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
                Id = x.Id,
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
            // look for product with this id
            var productEntity = _db.Products.Find(id);
            // if this product was not found with that id return not found
            if (productEntity == null)
            {
                return View("Error");
            }

            // copy from product entity to product model
            var productModel = new ProductModel

            {
                Name = productEntity.Name,
                Description = productEntity.Description,
                Amount = productEntity.Amount,
                Color = productEntity.Color,
                Id = productEntity.Id,


            };


            return View(productModel);
        }
        [HttpPost]

        public IActionResult Edit(ProductModel productModel)
        {
            // look for product with this id
            var productEntity = _db.Products.Find(productModel.Id);
            // if this product was not found with that id return not found
            if (productEntity == null)
            {
                return View("Error");
            }

            // lets update our entity
            productEntity.Name = productModel.Name; 
            productEntity.Description = productModel.Description;
            productEntity.Amount = productModel.Amount; 
            productEntity.Color = productModel.Color;
            productEntity.Id = productModel.Id;
            _db.SaveChanges();
            return RedirectToAction("Index");
               
            
            }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var productEntity =_db.Products.Find(id);
            if (productEntity == null)
            {
                return View("Error");
            }
            _db.Products.Remove(productEntity);
            _db.SaveChanges();
            return RedirectToAction("Index");


        }
        }
    }
