using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StokTakip.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace StokTakip.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Kategori

        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult Index(int sayfa=1)
        {
            //var degerler = db.TBLKATEGORILER.ToList(); // uzun yazmak yerıne to lıst metoduyla degerlerı getırdı
            var degerler = db.TBLKATEGORILER.ToList().ToPagedList(sayfa, 4);

            return View(degerler);
        }
        [HttpGet]
        public ActionResult YeniKategori()
        {
            return View();

        }


        [HttpPost]
        public ActionResult YeniKategori(TBLKATEGORILER p1)
        {
            if (!ModelState.IsValid)
            {

             return View("YeniKategori");
            }
            db.TBLKATEGORILER.Add(p1);
            db.SaveChanges();
            return View();


        }
        public ActionResult Sil(int id)
        {

            var kategori = db.TBLKATEGORILER.Find(id);
            db.TBLKATEGORILER.Remove(kategori);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
        public ActionResult KategoriGetir (int id)
        {
            var ktg = db.TBLKATEGORILER.Find(id);
            return View("KategoriGetir",ktg);


        }
        public ActionResult Guncelle(TBLKATEGORILER p1)
        {
            var ktg = db.TBLKATEGORILER.Find(p1.kategoriid);
            ktg.kategoriad = p1.kategoriad;

            db.SaveChanges();
            return RedirectToAction("Index");


        }
    }
}