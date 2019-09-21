using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ecommerce_Website.Models;

namespace Ecommerce_Website.Areas.Admin.Controllers
{
    public class CategoryMastersController : Controller
    {
        private DBProjectEntities db = new DBProjectEntities();

        // GET: Admin/CategoryMasters
        public ActionResult Index()
        {
            return View(db.CategoryMasters.ToList());
        }

        // GET: Admin/CategoryMasters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Error", "Dashboard", new { err = "Id is not available!" });
            }
            CategoryMaster categoryMaster = db.CategoryMasters.Find(id);
            if (categoryMaster == null)
            {
                return RedirectToAction("Error", "Dashboard", new { err = "Data id not found" });
            }
            return View(categoryMaster);
        }

        // GET: Admin/CategoryMasters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/CategoryMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryId,CategoryName")] CategoryMaster categoryMaster)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.CategoryMasters.Add(categoryMaster);
                    db.SaveChanges();
                    TempData["msg"] = "Category Details Saved!!";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    TempData["err"] = ex.Message;
                }

            }

            return View(categoryMaster);
        }

        // GET: Admin/CategoryMasters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Error", "Dashboard", new { err = "Id is not Avalible!!" });
            }
            CategoryMaster categoryMaster = db.CategoryMasters.Find(id);
            if (categoryMaster == null)
            {
                return RedirectToAction("Error", "Dashboard", new { err = "Category Details id not found" });
            }
            return View(categoryMaster);
        }

        // POST: Admin/CategoryMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryId,CategoryName")] CategoryMaster categoryMaster)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(categoryMaster).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["msg"] = "Category Updated!!";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    TempData["err"] = ex.Message;
                }
            }
            return View(categoryMaster);
        }

        // GET: Admin/CategoryMasters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Error", "Dashboard", new { err = "Category id not Avalible!!" });
            }
            CategoryMaster categoryMaster = db.CategoryMasters.Find(id);
            if (categoryMaster == null)
            {
                return RedirectToAction("Error", "Dashboard", new { err = "Category Details id not found" });
            }
            return View(categoryMaster);
        }

        // POST: Admin/CategoryMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                CategoryMaster categoryMaster = db.CategoryMasters.Find(id);
                db.CategoryMasters.Remove(categoryMaster);
                db.SaveChanges();
                TempData["msg"] = "Category Deleted!!";
            }
            catch (Exception ex)
            {
                TempData["err"] = ex.Message;
            }

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
