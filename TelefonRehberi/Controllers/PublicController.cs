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
            vm.calisanlar = db.Calisanlar.ToList();
            return View(vm);
        }

        public JsonResult CalisanDetay(int calisanID)
        {
            var calisan = (from c in db.Calisanlar
                           where c.ID == calisanID
                           select c).Single();

            Calisan mapCalisan = new Calisan() {
                ID = calisan.ID,
                CalisanAd = calisan.CalisanAd,
                CalisanSoyad = calisan.CalisanSoyad,
                Telefon = calisan.Telefon,
                Departman = (calisan.Departmanlar != null) ? calisan.Departmanlar.DepartmanAdi : "Belirtilmemiş",
                Yonetici = (calisan.Calisanlar2 != null) ? calisan.Calisanlar2.CalisanAd + " " + calisan.Calisanlar2.CalisanSoyad : "Belirtilmemiş"
            };

            return Json(mapCalisan);
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

        public JsonResult CikisYap()
        {
            try
            {
                Response.Cookies["KullaniciKimligi"].Value = null;
                Response.Cookies["KullaniciKimligi"].Expires = DateTime.Now.AddDays(-1);
                Session["KullaniciID"] = null;

                HttpCookie sonZiyaret = new HttpCookie("SonZiyaret", DateTime.Now.ToString());
                sonZiyaret.Expires = DateTime.Now.AddDays(1);
                Response.Cookies.Add(sonZiyaret);

                return Json("Çıkış Başarılı.");
            }
            catch
            {
                return Json("Çıkış Başarısız!");
            }
        }
    }
}