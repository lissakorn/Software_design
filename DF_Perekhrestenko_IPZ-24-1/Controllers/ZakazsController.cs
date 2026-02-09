using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DF_Perekhrestenko_IPZ_24_1.Models;

namespace DF_Perekhrestenko_IPZ_24_1.Controllers
{
    public class ZakazsController : Controller
    {
        private HotelFinalContainer db = new HotelFinalContainer();
        private hotelEntities dbMain = new hotelEntities();
        public ActionResult Index()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var zakazSet = db.ZakazSet.Include(z => z.Staff);
            return View(zakazSet.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zakaz zakaz = db.ZakazSet.Find(id);
            if (zakaz == null)
            {
                return HttpNotFound();
            }
            return View(zakaz);
        }

        public ActionResult Create()
        {
            if (Session["UserName"] == null) return RedirectToAction("Login", "Account");

            var newZakaz = new Zakaz();
            newZakaz.OrderDate = DateTime.Now;

            ViewBag.RoomNumber = new SelectList(dbMain.bookings.Where(b => b.Status == "Active")
                                                 .Select(b => b.room.Number.ToString()).Distinct().ToList());
            ViewBag.Description = new SelectList(dbMain.services, "Name", "Name");

            return View(newZakaz); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,RoomNumber,Description,OrderDate,IsDone")] Zakaz zakaz)
        {

            if (ModelState.IsValid)
            {
                zakaz.StaffId = null; 
                db.ZakazSet.Add(zakaz);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RoomNumber = new SelectList(dbMain.bookings.Select(b => b.room.Number).Distinct(), zakaz.RoomNumber);
            ViewBag.Description = new SelectList(dbMain.services, "Name", "Name", zakaz.Description);
            return View(zakaz);
        }
        public ActionResult Edit(int? id)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zakaz zakaz = db.ZakazSet.Find(id);
            if (zakaz == null)
            {
                return HttpNotFound();
            }
            ViewBag.StaffId = new SelectList(db.StaffSet, "Id", "FullName", zakaz.StaffId);
            return View(zakaz);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,RoomNumber,Description,OrderDate,IsDone,StaffId")] Zakaz zakaz)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            if (ModelState.IsValid)
            {
                db.Entry(zakaz).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StaffId = new SelectList(db.StaffSet, "Id", "FullName", zakaz.StaffId);
            return View(zakaz);
        }

        public ActionResult Delete(int? id)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zakaz zakaz = db.ZakazSet.Find(id);
            if (zakaz == null)
            {
                return HttpNotFound();
            }
            return View(zakaz);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            Zakaz zakaz = db.ZakazSet.Find(id);
            db.ZakazSet.Remove(zakaz);
            db.SaveChanges();
            return RedirectToAction("Index");
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
