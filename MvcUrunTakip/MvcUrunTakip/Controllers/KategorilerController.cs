using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcUrunTakip.Models.Entities;
namespace MvcUrunTakip.Controllers
{
    public class KategorilerController : Controller
    {
        DbMvcStokEntities2 db = new DbMvcStokEntities2();
        // GET: Kategoriler
        public ActionResult KategoriListele()
        {
            var kategoriler = db.tblKategoris.ToList();
            return View(kategoriler);
        }
        [HttpGet]
        public ActionResult KategoriEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult KategoriEkle(tblKategori ktgr)
        {
            db.tblKategoris.Add(ktgr);
            db.SaveChanges();
            return RedirectToAction("KategoriListele");
        }
        public ActionResult KategoriSil(int id)
        {
            
            var deleted_ktgr = db.tblKategoris.Find(id);
            db.tblKategoris.Remove(deleted_ktgr);
            db.SaveChanges();
            
            return RedirectToAction("KategoriListele");
        }
        public ActionResult KategoriGuncelle(int id)
        {
            var updated_ktgr = db.tblKategoris.Find(id);
            return View("KategoriGuncelle",updated_ktgr);
        }
        [HttpPost]
        public ActionResult KategoriGuncelle(tblKategori ktgr)
        {
            var kategori = db.tblKategoris.FirstOrDefault(x => x.Kategori_Id == ktgr.Kategori_Id);
            kategori.Kategori_Id = ktgr.Kategori_Id;
            kategori.Kategori_Ad = ktgr.Kategori_Ad;
            db.SaveChanges();
            return RedirectToAction("KategoriListele");
        }
    }
}