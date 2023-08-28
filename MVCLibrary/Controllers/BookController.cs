using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Antlr.Runtime.Misc;
using MVCLibrary.Models.Entities;

namespace MVCLibrary.Controllers
{
    public class BookController : Controller
    {
        DbLibraryEntities dbLibraryEntities;
        Book book;
        public BookController()
        {
            dbLibraryEntities = new DbLibraryEntities();
        }
        // GET: Book
        public ActionResult Index(string parameters, string action)
        {
            var books = from book in dbLibraryEntities.Book select book;
            if (!string.IsNullOrEmpty(parameters))
            {
                books = books.Where(p => p.bookName.Contains(parameters));
            }
            if (action.Equals("Temizle"))
            {
                books = dbLibraryEntities.Book;
            }
            return View(books.ToList());
        }
        [HttpGet]
        public ActionResult BookAdd()
        {
            ViewBag._authorList = GetSelectListItems<Author>(dbLibraryEntities.Author.ToList(),
                author => author.firstName + ' ' + author.lasName,
                author => author.authorId.ToString());
            ViewBag._categoryList = GetSelectListItems<Category>(dbLibraryEntities.Category.ToList(),
                category => category.categoryName,
                category => category.categoryId.ToString());
            return View();
        }
        [HttpPost]
        public ActionResult BookAdd(Book book)
        {
            var category = dbLibraryEntities.Category.Where(p => p.categoryId == book.categoryId).FirstOrDefault();
            var author = dbLibraryEntities.Author.Where(p => p.authorId == book.authorId).FirstOrDefault();
            book.Category = category;
            book.Author = author;
            dbLibraryEntities.Book.Add(book);
            dbLibraryEntities.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult BookDelete(int id)
        {
            dbLibraryEntities.Book.Remove(dbLibraryEntities.Book.Find(id));
            dbLibraryEntities.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult BookGet(int id)
        {
            book = dbLibraryEntities.Book.Find(id);
            ViewBag._authorList = GetSelectListItems<Author>(dbLibraryEntities.Author.ToList(),
               author => author.firstName + ' ' + author.lasName,
               author => author.authorId.ToString());
            ViewBag._categoryList = GetSelectListItems<Category>(dbLibraryEntities.Category.ToList(),
                category => category.categoryName,
                category => category.categoryId.ToString());
            return View("BookGet", book);
        }

        public ActionResult UpdateBook(Book updateBook)
        {
            book = dbLibraryEntities.Book.Find(updateBook.bookId);
            book.bookName = updateBook.bookName;
            book.Author = dbLibraryEntities.Author.Where(author => author.authorId == updateBook.authorId).FirstOrDefault();
            book.Category = dbLibraryEntities.Category.Where(category => category.categoryId == updateBook.categoryId).FirstOrDefault();
            book.publicationYear = updateBook.publicationYear;
            book.publishingHouse = updateBook.publishingHouse;
            book.pageNumber = updateBook.pageNumber;
            dbLibraryEntities.SaveChanges();
            return RedirectToAction("Index");
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