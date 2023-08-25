using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCLibrary.Models.Entities;

namespace MVCLibrary.Controllers
{
    public class BookController : Controller
    {
        DbLibraryEntities dbLibraryEntities;
        public BookController()
        {
            dbLibraryEntities = new DbLibraryEntities();
        }
        // GET: Book
        public ActionResult Index()
        {
            var books = dbLibraryEntities.Book.ToList();
            return View(books);
        }
        [HttpGet]
        public ActionResult BookAdd()
        {
            ViewBag._categoryList = GetSelectListItems<Category>(dbLibraryEntities.Category.ToList(),
                category => category.categoryName,
                category => category.categoryId.ToString());
            ViewBag._authorList = GetSelectListItems<Author>(dbLibraryEntities.Author.ToList(),
                author => author.firstName,
                author => author.authorId.ToString());
            return View();
        }
        [HttpPost]
        public ActionResult BookAdd(Book book)
        {

            return View();
        }
        public List<SelectListItem> GetSelectListItems<TEntity>(List<TEntity> entities, Func<TEntity, string> textSelector, Func<TEntity, string> valueSelector)
        {
            return entities.Select(entity => new SelectListItem
            {
                Text = textSelector(entity),
                Value = valueSelector(entity)
            }).ToList();
        }
    }
}