using MaliLb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MaliLb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Roles.Admin)]
    public class EditionController : Controller
    {
        public readonly ApplicationDbContext _db;
        public EditionController(ApplicationDbContext db) => _db = db;

        public IActionResult Index(string sortOrder, string searchString)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            var edition = from e in _db.Edition select e;

            if (!String.IsNullOrEmpty(searchString))
            {
                edition = edition.Where(e => e.Name.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    edition = edition.OrderByDescending(e => e.Name);
                    break;
                default:
                    edition = edition.OrderBy(e => e.Name);
                    break;
            }
            return View(edition);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Edition edition)
        {
            if (ModelState.IsValid)
            {
                _db.Edition.Add(edition);
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

            Edition? editionFromDb = _db.Edition.Find(id);
            if (editionFromDb == null)
            {
                return NotFound();
            }
            return View(editionFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Edition edition)
        {
            if (ModelState.IsValid)
            {
                _db.Edition.Update(edition);
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

            Edition? editionFromDb = _db.Edition.Find(id);
            if (editionFromDb == null)
            {
                return NotFound();
            }
            return View(editionFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Edition? editionFromDb = _db.Edition.Find(id);
            if (editionFromDb == null)
            {
                return NotFound();
            }
            _db.Edition.Remove(editionFromDb);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
