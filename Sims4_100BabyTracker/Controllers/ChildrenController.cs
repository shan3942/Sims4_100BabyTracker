using PagedList;
using Sims4_100BabyTracker.Models;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace Sims4_100BabyTracker.Controllers
{
    public class ChildrenController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: Children
        public ActionResult Index(int? page)
        {
            IPagedList<Child> children = db.Children.Include(c => c.Mother).OrderBy(x => x.ChildId).ToPagedList(page ?? 1, 10);
            return View(children);
        }

        // GET: Children/Create
        public ActionResult Create()
        {
            ViewBag.MotherId = new SelectList(db.Matriarches, "MatriarchId", "Name");
            return View();
        }

        // POST: Children/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ChildId,Name,Father,MotherId")] Child child)
        {
            if (ModelState.IsValid)
            {
                child.Name += " Hinds";
                db.Children.Add(child);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MotherId = new SelectList(db.Matriarches, "MatriarchId", "Name", child.MotherId);
            return View(child);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}