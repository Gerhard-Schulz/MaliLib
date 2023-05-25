using MaliLb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MaliLb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Roles.Admin)]
    public class VisitorController : Controller
    {
        public readonly ApplicationDbContext _db;
        public VisitorController(ApplicationDbContext db) => _db = db;

        public IActionResult Index(string sortOrder, string searchString)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["NumSortParm"] = sortOrder == "Num" ? "num_desc" : "Num";
            var visitor = from v in _db.Visitor select v;

            if (!String.IsNullOrEmpty(searchString))
            {
                visitor = visitor.Where(v => v.Name.Contains(searchString) || v.СardNumber.ToString().Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    visitor = visitor.OrderByDescending(v => v.Name);
                    break;
                case "Num":
                    visitor = visitor.OrderBy(s => s.СardNumber);
                    break;
                case "num_desc":
                    visitor = visitor.OrderByDescending(s => s.СardNumber);
                    break;
                default:
                    visitor = visitor.OrderBy(v => v.Name);
                    break;
            }
            return View(visitor);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Visitor visitor)
        {
            if (ModelState.IsValid)
            {
                _db.Visitor.Add(visitor);
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

            Visitor? visitorFromDb = _db.Visitor.Find(id);
            if (visitorFromDb == null)
            {
                return NotFound();
            }
            return View(visitorFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Visitor visitor)
        {
            if (ModelState.IsValid)
            {
                _db.Visitor.Update(visitor);
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

            Visitor? visitorFromDb = _db.Visitor.Find(id);
            if (visitorFromDb == null)
            {
                return NotFound();
            }
            return View(visitorFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Visitor? visitorFromDb = _db.Visitor.Find(id);
            if (visitorFromDb == null)
            {
                return NotFound();
            }
            _db.Visitor.Remove(visitorFromDb);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
