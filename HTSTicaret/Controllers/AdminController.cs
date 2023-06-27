﻿using System;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using HTSTicaret.Models;
using HTSTicaret.WebUI.App_Classes;
using static HTSTicaret.WebUI.Models.Class1;
namespace HTSTicaret.WebUI.Controllers
{
    public class AdminController : Controller
    {
       
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Urunler(string searchName)
        {
            var urunler = Context.Baglanti.Urun.AsQueryable();

            if (!string.IsNullOrEmpty(searchName))
            {

                urunler = urunler.Where(u => u.Adi.Contains(searchName));
            }

            ViewBag.SearchName = searchName;

            return View(urunler.ToList());
        }
        public ActionResult UrunEkle()
        {
            ViewBag.Kategoriler=Context.Baglanti.Kategori.ToList();
            ViewBag.Markalar=Context.Baglanti.Marka.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult UrunEkle(Urun urn)
        {

            Context.Baglanti.Urun.Add(urn);
            Context.Baglanti.SaveChanges();
            return RedirectToAction("Urunler");
        }
        public ActionResult UrunSil(int id)
        {
            var urun=Context.Baglanti.Urun.FirstOrDefault(u=>u.Id==id);
            if (urun!=null)
            {
                Context.Baglanti.Urun.Remove(urun);
                Context.Baglanti.SaveChanges();
            }
            else
            {
                return RedirectToAction("UrunSil");
            }
            return RedirectToAction("Urunler");
        }
        public ActionResult Markalar()
        {
            return View(Context.Baglanti.Marka.ToList());
        }
        public ActionResult MarkaEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult MarkaEkle(Marka mrk,
            HttpPostedFileBase fileUpload)
        {
            int resimId = -1;
            if (fileUpload!=null)
            {
                System.Drawing.Image img = System.Drawing.Image.FromStream(fileUpload.InputStream);

                int width =Convert.ToInt32
                    (ConfigurationManager.AppSettings
                    ["MarkaWidth"].ToString());

                int height = Convert.ToInt32
                    (ConfigurationManager.AppSettings
                    ["MarkaHeight"].ToString());

                string name = "/Content/MarkaResim/" + 
                    Guid.NewGuid() + Path.GetExtension
                    (fileUpload.FileName);

                Bitmap bm = new Bitmap(img, width, height);
                bm.Save(Server.MapPath(name));

                Resim rsm = new Resim();
                rsm.OrtaYol = name;
                Context.Baglanti.Resim.Add(rsm);
                Context.Baglanti.SaveChanges();
                if (rsm.Id != null)
                    resimId = rsm.Id;
            }
            if (resimId != -1)
                mrk.ResimID = resimId;
            Context.Baglanti.Marka.Add(mrk);
            Context.Baglanti.SaveChanges();
            return RedirectToAction("Markalar");
        }
        public ActionResult MarkaSil(int id)
        {
            var marka=Context.Baglanti.Marka.FirstOrDefault(u=>u.Id== id);
            if (marka!=null)
            {
                Context.Baglanti.Marka.Remove(marka);
                Context.Baglanti.SaveChanges();
            }
            else
            {
                return RedirectToAction("MarkaSil");
            }
            return RedirectToAction("Markalar");
        }
        public ActionResult Kategoriler()
        {
            return View(Context.Baglanti.Kategori.ToList());
        }
        public ActionResult KategoriEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult KategoriEkle(Kategori ktg) 
        { 
            Context.Baglanti.Kategori.Add(ktg);
            Context.Baglanti.SaveChanges();
            return RedirectToAction("Kategoriler");
        }
        public ActionResult KategoriSil(int id)
        {
            var urun=Context.Baglanti.Kategori.FirstOrDefault(x => x.Id == id);
            if (urun!=null)
            {
                Context.Baglanti.Kategori.Remove(urun);
                Context.Baglanti.SaveChanges();
            }
            else
            {
                return RedirectToAction("KategoriSil");
            }
            return RedirectToAction("Kategoriler");
        }
        public ActionResult OzellikTipleri()
        {
            return View(Context.Baglanti.OzellikTip.ToList());
        }
        public ActionResult OzellikTipEkle()
        {
            return View(Context.Baglanti.Kategori.ToList());
        }
        [HttpPost]
        public ActionResult OzellikTipEkle(OzellikTip ot)
        {
            Context.Baglanti.OzellikTip.Add(ot);
            Context.Baglanti.SaveChanges();
            return RedirectToAction("OzellikTipleri");
        }
        public ActionResult OzellikDegerleri()
        {
            return View(Context.Baglanti.OzellikDeger.ToList());
        }
        public ActionResult OzellikDegerEkle()
        {
            return View(Context.Baglanti.OzellikTip.ToList());
        }
        [HttpPost]
        public ActionResult OzellikDegerEkle(OzellikDeger od)
        {
            Context.Baglanti.OzellikDeger.Add(od);
            Context.Baglanti.SaveChanges();
            return RedirectToAction("OzellikDegerleri");
        }
        public ActionResult UrunOzellikleri()
        {
            return View(Context.Baglanti.UrunOzellik.ToList());
        }
        public ActionResult UrunOzellikSil(int urunId, int tipId, int degerId)
        {
            UrunOzellik uo = Context.Baglanti.UrunOzellik.FirstOrDefault(x => x.UrunID == urunId && x.OzellikTipID == tipId && x.OzellikDegerID == degerId);
            Context.Baglanti.UrunOzellik.Remove(uo);
            Context.Baglanti.SaveChanges();
            return RedirectToAction("UrunOzellikleri");
        }
        public ActionResult UrunOzellikEkle()
        {

            return View(Context.Baglanti.Urun.ToList());
        }

        public PartialViewResult UrunOzellikTipWidget(int? katId)
        {
            if (katId!=null)
            {
                var data = Context.Baglanti.OzellikTip.Where(x => x.KategoriID == katId).ToList();
                return PartialView(data);
            }
            else
            {
                var data = Context.Baglanti.OzellikTip.ToList();
                return PartialView(data);
            }
        }
        public PartialViewResult urunOzellikDegerWidget(int? tipId)
        {
            if (tipId!=null) 
            {
                var data = Context.Baglanti.OzellikDeger.Where(x => x.OzellikTipId == tipId).ToList();
                return PartialView(data);
            }
            else
            {
                var data = Context.Baglanti.OzellikDeger.ToList();
                return PartialView(data);
            }
        }

        [HttpPost]
        public ActionResult UrunOzellikEkle(UrunOzellik uo)
        {
            Context.Baglanti.UrunOzellik.Add(uo);
            Context.Baglanti.SaveChanges();
            return RedirectToAction("UrunOzellikleri");
        }
        public ActionResult UrunResimEkle(int id)
        {
            var data =Context.Baglanti.Urun.FirstOrDefault(x => x.Id == id);
            return View(data);
        }
        public ActionResult SliderResimleri()
        {
            return View();
        }
        public ActionResult SoapResult()
        {

            var webServiceProxy = new WebServiceProxy();
            string soapResult = webServiceProxy.GetData("input");
            return View(soapResult);
        }

        //[HttpPost]
        //public ActionResult SliderResimEkle(HttpPostedFileBase fileupload, System.Drawing.Image img)
        //{
        //    if (fileupload!=null)
        //    {
        //        System.Drawing.Image img Image.FromStream(fileupload.InputStream);

        //        Bitmap bmp = new Bitmap(img, 
        //            Settings.SliderResimBoyut);

        //        string yol = "/Content/SliderResim/"+Guid.NewGuid()+Path.GetExtension(fileupload.FileName);
        //        bmp.Save(Server.MapPath(yol));

        //        Resim rsm = new Resim();
        //        rsm.BuyukYol = yol;
        //        Context.Baglanti.Resim.Add(rsm);
        //        Context.Baglanti.SaveChanges();


        //    }
        //    return View();
        //}
    }
 }