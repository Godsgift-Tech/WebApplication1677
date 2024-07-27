
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var productmodellist = products.Select(x => new ProductModel
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

        public async Task<IActionResult> Add(ProductModel productModel)
        {
            try
            {
                // Create a list to store the file paths associated with the product
                List<string> imagePaths = new List<string>();

                // Handle the uploaded images
                if (productModel.Images != null && productModel.Images.Count > 0)
                {
                    foreach (var imageFile in productModel.Images)
                    {
                        if (imageFile != null && imageFile.Length > 0)
                        {
                            // Save the image to a location of your choice, e.g., a folder on the server
                            // You can generate a unique file name to avoid overwriting existing images
                            var fileName = Guid.NewGuid() + Path.GetExtension(imageFile.FileName);
                            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);

                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await imageFile.CopyToAsync(stream);
                            }

                            // Store the file path or other information in your database for reference
                            // You can associate the file with the product being added
                            // Store the file path in the list
                            imagePaths.Add("images/" + fileName);
                        }
                    }
                }
                var productEntity = new Product
                {
                    Name = productModel.Name,
                    Description = productModel.Description,
                    Amount = productModel.Amount,
                    Color = productModel.Color,

                };
                _db.Products.Add(productEntity);
                _db.SaveChanges();
                // return RedirectToAction("Index");
                // Associate the uploaded image file paths with the product
                if (imagePaths.Count > 0)
                {
                    foreach (var imagePath in imagePaths)
                    {
                        var imageEntity = new ProductImage
                        {
                            ProductId = productEntity.Id,
                            ImagePath = imagePath
                        };

                        _db.ProductImages.Add(imageEntity);
                    }

                    await _db.SaveChangesAsync();
                }


                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                throw;
            }
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            // look for product with this id
            var productEntity = _db.Products.Find(id);
            // if this product was not found with that id return not found
            if (productEntity == null)
            {
                return View("Error" + "This is an error message");
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
            //save the new update
            _db.SaveChanges();
            return RedirectToAction("Index");


        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var productEntity = _db.Products.Find(id);
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
