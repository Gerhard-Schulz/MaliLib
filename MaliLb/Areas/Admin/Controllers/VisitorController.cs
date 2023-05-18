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

        public IActionResult Index()
        {
            List<Visitor> visitorList = _db.Visitor.ToList();
            return View(visitorList);
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
