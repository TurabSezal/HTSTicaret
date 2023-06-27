using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HTSTicaret.WebUI.App_Classes;

namespace HTSTicaret.WebUI.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult Sepet()
        {
            return PartialView();
        }
        public PartialViewResult Slider()
        {
            return PartialView();
        }
        public PartialViewResult YeniUrunler()
        {
            var data=Context.Baglanti.Urun.ToList();
            return PartialView(data);
        }
        public PartialViewResult Servisler()
        {
            return PartialView();
        }
        public PartialViewResult Markalar()
        {
            var data=Context.Baglanti.Marka.ToList();
            return PartialView(data);
        }
        public ActionResult Hakkimizda()
        {
            return View();
        }

    }
}