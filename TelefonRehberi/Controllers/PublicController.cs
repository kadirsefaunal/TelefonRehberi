﻿using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TelefonRehberi.Models;
using TelefonRehberi.ViewModel;
using TelefonRehberi.IsKatmanlari;

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

        /// <summary>
        /// Çalışanın detay bilgisini gösterir.
        /// </summary>
        /// <param name="calisanID">Detayı istenen çalışanın ID'si</param>
        /// <returns>Partialview</returns>
        [HttpPost]
        public ActionResult CalisanDetay(int calisanID)
        {
            Calisan mapCalisan = IslemPublic.CalisanDetay(calisanID);
            return PartialView("_CalisanDetay", mapCalisan);
        }
        
        /// <summary>
        /// Adminin sisteme giriş yapmasını sağlar
        /// </summary>
        /// <param name="kullaniciAdi"></param>
        /// <param name="parola"></param>
        /// <returns>Giriş işleminin sonucu</returns>
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

        /// <summary>
        /// Adminin sistemden çıkmasını sağlar.
        /// </summary>
        /// <returns></returns>
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