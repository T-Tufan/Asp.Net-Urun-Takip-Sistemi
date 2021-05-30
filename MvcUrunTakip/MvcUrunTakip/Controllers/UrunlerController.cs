using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcUrunTakip.Models.Entities;
namespace MvcUrunTakip.Controllers
{
    
    public class UrunlerController : Controller
    {
        // GET: Urunler
        DbMvcStokEntities2 db = new DbMvcStokEntities2();
        public ActionResult UrunListele(string aranan)
        {
            //var urunler = db.tblUruns.Where(x => x.Durum == true).ToList();
            var urunler = from x in db.tblUruns select x;
            if (!string.IsNullOrEmpty(aranan))
            {
                urunler = urunler.Where(x => x.Urun_Ad.Contains(aranan));
            }
            return View(urunler.ToList());
        }
        public ActionResult UrunEkle()
        {
            List<SelectListItem> ktg = (from x in db.tblKategoris.ToList()
                                        select new SelectListItem
                                        {
                                            Text = x.Kategori_Ad,
                                            Value = x.Kategori_Id.ToString()
                                        }).ToList();
            ViewBag.DropKtg = ktg;
            return View();
        }
        [HttpPost]
        public ActionResult UrunEkle(tblUrun urn)
        {
            var urunkategori = db.tblKategoris.Where(x => x.Kategori_Id == urn.tblKategori.Kategori_Id).FirstOrDefault();
            urn.tblKategori = urunkategori;
            db.tblUruns.Add(urn);
            db.SaveChanges();
            return RedirectToAction("UrunListele");
        }
        public ActionResult UrunSil(int id)
        {
            /*
            var deleted_urun = db.tblUruns.Find(id);
            db.tblUruns.Remove(deleted_urun);
            db.SaveChanges();
            */
            //İlişkili tablolarda silme işlemi yukarıdaki gibi yapılmaz.Aktif ve pasif işlemleri uygulanır.
            var urunbul = db.tblUruns.Find(id);
            urunbul.Durum = false;
            db.SaveChanges();
            return RedirectToAction("UrunListele");
        }
        [HttpGet]
        public ActionResult UrunGuncelle(int id)
        {
            List<SelectListItem> ktg = (from x in db.tblKategoris.ToList()
                                        select new SelectListItem
                                        {
                                            Text = x.Kategori_Ad,
                                            Value = x.Kategori_Id.ToString()
                                        }).ToList();
            ViewBag.DropKtg = ktg;
            var urun = db.tblUruns.Find(id);
            return View(urun);
        }
        [HttpPost]
        public ActionResult UrunGuncelle(tblUrun urn)
        {
            var urunktg = db.tblKategoris.Where(x => x.Kategori_Id == urn.tblKategori.Kategori_Id).FirstOrDefault();
            var urun = db.tblUruns.FirstOrDefault(x => x.Urun_Id == urn.Urun_Id);
            urun.Urun_Ad = urn.Urun_Ad;
            urun.Urun_Marka = urn.Urun_Marka;
            urun.Urun_Stok = urn.Urun_Stok;
            urun.Urun_AlisFiyat = urn.Urun_AlisFiyat;
            urun.Urun_SatisFiyat = urn.Urun_SatisFiyat;
            urun.tblKategori = urunktg;
            db.SaveChanges();
            return RedirectToAction("UrunListele");
        }
    }
}