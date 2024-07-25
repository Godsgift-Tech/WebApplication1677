using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1677.Data;
using WebApplication1677.Entities;
using WebApplication1677.Models;

namespace WebApplication1677.Controllers
{
    public class CategoryController : Controller
    {
        MyDbContext _DbContext;
        public CategoryController(MyDbContext dbContext)
        {
            _DbContext = dbContext;
        }
        public IActionResult Index()
        {
            var categories = _DbContext.CategoryEntities.ToList();
            var cartegoryModellist = categories.Select(x => new CategoryModel
            {
              Name = x.Name

            }).ToList();

            return View(cartegoryModellist);
        }
        [HttpGet]
        public IActionResult Add()
        {
         return View();
        }
        [HttpPost]
        public IActionResult Add(CategoryModel categoryModel)
        {
            var categoryEntity = new CategoryEntity
            {
                Name = categoryModel.Name,
            };
            _DbContext.CategoryEntities.Add(categoryEntity);
            _DbContext.SaveChanges();
            
            return RedirectToAction("Index");

        }
    }
}
