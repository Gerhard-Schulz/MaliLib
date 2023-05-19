using MaliLb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace MaliLb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Roles.Admin)]
    public class BookController : Controller
    {
        public readonly ApplicationDbContext _db;
        public BookController(ApplicationDbContext db) => _db = db;

        public IActionResult Index()
        {
            List<Book> bookList = _db.Book.Include(u => u.Work).Include(u => u.Edition).Include(u => u.Visitor).ToList();
            return View(bookList);
        }

        public IActionResult Details(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Book? bookFromDb = _db.Book.Include(u => u.Work).Include(u => u.Edition).Include(u => u.Visitor).FirstOrDefault(u => u.ID == id);
            if (bookFromDb == null)
            {
                return NotFound();
            }
            return View(bookFromDb);
        }

        public IActionResult Add()
        {
            IEnumerable<SelectListItem> WorkList = _db.Work.ToList().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.ID.ToString()
            });
            ViewBag.WorkList = WorkList;

            IEnumerable<SelectListItem> EditionList = _db.Edition.ToList().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.ID.ToString()
            });
            ViewBag.EditionList = EditionList;

            return View();
        }

        [HttpPost]
        public IActionResult Add(Book book)
        {
            if (ModelState.IsValid)
            {
                _db.Book.Add(book);
                _db.SaveChanges();
                return Redirect($"/Admin/Book/Details/{book.ID}");
            }
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Book? bookFromDb = _db.Book.Find(id);
            if (bookFromDb == null)
            {
                return NotFound();
            }

            IEnumerable<SelectListItem> WorkList = _db.Work.ToList().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.ID.ToString()
            });
            ViewBag.WorkList = WorkList;

            IEnumerable<SelectListItem> EditionList = _db.Edition.ToList().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.ID.ToString()
            });
            ViewBag.EditionList = EditionList;

            IEnumerable<SelectListItem> ReaderList = _db.Visitor.ToList().Select(u => new SelectListItem
            {
                Text = u.СardNumber.ToString(),
                Value = u.ID.ToString()
            });
            ViewBag.ReaderList = ReaderList;

            return View(bookFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Book book)
        {
            if (ModelState.IsValid)
            {
                _db.Book.Update(book);
                _db.SaveChanges();
                return Redirect($"/Admin/Book/Details/{book.ID}");
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Book? bookFromDb = _db.Book.Find(id);
            if (bookFromDb == null)
            {
                return NotFound();
            }
            return View(bookFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Book? bookFromDb = _db.Book.Find(id);
            if (bookFromDb == null)
            {
                return NotFound();
            }
            _db.Book.Remove(bookFromDb);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
