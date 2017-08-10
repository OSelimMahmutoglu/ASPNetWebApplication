using North.Models;
using North.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace North.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun
        public ActionResult Duzenle(int? id)
        {
            if (id == null)
                return RedirectToAction("Index", "Home");
            var db = new northwindEntities();
            var urun = db.Products.Find(id.Value);

            if (urun == null)
                return RedirectToAction("Index", "Home");

            
            var kategoriler = new List<SelectListItem>();
            db.Categories.ToList().ForEach(x =>
            kategoriler.Add(new SelectListItem()
                 {
                Text = x.CategoryName,
                Value = x.CategoryID.ToString()
                 })
            );
            ViewBag.Kategoriler = kategoriler;
            return View(urun);
        }

        [HttpPost]
        public ActionResult Duzenle(Products model)
        {
            if(!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Hatalı bir işlemi başarısız");
                return View(model);
            }

            var db = new northwindEntities();
            var urun = db.Products.Find(model.ProductID);
            if (urun == null)
                return RedirectToAction("Index", "Home");

            try
            {
                urun.ProductName = model.ProductName;
                urun.UnitPrice = model.UnitPrice;
                urun.UnitsInStock = model.UnitsInStock;
                urun.Discontinued = model.Discontinued;
                urun.CategoryID = model.CategoryID;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Güncelle işlemi başarısız");
                return View(model);
            }
            //return View(model); 
            return RedirectToAction("Duzenle", new { id = urun.ProductID });
                
        }

        [HttpGet]
        public ActionResult Yeni()
        {
            var db = new northwindEntities();
            var kategoriler = new List<SelectListItem>();
            db.Categories.ToList().ForEach(x =>
            kategoriler.Add(new SelectListItem()
            {
                Text = x.CategoryName,
                Value = x.CategoryID.ToString()
            })
            );
            ViewBag.Kategoriler = kategoriler;
            return View();
        }

        [HttpPost]
        public ActionResult Yeni(Products model)
        {
            var urun = new Products()
            {
                ProductName = model.ProductName,
                UnitPrice = model.UnitPrice,
                Discontinued = model.Discontinued,
                CategoryID = model.CategoryID,
                UnitsInStock = model.UnitsInStock
            };

            try
            {
                var db = new northwindEntities();
                db.Products.Add(urun);
                db.SaveChanges();
               
            }
            catch (Exception ex)
            {

                throw;
            }
            return RedirectToAction("Duzenle", new { id = urun.ProductID });
        }

        public ActionResult Ara()
        {
            return View();
        }
        public JsonResult Bul(string key)
        {
            var db = new northwindEntities();
            var sonuc = db.Products.Where(x => x.ProductName.ToLower().Contains(key.ToLower()) || x.Categories.CategoryName.ToLower().Contains(key.ToLower())).Select(x => new ProductViewModel()
            {
                ProductsID = x.ProductID,
                ProductName = x.ProductName,
                UnitPrice = x.UnitPrice,
                Discontinued = x.Discontinued,
                CategoryName = x.Categories.CategoryName,
                UnitsInStock = x.UnitsInStock,
                CategoryId = x.CategoryID
            }).ToList();
            return Json(sonuc, JsonRequestBehavior.AllowGet);
        }
    }
}