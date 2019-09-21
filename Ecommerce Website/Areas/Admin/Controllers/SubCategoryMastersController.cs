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
    public class SubCategoryMastersController : Controller
    {
        private DBProjectEntities db = new DBProjectEntities();

        // GET: Admin/SubCategoryMasters
        public ActionResult Index()
        {
            var subCategoryMasters = db.SubCategoryMasters.Include(s => s.CategoryMaster);
            return View(subCategoryMasters.ToList());
        }

        // GET: Admin/SubCategoryMasters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Error", "Dashboard", new { err = "Id is not available!" });
            }
            SubCategoryMaster subCategoryMaster = db.SubCategoryMasters.Find(id);
            if (subCategoryMaster == null)
            {
                return RedirectToAction("Error", "Dashboard", new { err = "Data id not found" });
            }
            return View(subCategoryMaster);
        }

        // GET: Admin/SubCategoryMasters/Create
        public ActionResult Create()
        {
            ViewBag.refCategoryId = new SelectList(db.CategoryMasters, "CategoryId", "CategoryName");
            return View();
        }

        // POST: Admin/SubCategoryMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SubCategoryId,SubCategoryName,refCategoryId")] SubCategoryMaster subCategoryMaster)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.SubCategoryMasters.Add(subCategoryMaster);
                    db.SaveChanges();
                    TempData["msg"] = "SubCategory Details Saved!!";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    TempData["err"] = ex.Message;
                }

            }

            ViewBag.refCategoryId = new SelectList(db.CategoryMasters, "CategoryId", "CategoryName", subCategoryMaster.refCategoryId);
            return View(subCategoryMaster);
        }

        // GET: Admin/SubCategoryMasters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Error", "Dashboard", new { err = "SubCategory Id is Not Available!" });
            }
            SubCategoryMaster subCategoryMaster = db.SubCategoryMasters.Find(id);
            if (subCategoryMaster == null)
            {
                return RedirectToAction("Error", "Dashboard", new { err = "SubCategory Details is not Found!" });
            }
            ViewBag.refCategoryId = new SelectList(db.CategoryMasters, "CategoryId", "CategoryName", subCategoryMaster.refCategoryId);
            return View(subCategoryMaster);
        }

        // POST: Admin/SubCategoryMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SubCategoryId,SubCategoryName,refCategoryId")] SubCategoryMaster subCategoryMaster)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(subCategoryMaster).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["msg"] = "SubCategory Updated!";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    TempData["err"] = ex.Message;
                }

            }
            ViewBag.refCategoryId = new SelectList(db.CategoryMasters, "CategoryId", "CategoryName", subCategoryMaster.refCategoryId);
            return View(subCategoryMaster);
        }

        // GET: Admin/SubCategoryMasters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Error", "Dashboard", new { err = "SubCategory Id is not Available!" });
            }
            SubCategoryMaster subCategoryMaster = db.SubCategoryMasters.Find(id);
            if (subCategoryMaster == null)
            {
                return RedirectToAction("Error", "Dashboard", new { err = "SubCategory Details is not Found!" });
            }
            return View(subCategoryMaster);
        }

        // POST: Admin/SubCategoryMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                SubCategoryMaster subCategoryMaster = db.SubCategoryMasters.Find(id);
                db.SubCategoryMasters.Remove(subCategoryMaster);
                db.SaveChanges();
                TempData["msg"] = "SubCategory Deleted!";
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
