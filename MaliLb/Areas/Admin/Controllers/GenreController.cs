﻿using MaliLb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MaliLb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Roles.Admin)]
    public class GenreController : Controller
    {
        public readonly ApplicationDbContext _db;
        public GenreController(ApplicationDbContext db) => _db = db;

        public IActionResult Index(string sortOrder, string searchString)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            var genre = _db.Genre.AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                genre = genre.Where(g => g.Name.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    genre = genre.OrderByDescending(g => g.Name);
                    break;
                default:
                    genre = genre.OrderBy(g => g.Name);
                    break;
            }
            return View(genre.ToList());
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Genre genre)
        {
            if (ModelState.IsValid)
            {
                _db.Genre.Add(genre);
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

            Genre? genreFromDb = _db.Genre.Find(id);
            if (genreFromDb == null)
            {
                return NotFound();
            }
            return View(genreFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Genre genre)
        {
            if (ModelState.IsValid)
            {
                _db.Genre.Update(genre);
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

            Genre? genreFromDb = _db.Genre.Find(id);
            if (genreFromDb == null)
            {
                return NotFound();
            }
            return View(genreFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Genre? genreFromDb = _db.Genre.Find(id);
            if (genreFromDb == null)
            {
                return NotFound();
            }
            _db.Genre.Remove(genreFromDb);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
