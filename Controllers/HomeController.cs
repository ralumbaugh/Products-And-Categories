using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsAndCategories.Models;

namespace ProductsAndCategories.Controllers
{
    public class HomeController : Controller
    {
        private MyContext dbContext;
        public HomeController(MyContext context)
        {
            dbContext = context;
        }

        [HttpGet("")]
        public IActionResult Products()
        {
            Wrapper AllMyProducts = new Wrapper();
            AllMyProducts.AllProducts = dbContext.Products.ToList();
            return View(AllMyProducts);
        }
        public IActionResult Categories()
        {
            Wrapper AllMyCategories = new Wrapper();
            AllMyCategories.AllCategories = dbContext.Categories.ToList();
            return View(AllMyCategories);
        }
        [HttpPost("NewProduct")]
        public IActionResult NewProduct(Wrapper WrappedProduct)
        {
            if(ModelState.IsValid)
            {
                dbContext.Add(WrappedProduct.NewProduct);
                dbContext.SaveChanges();
                return RedirectToAction("Products");
            }
            else
            {
                WrappedProduct.AllProducts = dbContext.Products.ToList();
                return View("Products",WrappedProduct);
            }
        }

        [HttpPost("NewCategory")]
        public IActionResult NewCategory(Wrapper WrappedCategory)
        {
            if(ModelState.IsValid)
            {
                dbContext.Add(WrappedCategory.NewCategory);
                dbContext.SaveChanges();
                return RedirectToAction("Categories");
            }
            else
            {
                WrappedCategory.AllCategories = dbContext.Categories.ToList();
                return View("Categories",WrappedCategory);
            }
        }

        [HttpGet("Products/{ProductId}")]
        public IActionResult IndividualProduct(int ProductId)
        {
            Wrapper MyProduct = new Wrapper();
            MyProduct.NewProduct = dbContext.Products
                .Include(product => product.Associations)
                .ThenInclude(associations => associations.ThisCategory)
                .FirstOrDefault(product => product.ProductId == ProductId);
            MyProduct.AllCategories = dbContext.Categories.ToList();
            return View(MyProduct);
        }
        [HttpGet("Categories/{CategoryId}")]
        public IActionResult IndividualCategory(int CategoryId)
        {
            Wrapper MyCategory = new Wrapper();
            MyCategory.NewCategory = dbContext.Categories
                .Include(category => category.Associations)
                .ThenInclude(associations => associations.ThisProduct)
                .FirstOrDefault(category => category.CategoryId == CategoryId);
            MyCategory.AllProducts = dbContext.Products.ToList();
            return View(MyCategory);
        }

        [HttpPost("AddCategory")]
        public IActionResult AddCategory(Wrapper Wrapped)
        {
            Association NewAssociation = new Association();
            NewAssociation.CategoryId = Wrapped.NewCategory.CategoryId;
            NewAssociation.ProductId = Wrapped.NewProduct.ProductId;
            dbContext.Add(NewAssociation);
            dbContext.SaveChanges();
            return RedirectToAction("IndividualProduct", new { ProductId = Wrapped.NewProduct.ProductId});
        }
        [HttpPost("AddProduct")]
        public IActionResult AddProduct(Wrapper Wrapped)
        {
            Association NewAssociation = new Association();
            NewAssociation.CategoryId = Wrapped.NewCategory.CategoryId;
            NewAssociation.ProductId = Wrapped.NewProduct.ProductId;
            dbContext.Add(NewAssociation);
            dbContext.SaveChanges();
            return RedirectToAction("IndividualCategory", new {CategoryId = Wrapped.NewCategory.CategoryId});
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
