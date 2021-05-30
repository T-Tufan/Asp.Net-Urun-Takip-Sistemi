using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcUrunTakip.Models.Entities;
namespace MvcUrunTakip.Controllers
{
    public class SatislarController : Controller
    {
        DbMvcStokEntities2 db = new DbMvcStokEntities2();
        // GET: Satislar
        public ActionResult SatisListele()
        {
            var satislar = db.tblSatislars.ToList();
            return View(satislar);
        }
        [HttpGet]
        public ActionResult SatisEkle()
        {
            List<SelectListItem> uruns = ListValues(db, 1);
            List<SelectListItem> personels = ListValues(db, 2);
            List<SelectListItem> musteris = ListValues(db, 3);
            ViewBag.urunler = uruns;
            ViewBag.personeller = personels;
            ViewBag.musteriler = musteris;
            return View();
        }
        [HttpPost]
        public ActionResult SatisEkle(tblSatislar s)
        {
            var urns = db.tblUruns.Where(x => x.Urun_Id == s.tblUrun.Urun_Id).FirstOrDefault();
            var prsnls = db.tblPersonels.Where(x => x.Personel_Id == s.tblPersonel.Personel_Id).FirstOrDefault();
            var mstrs = db.tblMusteris.Where(x => x.Musteri_Id == s.tblMusteri.Musteri_Id).FirstOrDefault();
            s.tblUrun = urns;
            s.tblPersonel = prsnls;
            s.tblMusteri = mstrs;
            s.Satis_Tarih = DateTime.Parse(DateTime.Now.ToLongDateString());
            db.tblSatislars.Add(s);
            db.SaveChanges();
            return RedirectToAction("SatisListele");
        }

        public List<SelectListItem> ListValues(DbMvcStokEntities2 db,int secim)
        {
            List<SelectListItem> values = new List<SelectListItem>();
            if (secim == 1)
            {
                values = (from x in db.tblUruns.ToList()
                          select new SelectListItem
                          {
                           Text = x.Urun_Ad,
                           Value = x.Urun_Id.ToString()
                          }).ToList();
            }
            else if(secim == 2)
            {
                values = (from x in db.tblPersonels.ToList()
                          select new SelectListItem
                          {
                              Text = x.Personel_Ad +" "+ x.Personel_Soyad,
                              Value = x.Personel_Id.ToString()
                          }).ToList();
            }
            else if(secim == 3)
            {
                values = (from x in db.tblMusteris.ToList()
                          select new SelectListItem
                          {
                              Text = x.Musteri_Ad +" "+ x.Musteri_Soyad,
                              Value = x.Musteri_Id.ToString()
                          }).ToList();
            }
            return values;
        }
    }
}