
using HTSTicaret.Models;
using HTSTicaret.WebUI.App_Classes;
using System;
using System.Data.SqlClient;
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
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    using (SqlCommand com = new SqlCommand("SELECT * FROM dbo.Kargo WHERE Email=@Email AND Password=@Password", con))
                    {
                        com.Parameters.AddWithValue("@Email", email);
                        com.Parameters.AddWithValue("@Password", password);

                        using (SqlDataReader dr = com.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                return RedirectToAction("index", "Admin");
                            }
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
            Context.Baglanti.Kargo.Add(krg);
            Context.Baglanti.SaveChanges();
            return RedirectToAction("Login");
        }
    }
}