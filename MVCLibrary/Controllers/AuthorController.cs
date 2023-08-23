using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCLibrary.Models.Entities;

namespace MVCLibrary.Controllers
{
    public class AuthorController : Controller
    {
        DbLibraryEntities dbLibraryEntities;
        Author author;
        public AuthorController()
        {
            dbLibraryEntities = new DbLibraryEntities();
        }
        // GET: Author
        public ActionResult Index()
        {
            var authors = dbLibraryEntities.Author.ToList();
            return View(authors);
        }
        [HttpGet]
        public ActionResult AuthorAdd()
        {
            return View();
        }
        public ActionResult AuthorAdd(Author newAuthor)
        {
            author = dbLibraryEntities.Author.Add(newAuthor);
            dbLibraryEntities.SaveChanges();
            return View();
        }
        public ActionResult AuthorDelete(int id)
        {
            author = dbLibraryEntities.Author.Find(id);
            dbLibraryEntities.Author.Remove(author);
            dbLibraryEntities.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult AuthorGet(int id)
        {
            author = dbLibraryEntities.Author.Find(id);
            return View("AuthorGet", author);
        }
        public ActionResult AuthorUpdate(Author authorUpdate)
        {
            author = dbLibraryEntities.Author.Find(authorUpdate.authorId);
            author.firstName = authorUpdate.firstName;
            author.lasName = authorUpdate.lasName;
            author.detail = authorUpdate.detail;
            dbLibraryEntities.SaveChanges();
            return View("AuthorGet", author);
        }
    }
}