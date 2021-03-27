using Sims4_100BabyTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sims4_100BabyTracker.Controllers
{
    public class HomeController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        public ActionResult Summary()
        {
            Matriarch currentMom = db.Matriarches.OrderByDescending(x => x.MatriarchId).First();
            int babies = 0, bladder = 0, energy = 0;
            int currentDaddies = db.Children.Where(x => x.MotherId == currentMom.MatriarchId).GroupBy(x => x.Father).Select(x => x.FirstOrDefault()).Count();
            int allDaddies = db.Children.GroupBy(x => x.Father).Select(x => x.FirstOrDefault()).ToList().Count();
            int matriarchs = db.Matriarches.ToList().Count;

            foreach (Matriarch matriarch in db.Matriarches.ToList())
            {
                babies += matriarch.Children.Count;
                bladder += matriarch.BladderFailures;
                energy += matriarch.EnergyFailures;
            }

            ViewBag.Babies = babies;
            ViewBag.CurrentMom = currentMom;
            ViewBag.CurrentDaddies = currentDaddies;
            ViewBag.AllDaddies = allDaddies;
            ViewBag.Mommies = matriarchs;
            ViewBag.Bladder = bladder;
            ViewBag.Energy = energy;

            return View();
        }
    }
}