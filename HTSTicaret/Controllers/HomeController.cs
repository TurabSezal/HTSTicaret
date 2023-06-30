using System.Linq;
using System.Web.Mvc;
using HTSTicaret.Models;
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
            var urunler = Context.Baglanti.Urun.AsQueryable();
            return PartialView(urunler.ToList());
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
        public ActionResult SepeteAt(int id, Urun urn)
        {
            var urun = Context.Baglanti.Urun.FirstOrDefault(u => u.Id == id);
            if (urun != null)
            {
                SepeteAt(id, urn);
            }
            return RedirectToAction("Index");
        }

    }
}