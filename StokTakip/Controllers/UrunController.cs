using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StokTakip.Models.Entity;

namespace StokTakip.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun
        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult Index()
        {

            var degerler = db.TBLURUNLER.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult UrunEkle()
        {
            List<SelectListItem> degerler = (from i in db.TBLKATEGORILER.ToList()
                                             select new SelectListItem

                                             {
                                                 Text = i.kategoriad,
                                                 Value = i.kategoriid.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;

            return View();

        }
        [HttpPost]
        public ActionResult UrunEkle(TBLURUNLER p1)
        {
            var ktg=db.TBLKATEGORILER.Where(m=> m.kategoriid== p1.TBLKATEGORILER.kategoriid).FirstOrDefault(); //kategoriid almak için linq sorgusu yaptım
            p1.TBLKATEGORILER = ktg;


            db.TBLURUNLER.Add(p1);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Sil(int id)
        {
            var urun = db.TBLURUNLER.Find(id); //find ile bul remove ile kaldır
            db.TBLURUNLER.Remove(urun);
            db.SaveChanges();
            return RedirectToAction("Index");


        }
        public ActionResult UrunGetir(int id)
        {

            var urun = db.TBLURUNLER.Find(id);

            List<SelectListItem> degerler = (from i in db.TBLKATEGORILER.ToList()
                                             select new SelectListItem

                                             {
                                                 Text = i.kategoriad,
                                                 Value = i.kategoriid.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;
            return View("UrunGetir", urun);

        }
        public ActionResult Guncelle (TBLURUNLER p)
        {

            var urun = db.TBLURUNLER.Find(p.urunid);
            urun.urunad = p.urunad;
            urun.marka = p.marka;
            urun.fiyat = p.fiyat;
            urun.stok = p.stok;
            //urun.urunkategori = p.urunkategori;
            var ktg = db.TBLKATEGORILER.Where(m => m.kategoriid == p.TBLKATEGORILER.kategoriid).FirstOrDefault();
            urun.urunkategori = ktg.kategoriid;
            db.SaveChanges();
            return RedirectToAction("Index");

        }

    }
}

