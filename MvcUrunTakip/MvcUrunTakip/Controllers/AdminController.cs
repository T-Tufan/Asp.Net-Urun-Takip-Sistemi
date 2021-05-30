using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcUrunTakip.Models.Entities;
namespace MvcUrunTakip.Controllers
{
    public class AdminController : Controller
    {
        DbMvcStokEntities2 db = new DbMvcStokEntities2();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult YeniAdmin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniAdmin(tblAdmin a)
        {
            db.tblAdmins.Add(a);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}