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

        public IActionResult Index()
        {
            List<Work> workList = _db.Work.Include(u => u.Author).Include(u => u.Genre).ToList();
            return View(workList);
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
