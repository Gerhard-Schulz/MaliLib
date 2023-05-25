using MaliLb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MaliLb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Roles.Admin)]
    public class AuthorController : Controller
    {
        public readonly ApplicationDbContext _db;
        public AuthorController(ApplicationDbContext db) => _db = db;

        public IActionResult Index(string sortOrder, string searchString)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            var author = from a in _db.Author select a;

            if (!String.IsNullOrEmpty(searchString))
            {
                author = author.Where(a => a.Name.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    author = author.OrderByDescending(a => a.Name);
                    break;
                default:
                    author = author.OrderBy(a => a.Name);
                    break;
            }
            return View(author);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Author author)
        {
            if (ModelState.IsValid)
            {
                _db.Author.Add(author);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Author? authorFromDb = _db.Author.Find(id);
            if (authorFromDb == null)
            {
                return NotFound();
            }
            return View(authorFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Author author)
        {
            if (ModelState.IsValid)
            {
                _db.Author.Update(author);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Author? authorFromDb = _db.Author.Find(id);
            if (authorFromDb == null)
            {
                return NotFound();
            }
            return View(authorFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Author? authorFromDb = _db.Author.Find(id);
            if (authorFromDb == null)
            {
                return NotFound();
            }
            _db.Author.Remove(authorFromDb);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
