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
    public class WorkController : Controller
    {
        public readonly ApplicationDbContext _db;
        public WorkController(ApplicationDbContext db) => _db = db;

        public IActionResult Index(string sortOrder, string searchString)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["AuthorSortParm"] = sortOrder == "Author" ? "author_desc" : "Author";
            ViewData["GenreSortParm"] = sortOrder == "Genre" ? "genre_desc" : "Genre";

            var work = _db.Work.Include(u => u.Author).Include(u => u.Genre).AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                work = work.Where(v => v.Name.Contains(searchString) || v.Author.Name.Contains(searchString) || v.Genre.Name.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    work = work.OrderByDescending(v => v.Name);
                    break;
                case "Date":
                    work = work.OrderBy(s => s.DateOfWriting);
                    break;
                case "date_desc":
                    work = work.OrderByDescending(s => s.DateOfWriting);
                    break;
                case "Author":
                    work = work.OrderBy(s => s.Author).ThenBy(s => s.ID);
                    break;
                case "author_desc":
                    work = work.OrderByDescending(s => s.Author).ThenBy(s => s.ID);
                    break;
                case "Genre":
                    work = work.OrderBy(s => s.Genre).ThenBy(s => s.ID);
                    break;
                case "genre_desc":
                    work = work.OrderByDescending(s => s.Genre).ThenBy(s => s.ID);
                    break;
                default:
                    work = work.OrderBy(v => v.Name);
                    break;
            }
            return View(work.ToList());
        }

        public IActionResult Add()
        {
            IEnumerable<SelectListItem> GenreList = _db.Genre.ToList().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.ID.ToString()
            });
            ViewBag.GenreList = GenreList;

            IEnumerable<SelectListItem> AuthorList = _db.Author.ToList().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.ID.ToString()
            });
            ViewBag.AuthorList = AuthorList;

            return View();
        }

        [HttpPost]
        public IActionResult Add(Work work)
        {
            if (ModelState.IsValid)
            {
                _db.Work.Add(work);
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

            Work? workFromDb = _db.Work.Find(id);
            if (workFromDb == null)
            {
                return NotFound();
            }

            IEnumerable<SelectListItem> GenreList = _db.Genre.ToList().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.ID.ToString()
            });
            ViewBag.GenreList = GenreList;

            IEnumerable<SelectListItem> AuthorList = _db.Author.ToList().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.ID.ToString()
            });
            ViewBag.AuthorList = AuthorList;

            return View(workFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Work work)
        {
            if (ModelState.IsValid)
            {
                _db.Work.Update(work);
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

            Work? workFromDb = _db.Work.Find(id);
            if (workFromDb == null)
            {
                return NotFound();
            }
            return View(workFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Work? workFromDb = _db.Work.Find(id);
            if (workFromDb == null)
            {
                return NotFound();
            }
            _db.Work.Remove(workFromDb);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
