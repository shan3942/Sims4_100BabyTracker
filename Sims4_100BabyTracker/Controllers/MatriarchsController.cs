using PagedList;
using Sims4_100BabyTracker.Models;
using Sims4_100BabyTracker.ViewModels;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Sims4_100BabyTracker.Controllers
{
    public class MatriarchsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: Matriarchs
        public ActionResult Index()
        {
            return View(db.Matriarches.ToList());
        }

        // GET: Matriarchs/Details/5
        public ActionResult Details(int? id, int? daddiesPage, int? childrenPage)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Matriarch matriarch = db.Matriarches.Find(id);
            if (matriarch == null)
            {
                return HttpNotFound();
            }

            List<Child> children = (from child in db.Children
                                    where child.MotherId == matriarch.MatriarchId
                                    group child by child.Father into daddy
                                    select daddy.FirstOrDefault()).ToList();

            List<string> daddies = new List<string>();
            foreach (Child child in children)
            {
                daddies.Add(child.Father);
            }
            ViewBag.DaddyCount = daddies.Count;

            IPagedList<string> daddiesPagedList = daddies.ToPagedList(daddiesPage ?? 1, 5);
            IPagedList<Child> childrenPagedList = (from child in db.Children
                                                    where child.MotherId == matriarch.MatriarchId
                                                    select child).OrderBy(x => x.ChildId).ToPagedList(childrenPage ?? 1, 10);

            ViewBag.DaddyCount = daddies.Count;
            ViewBag.ChildCount = (from child in db.Children
                                  where child.MotherId == matriarch.MatriarchId
                                  select child.Name).ToList().Count;

            MatriarchDetailsVM matriarchVM = new MatriarchDetailsVM
            {
                Mother = matriarch,
                ChildrenList = childrenPagedList,
                DaddiesList = daddiesPagedList,
            };

            return View(matriarchVM);
        }

        // GET: Matriarchs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Matriarchs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MatriarchId,Name,BladderFailures,EnergyFailures")] Matriarch matriarch)
        {
            if (ModelState.IsValid)
            {
                db.Matriarches.Add(matriarch);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(matriarch);
        }

        // POST: Matriarchs/AddBladderFailure
        [HttpPost]
        public ActionResult AddBladderFailure()
        {
            Matriarch matriarch = db.Matriarches.OrderByDescending(x => x.MatriarchId).First();

            matriarch.BladderFailures += 1;
            
            db.Entry(matriarch).State = EntityState.Modified;
            db.SaveChanges();

            return Redirect(Request.UrlReferrer.ToString());
        }

        // POST: Matriarchs/AddEnergyFailure
        [HttpPost]
        public ActionResult AddEnergyFailure()
        {
            Matriarch matriarch = db.Matriarches.OrderByDescending(x => x.MatriarchId).First();

            matriarch.EnergyFailures += 1;

            db.Entry(matriarch).State = EntityState.Modified;
            db.SaveChanges();

            return Redirect(Request.UrlReferrer.ToString());
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
