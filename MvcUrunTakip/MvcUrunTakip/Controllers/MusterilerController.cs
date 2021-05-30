using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcUrunTakip.Models.Entities;
using PagedList;
using PagedList.Mvc;
namespace MvcUrunTakip.Controllers

{
    public class MusterilerController : Controller
    {
        DbMvcStokEntities2 db = new DbMvcStokEntities2();
        // GET: Musteriler
        public ActionResult MusteriListele(int page = 1)
        {
            //var musteriListe = db.tblMusteris.ToList();
            var musteriListe = db.tblMusteris.Where(x => x.Musteri_Durum == true).ToList().ToPagedList(page, 7);
            return View(musteriListe);
        }
        [HttpGet]
        public ActionResult MusteriEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult MusteriEkle(tblMusteri m)
        {
            if (!ModelState.IsValid)
            {
                return View("MusteriEkle");
            }
            m.Musteri_Durum = true;
            db.tblMusteris.Add(m);
            db.SaveChanges();
            return RedirectToAction("MusteriListele");
        }
        [HttpGet]
        public ActionResult MusteriSil(int id)
        {
            var musteris = db.tblMusteris.Find(id);
            musteris.Musteri_Durum = false;
            db.SaveChanges();
            return RedirectToAction("MusteriListele");
        }
        [HttpGet]
        public ActionResult MusteriGuncelle(int id)
        {
            var mus = db.tblMusteris.Find(id);
            return View(mus);
        }

        [HttpPost]
        public ActionResult MusteriGuncelle(tblMusteri m)
        {
            var musteri = db.tblMusteris.FirstOrDefault(x => x.Musteri_Id == m.Musteri_Id);
            musteri.Musteri_Ad = m.Musteri_Ad;
            musteri.Musteri_Soyad = m.Musteri_Soyad;
            musteri.Musteri_Sehir = m.Musteri_Sehir;
            musteri.Musteri_Bakiye = m.Musteri_Bakiye;
            db.SaveChanges();
            return RedirectToAction("MusteriListele");
        }
    }
}