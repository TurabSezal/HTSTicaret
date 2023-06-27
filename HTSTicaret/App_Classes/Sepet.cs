
using HTSTicaret.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HTSTicaret.WebUI.App_Classes
{
    public class Sepet
    {
        public static Sepet AktifSepet
        {
            get
            {
                HttpContext ctx = HttpContext.Current;
                if (ctx.Session["AktifSepet"] == null)
                    ctx.Session["AktifSepet"] = new Sepet();

                return (Sepet)ctx.Session["AktifSepet"];




            }
        }

        private List<SepetItem> urunler = new List<SepetItem>();

        public List<SepetItem> MyProperty
        {
            get { return urunler; }
            set { urunler = value; }
        }

        public void SepeteEkle(SepetItem si)
        {
            if (urunler.Any(x => x.urun.Id == si.urun.Id))
                urunler.FirstOrDefault(x => x.urun.Id == si.urun.Id).adet++;
            else
                urunler.Add(si);
        }
    }

    public class SepetItem
    {
        public Urun urun { get; set; }
        public int adet { get; set; }
        public double Indirim { get; set; }
        //public decimal Tutar
        //{
        //    get
        //    {
        //        return urun.SatisFiyati * adet * (decimal)(1 - Indirim);
        //    }
        //}
    }
}