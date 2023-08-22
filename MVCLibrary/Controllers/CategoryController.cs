using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCLibrary.Models.Entities;

namespace MVCLibrary.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        DbLibraryEntities dbLibraryEntities;
        Category category;
        public CategoryController()
        {
            dbLibraryEntities = new DbLibraryEntities();
        }
        public ActionResult Index()
        {
            var categories = dbLibraryEntities.Category.ToList();
            return View(categories);
        }
        [HttpGet] // sayfa yüklendiğinde çalıştır.
        public ActionResult CategoryAdd()
        {
            return View();
        }
        [HttpPost] // sayfada post işlemi yapınca çalıştır.
        public ActionResult CategoryAdd(Category category)
        {
            dbLibraryEntities.Category.Add(category);
            dbLibraryEntities.SaveChanges();
            return View();
        }
        public ActionResult CategoryDelete(int categoryId)
        {
            category = dbLibraryEntities.Category.Find(categoryId);
            dbLibraryEntities.Category.Remove(category);
            dbLibraryEntities.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult CategoryGet(int categoryId)
        {
            category = dbLibraryEntities.Category.Find(categoryId);
            return View("CategoryGet", category);
        }
        public ActionResult CategoryUpdate(Category getCategory)
        {
            category = dbLibraryEntities.Category.Find(getCategory.categoryId);
            category.categoryName = getCategory.categoryName;
            dbLibraryEntities.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}