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
    public class ProductMastersController : Controller
    {
        private DBProjectEntities db = new DBProjectEntities();

        // GET: Admin/ProductMasters
        public ActionResult Index()
        {
            var productMasters = db.ProductMasters.Include(p => p.SubCategoryMaster);
            return View(productMasters.ToList());
        }

        // GET: Admin/ProductMasters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductMaster productMaster = db.ProductMasters.Find(id);
            if (productMaster == null)
            {
                return HttpNotFound();
            }
            return View(productMaster);
        }

        // GET: Admin/ProductMasters/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.CategoryMasters.ToList(), "CategoryId", "CategoryName");
            ViewBag.RefSubCategoryId = new List<SelectListItem>(){
                new SelectListItem(){ Text = "", Value="Select Sub Category"}};
            return View();
        }

        // POST: Admin/ProductMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,ProductName,Price,ProductImage,Description,Quantity,refSubCategoryId")] ProductMaster productMaster)
        {
            if (ModelState.IsValid)
            {
                db.ProductMasters.Add(productMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.refSubCategoryId = new SelectList(db.SubCategoryMasters, "SubCategoryId", "SubCategoryName", productMaster.refSubCategoryId);
            return View(productMaster);
        }

        public JsonResult GetSubCatByCatId(int CategoryId)
        {
            return Json(db.SubCategoryMasters.Where(a => a.refCategoryId == CategoryId).Select(b => new { b.SubCategoryId, b.SubCategoryName }).ToList(), JsonRequestBehavior.AllowGet);
        }

        // GET: Admin/ProductMasters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductMaster productMaster = db.ProductMasters.Find(id);
            if (productMaster == null)
            {
                return HttpNotFound();
            }
            ViewBag.refSubCategoryId = new SelectList(db.SubCategoryMasters, "SubCategoryId", "SubCategoryName", productMaster.refSubCategoryId);
            return View(productMaster);
        }

        // POST: Admin/ProductMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,ProductName,Price,ProductImage,Description,Quantity,refSubCategoryId")] ProductMaster productMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.refSubCategoryId = new SelectList(db.SubCategoryMasters, "SubCategoryId", "SubCategoryName", productMaster.refSubCategoryId);
            return View(productMaster);
        }

        // GET: Admin/ProductMasters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductMaster productMaster = db.ProductMasters.Find(id);
            if (productMaster == null)
            {
                return HttpNotFound();
            }
            return View(productMaster);
        }

        // POST: Admin/ProductMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductMaster productMaster = db.ProductMasters.Find(id);
            db.ProductMasters.Remove(productMaster);
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
