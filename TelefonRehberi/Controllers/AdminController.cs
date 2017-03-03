using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TelefonRehberi.Models;
using TelefonRehberi.ViewModel;
using TelefonRehberi.IsKatmanlari;

namespace TelefonRehberi.Controllers
{
    public class AdminController : Controller
    {
        private readonly TelefonRehberiDBEntities db = new TelefonRehberiDBEntities();
        private VM vm = new VM();
        // GET: Admin
        public ActionResult Index()
        {
            if(Request.Cookies["KullaniciKimligi"] != null)
            {
                vm.calisanlar = db.Calisanlar.ToList();
                return View(vm);
            }
            return RedirectToAction("Index", "Public");
        }
        
        public ActionResult CalisanEkle()
        {
            if (Request.Cookies["KullaniciKimligi"] != null)
            {
                vm.calisanlar = db.Calisanlar.ToList();
                vm.departmanlar = db.Departmanlar.ToList();
                return View(vm);
            }
            return RedirectToAction("Index", "Public");
        }

        public JsonResult CEkle(Calisanlar calisan)
        {
            string sonuc = IslemCalisan.CalisanEkle(calisan);
            return Json(sonuc);
        }

        public ActionResult CalisanSil(int calisanID)
        {
            if (Request.Cookies["KullaniciKimligi"] != null)
            {
                Calisan mapCalisan = IslemCalisan.CalisanGetir(calisanID);
                if (mapCalisan != null)
                {
                    return View(mapCalisan);
                }
                else
                {
                    return RedirectToAction("Index", "Admin");
                }
            }
            return RedirectToAction("Index", "Public");
        }

        public JsonResult CalisanSill(int calisanID)
        {
            string sonuc = IslemCalisan.CalisanSil(calisanID);
            return Json(sonuc);
        }

        public ActionResult CalisanDuzenle(int calisanID)
        {
            if (Request.Cookies["KullaniciKimligi"] != null)
            {
                vm = IslemCalisan.CalisanGetirDuzenleme(calisanID);
                if (vm != null)
                {
                    return View(vm);
                }
                else
                {
                    return RedirectToAction("Index", "Admin");
                }
            }
            return RedirectToAction("Index", "Public");
        }
        
        public JsonResult CalisanDuzenlee(Calisanlar calisan)
        {
            string sonuc = IslemCalisan.CalisanDuzenle(calisan);
            return Json(sonuc);
        }

        public ActionResult Departmanlar()
        {
            if (Request.Cookies["KullaniciKimligi"] != null)
            {
                vm.departmanlar = db.Departmanlar.ToList();
                return View(vm);
            }
            return RedirectToAction("Index", "Public");
        }

        public ActionResult DepartmanEkle()
        {
            if (Request.Cookies["KullaniciKimligi"] != null)
            {
                return View();
            }
            return RedirectToAction("Index", "Public");
        }

        public JsonResult DepartmanEklee(Departmanlar departman)
        {
            string sonuc = IslemDepartman.DepartmanEkle(departman);
            return Json(sonuc);
        }

        public ActionResult DepartmanSil(int departmanID)
        {
            if (Request.Cookies["KullaniciKimligi"] != null)
            {
                var departman = (from d in db.Departmanlar
                                 where d.ID == departmanID
                                 select d).SingleOrDefault();

                if (departman != null)
                {
                    return View(departman);
                }
                else
                {
                    return RedirectToAction("Departmanlar", "Admin");
                }
            }
            return RedirectToAction("Index", "Public");
        }

        public JsonResult DepartmanSill(int departmanID)
        {
            string sonuc = IslemDepartman.DepartmanSil(departmanID);
            return Json(sonuc);
        }

        public ActionResult DepartmanDuzenle(int departmanID)
        {
            if (Request.Cookies["KullaniciKimligi"] != null)
            {
                var departman = (from d in db.Departmanlar
                                 where d.ID == departmanID
                                 select d).SingleOrDefault();

                if (departman != null)
                {
                    return View(departman);
                }
                else
                {
                    return RedirectToAction("Departmanlar", "Admin");
                }
            }
            return RedirectToAction("Index", "Public");
        }

        public JsonResult DepartmanDuzenlee(Departmanlar departman)
        {
            string sonuc = IslemDepartman.DepartmanGuncelle(departman);
            return Json(sonuc);
        }

        public JsonResult SifreDegistir(string eskiParola, string yeniParola)
        {
            try
            {
                int kullaniciID = Convert.ToInt32(Request.Cookies["KullaniciKimligi"].Value);
                var admin = (from a in db.Admin
                             where a.ID == kullaniciID
                             select a).SingleOrDefault();
                if (admin != null)
                {
                    if (eskiParola == admin.Parola)
                    {
                        admin.Parola = yeniParola;
                        db.SaveChanges();
                        return Json("Başarılı.");
                    }
                    else
                    {
                        return Json("Parola Hatalı!");
                    }
                }
                else
                {
                    return Json("Başarısız!");
                }
            }
            catch (Exception)
            {
                return Json("Başarısız!");
            }
        }
    }
}