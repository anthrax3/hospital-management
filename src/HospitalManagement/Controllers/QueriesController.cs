using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using HospitalManagement.Models;

namespace HospitalManagement.Controllers
{
    public class QueriesController : Controller
    {
        private ApplicationDbContext _context;

        public QueriesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Queries
        public IActionResult Index()
        {
            return View(_context.Query.ToList());
        }

        // GET: Queries/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Query query = _context.Query.Single(m => m.QueryId == id);
            if (query == null)
            {
                return HttpNotFound();
            }

            return View(query);
        }

        // GET: Queries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Queries/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Query query)
        {
            if (ModelState.IsValid)
            {
                _context.Query.Add(query);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(query);
        }

        // GET: Queries/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Query query = _context.Query.Single(m => m.QueryId == id);
            if (query == null)
            {
                return HttpNotFound();
            }
            return View(query);
        }

        // POST: Queries/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Query query)
        {
            if (ModelState.IsValid)
            {
                _context.Update(query);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(query);
        }

        // GET: Queries/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Query query = _context.Query.Single(m => m.QueryId == id);
            if (query == null)
            {
                return HttpNotFound();
            }

            return View(query);
        }

        // POST: Queries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Query query = _context.Query.Single(m => m.QueryId == id);
            _context.Query.Remove(query);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
