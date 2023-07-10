using System.Linq;
using System.Web.Mvc;
using HTSTicaret.Models;
using HTSTicaret.WebUI.App_Classes;

namespace HTSTicaret.WebUI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Kategoriler = Context.Baglanti.Kategori.ToList();
            ViewBag.Markalar = Context.Baglanti.Marka.ToList();
            return View();
        }

        public PartialViewResult SepetGoster()
        {
            var urunler = Context.Baglanti.Urun.AsQueryable();
            var sepetItems = urunler.Select(u => new SepetItem { urun = u, adet = 1 }).ToList();
            return PartialView(sepetItems);
        }

        public PartialViewResult Slider()
        {
            return PartialView();
        }

        public PartialViewResult YeniUrunler()
        {
            var data = Context.Baglanti.Urun.ToList();
            return PartialView(data);
        }

        public PartialViewResult Servisler()
        {
            return PartialView();
        }

        public ActionResult Hakkimizda()
        {
            ViewBag.Kategoriler = Context.Baglanti.Kategori.ToList();
            ViewBag.Markalar = Context.Baglanti.Marka.ToList();
            return View();
        }

        public ActionResult SepeteAt(int id)
        {
            var urun = Context.Baglanti.Urun.FirstOrDefault(u => u.Id == id);
            if (urun != null)
            {
                Sepet.AktifSepet.SepeteEkle(new SepetItem { urun = urun, adet = 1 });
            }
            return RedirectToAction("Index");
        }


        public ActionResult SepettenCikar(int id)
        {
            var urun = Context.Baglanti.Urun.FirstOrDefault(u => u.Id == id);
            if (urun != null)
            {
                Sepet.AktifSepet.SepetCikar(new SepetItem { urun = urun, adet = 1 });
            }
            return RedirectToAction("Index");
        }
        public ActionResult UrunDetay(int id)
        {
            ViewBag.Kategoriler = Context.Baglanti.Kategori.ToList();
            ViewBag.Markalar = Context.Baglanti.Marka.ToList();
            ViewBag.UrunOzellik = Context.Baglanti.UrunOzellik.FirstOrDefault(u => u.UrunID == id);
            var urun = Context.Baglanti.Urun.FirstOrDefault(u => u.Id == id);
            return View(urun);
        }
        public ActionResult MarkaUrunler(int id)
        {
            ViewBag.Kategoriler = Context.Baglanti.Kategori.ToList();
            ViewBag.Markalar = Context.Baglanti.Marka.ToList();
            var urunler = Context.Baglanti.Urun.Where(u => u.MarkaID == id).ToList();
            return View(urunler);
        }
        public ActionResult KategoriUrunler(int id)
        {
            ViewBag.Kategoriler = Context.Baglanti.Kategori.ToList();
            ViewBag.Markalar=Context.Baglanti.Marka.ToList() ;
            var urunler=Context.Baglanti.Urun.Where(u=>u.KategoriID== id).ToList();
            return View(urunler);
        }

    }
}
