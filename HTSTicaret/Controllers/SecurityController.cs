using HTSTicaret.Models;
using HTSTicaret.WebUI.App_Classes;
using System;
using System.Linq;
using System.Web.Mvc;

namespace HTSTicaret.Controllers
{
    public class SecurityController : Controller
    {
        private string connectionString = "data source=DESKTOP-BTFQGIN; database=B403_eTicaret;integrated security=SSPI;";

        [HttpGet]
        public ActionResult Login()
        {
            
            return View();
        }

        [HttpGet]
        public ActionResult KayitOl()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Verify(Kargo acc)
        {
            string email = acc.Email;
            string password = acc.Password;

            try
            {
                using (var context = new Entities1())
                {
                    var user = context.Kargo.FirstOrDefault(u => u.Email == email && u.Password == password);

                    if (user != null)
                    {
                        SetLoggedInUser(email);
                        if (user.Identy == 2)
                        {
                            return RedirectToAction("Index", "Admin");
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }

                ViewBag.ErrorMessage = "Geçersiz e-posta veya şifre.";
                return View("Verify");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Bir hata oluştu: " + ex.Message;
                return View("Verify");
            }
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

        public ActionResult Logout()
        {
            ClearLoggedInUser();
            return RedirectToAction("Login");
        }

        private void SetLoggedInUser(string email)
        {
            Session["LoggedInUser"] = email;
        }

        private void ClearLoggedInUser()
        {
            Session.Remove("LoggedInUser");
        }

    }
}
