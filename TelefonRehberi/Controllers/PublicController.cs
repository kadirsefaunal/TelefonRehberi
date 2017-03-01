using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TelefonRehberi.Models;
using TelefonRehberi.ViewModel;

namespace TelefonRehberi.Controllers
{
    public class PublicController : Controller
    {
        private readonly TelefonRehberiDBEntities db = new TelefonRehberiDBEntities();
        private VM vm = new VM();

        // GET: Public
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GirisYap(string kullaniciAdi, string parola)
        {
            try
            {
                int kullaniciKimligi = (from k in db.Admin
                                        where k.KullaniciAdi == kullaniciAdi && k.Parola == parola
                                        select k.ID).SingleOrDefault();

                if (kullaniciKimligi != 0)
                {
                    Response.Cookies["KullaniciKimligi"].Value = kullaniciKimligi.ToString();
                    Response.Cookies["KullaniciKimligi"].Expires = DateTime.Now.AddDays(1);
                    Session["KullaniciID"] = Convert.ToInt32(Response.Cookies["KullaniciKimligi"].Value);

                    Session["KullaniciID"] = kullaniciKimligi;

                    HttpCookie sonZiyaret = new HttpCookie("SonZiyaret", DateTime.Now.ToString());
                    sonZiyaret.Expires = DateTime.Now.AddDays(1);
                    Response.Cookies.Add(sonZiyaret);

                    return Json("Giriş başarılı.");
                }
                else
                {
                    return Json("Giriş başarısız.");
                }
            }
            catch
            {
                return Json("Giriş başarısız.");
            }
        }
    }
}