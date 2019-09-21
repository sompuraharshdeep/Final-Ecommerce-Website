using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ecommerce_Website.Models;

namespace Ecommerce_Website.Areas.Admin.Controllers
{
    public class ProductsController : Controller
    {
        private DBProjectEntities db = new DBProjectEntities();

        // GET: Admin/Products
        public ActionResult Index()
        {
            var productMasters = db.ProductMasters.Include(p => p.SubCategoryMaster);
            return View(productMasters.ToList());
        }

        // GET: Admin/Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Error", "Dashboard", new { err = "Id is not available!" });
            }
            ProductMaster productMaster = db.ProductMasters.Find(id);
            if (productMaster == null)
            {
                return RedirectToAction("Error", "Dashboard", new { err = "Data id not found" });
            }
            return View(productMaster);
        }

        // GET: Admin/Products/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.CategoryMasters.ToList(), "CategoryId", "CategoryName");
            ViewBag.refSubCategoryId = new List<SelectListItem>(){
                new SelectListItem(){ Text = "", Value = "Select Sub Category" }};
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,ProductName,Price,ProductImage,Description,Quantity,refSubCategoryId")] ProductMaster productMaster, HttpPostedFileBase ProductImage)
        {

            try
            {
                string dirPath = Server.MapPath("~/Content/Admin/Images/");
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }

                string filename = "Product_" + Guid.NewGuid().ToString().Replace('-', '_') + "." + ProductImage.FileName.Split('.')[1];
                string filepath = dirPath + "\\" + filename;
                ProductImage.SaveAs(filepath);


                ViewBag.message = "Image Uploaded!";
                productMaster.ProductImage = filename;


                if (ModelState.IsValid)
                {
                    db.ProductMasters.Add(productMaster);
                    db.SaveChanges();
                    TempData["msg"] = "Product Details Saved!!";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["err"] = ex.Message;
                ViewBag.error = ex.Message;
            }
            ViewBag.CategoryId = new SelectList(db.CategoryMasters, "CategoryId", "CategoryName");
            ViewBag.refSubCategoryId = new List<SelectListItem>(){
                new SelectListItem(){ Text = "", Value = "Select Sub Category" }};

            //ViewBag.refSubCategoryId = new SelectList(db.SubCategoryMasters, "SubCategoryId", "SubCategoryName", productMaster.refSubCategoryId);
            return View(productMaster);
        }

        public JsonResult GetSubCatByCatId(int CategoryId)
        {
            return Json(db.SubCategoryMasters.Where(a => a.refCategoryId == CategoryId).Select(b => new { b.SubCategoryId, b.SubCategoryName }).ToList(), JsonRequestBehavior.AllowGet);
        }

        // GET: Admin/Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Error", "Dashboard", new { err = "Id is not Avalible!!" });
            }
            ProductMaster productMaster = db.ProductMasters.Find(id);
            if (productMaster == null)
            {
                return RedirectToAction("Error", "Dashboard", new { err = "Product Details id not found" });
            }
            ViewBag.refSubCategoryId = new SelectList(db.SubCategoryMasters, "SubCategoryId", "SubCategoryName", productMaster.refSubCategoryId);
            return View(productMaster);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,ProductName,Price,ProductImage,Description,Quantity,refSubCategoryId")] ProductMaster productMaster)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(productMaster).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["msg"] = "Product Updated!!";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    TempData["err"] = ex.Message;
                }

            }
            ViewBag.refSubCategoryId = new SelectList(db.SubCategoryMasters, "SubCategoryId", "SubCategoryName", productMaster.refSubCategoryId);
            return View(productMaster);
        }

        // GET: Admin/Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Error", "Dashboard", new { err = "Product id not Avalible!!" });
            }
            ProductMaster productMaster = db.ProductMasters.Find(id);
            if (productMaster == null)
            {
                return RedirectToAction("Error", "Dashboard", new { err = "Product Details id not found" });
            }
            return View(productMaster);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                ProductMaster productMaster = db.ProductMasters.Find(id);
                db.ProductMasters.Remove(productMaster);
                db.SaveChanges();
                TempData["msg"] = "Product Deleted!!";
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
