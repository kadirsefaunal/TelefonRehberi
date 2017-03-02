﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TelefonRehberi.Models;
using TelefonRehberi.ViewModel;

namespace TelefonRehberi.Controllers
{
    public class AdminController : Controller
    {
        private readonly TelefonRehberiDBEntities db = new TelefonRehberiDBEntities();
        private VM vm = new VM();
        // GET: Admin
        public ActionResult Index()
        {
            if(Response.Cookies["KullaniciKimligi"] != null)
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
            try
            {
                db.Calisanlar.Add(calisan);
                db.SaveChanges();
                return Json("+");
            }
            catch (Exception)
            {
                return Json("-");
            }
        }

        public ActionResult CalisanSil(int calisanID)
        {
            var calisan = (from c in db.Calisanlar
                           where c.ID == calisanID
                           select c).Single();

            Calisan mapCalisan = new Calisan()
            {
                ID = calisan.ID,
                CalisanAd = calisan.CalisanAd,
                CalisanSoyad = calisan.CalisanSoyad,
                Telefon = calisan.Telefon,
                Departman = (calisan.Departmanlar != null) ? calisan.Departmanlar.DepartmanAdi : "Belirtilmemiş",
                Yonetici = (calisan.Calisanlar2 != null) ? calisan.Calisanlar2.CalisanAd + " " + calisan.Calisanlar2.CalisanSoyad : "Belirtilmemiş"
            };

            return View(mapCalisan);
        }

        public JsonResult CalisanSill(int calisanID)
        {
            try
            {
                var calisan = (from c in db.Calisanlar
                               where c.ID == calisanID
                               select c).Single();

                if (calisan.Calisanlar1.Count() == 0)
                {
                    db.Calisanlar.Remove(calisan);
                    db.SaveChanges();
                }
                else
                {
                    return Json("Bu çalışan yönetici konumunda");
                }
                return Json("+");
            }
            catch (Exception)
            {
                return Json("-");
            }
        }

        public ActionResult CalisanDuzenle(int calisanID)
        {
            vm.calisanlar = db.Calisanlar.ToList();
            vm.departmanlar = db.Departmanlar.ToList();
            vm.calisan = (from c in db.Calisanlar
                          where c.ID == calisanID
                          select c).Single();
            vm.mapCalisan = new Calisan()
            {
                ID = vm.calisan.ID,
                CalisanAd = vm.calisan.CalisanAd,
                CalisanSoyad = vm.calisan.CalisanSoyad,
                Telefon = vm.calisan.Telefon,
                Departman = (vm.calisan.Departmanlar != null) ? vm.calisan.Departmanlar.DepartmanAdi : "Belirtilmemiş",
                Yonetici = (vm.calisan.Calisanlar2 != null) ? vm.calisan.Calisanlar2.CalisanAd + " " + vm.calisan.Calisanlar2.CalisanSoyad : "Belirtilmemiş"
            };

            return View(vm);
        }
        
        public JsonResult CalisanDuzenlee(Calisanlar calisan)
        {
            try
            {
                var calisanEski = (from c in db.Calisanlar
                                   where c.ID == calisan.ID
                                   select c).Single();
                calisanEski.CalisanAd = calisan.CalisanAd;
                calisanEski.CalisanSoyad = calisan.CalisanSoyad;
                calisanEski.Telefon = calisan.Telefon;
                calisanEski.DepartmanID = calisan.DepartmanID;
                calisanEski.YoneticiID = calisan.YoneticiID;
                db.SaveChanges();

                return Json("+");
            }
            catch (Exception)
            {
                return Json("-");
            }
        }

        public JsonResult SifreDegistir(string eskiParola, string yeniParola)
        {
            try
            {
                int kullaniciID = Convert.ToInt32(Request.Cookies["KullaniciKimligi"].Value);
                var admin = (from a in db.Admin
                             where a.ID == kullaniciID
                             select a).Single();
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