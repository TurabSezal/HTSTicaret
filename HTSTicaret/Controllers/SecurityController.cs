using HTSTicaret.Models;
using HTSTicaret.WebUI.App_Classes;
using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web.Mvc;
using System.Web.Security;

namespace HTSTicaret.Controllers
{
    
    public class SecurityController : Controller
    {
        Entities1 db = new Entities1();
        public ActionResult KayitOl()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Kaydet(Kargo krg)
        {
            using (var context = new Entities1())
            {
                context.Kargo.Add(krg);
                context.SaveChanges();
            }

            return RedirectToAction("Login");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Kargo t)
        {
            var bilgiler =db.Kargo.FirstOrDefault(x=> x.Email == t.Email&& x.Password==t.Password);
            if (bilgiler != null)
            {
                if (bilgiler.Identy==0)
                {
                    FormsAuthentication.SetAuthCookie(bilgiler.Email,true);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(bilgiler.Email, true);
                    return RedirectToAction("Index", "Admin");
                   
                }
            }
            else
            {
                return View();
            }
            
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}
