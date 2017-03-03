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
        
        /// <summary>
        /// Çalışan ekleme sayfasına yönlendirir
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Çalışanı veritabanına ekler
        /// </summary>
        /// <param name="calisan"></param>
        /// <returns></returns>
        public JsonResult CEkle(Calisanlar calisan)
        {
            string sonuc = IslemCalisan.CalisanEkle(calisan);
            return Json(sonuc);
        }

        /// <summary>
        /// Çalışan silme ekranını açar ve silinecek çalışanın bilgilerini gösterir
        /// </summary>
        /// <param name="calisanID"></param>
        /// <returns></returns>
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

        /// <summary>
        /// ID'si gelen çalışanı koşulu sağlıyorsa veritabanından siler
        /// </summary>
        /// <param name="calisanID"></param>
        /// <returns></returns>
        public JsonResult CalisanSill(int calisanID)
        {
            string sonuc = IslemCalisan.CalisanSil(calisanID);
            return Json(sonuc);
        }

        /// <summary>
        /// Çalışan düzenle ekranını açar ve çalışana ait bilgileri gösterir
        /// </summary>
        /// <param name="calisanID"></param>
        /// <returns></returns>
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
        
        /// <summary>
        /// Çalışana ait yapılan değişiklikleri veritabanına kaydeder
        /// </summary>
        /// <param name="calisan"></param>
        /// <returns></returns>
        public JsonResult CalisanDuzenlee(Calisanlar calisan)
        {
            string sonuc = IslemCalisan.CalisanDuzenle(calisan);
            return Json(sonuc);
        }

        /// <summary>
        /// Departmanların listelendiği sayfayı açar
        /// </summary>
        /// <returns></returns>
        public ActionResult Departmanlar()
        {
            if (Request.Cookies["KullaniciKimligi"] != null)
            {
                vm.departmanlar = db.Departmanlar.ToList();
                return View(vm);
            }
            return RedirectToAction("Index", "Public");
        }

        /// <summary>
        /// Departman ekleme sayfasını açar
        /// </summary>
        /// <returns></returns>
        public ActionResult DepartmanEkle()
        {
            if (Request.Cookies["KullaniciKimligi"] != null)
            {
                return View();
            }
            return RedirectToAction("Index", "Public");
        }

        /// <summary>
        /// Gelen departman eğer veritabanında bulunmuyorsa veritabanına kaydeder
        /// </summary>
        /// <param name="departman"></param>
        /// <returns></returns>
        public JsonResult DepartmanEklee(Departmanlar departman)
        {
            string sonuc = IslemDepartman.DepartmanEkle(departman);
            return Json(sonuc);
        }

        /// <summary>
        /// Departmanı sil ekranını açar ve silinecek departmanın bilgilerini gösterir
        /// </summary>
        /// <param name="departmanID"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Seçilen departmanı eğer şartları sağlıyorsa veritabanından siler
        /// </summary>
        /// <param name="departmanID"></param>
        /// <returns></returns>
        public JsonResult DepartmanSill(int departmanID)
        {
            string sonuc = IslemDepartman.DepartmanSil(departmanID);
            return Json(sonuc);
        }

        /// <summary>
        /// Departman düzenleme sayfasını açar ve departmanın bilgilerini gösterir
        /// </summary>
        /// <param name="departmanID"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Düzenlenen departmanı veritabanına kaydeder
        /// </summary>
        /// <param name="departman"></param>
        /// <returns></returns>
        public JsonResult DepartmanDuzenlee(Departmanlar departman)
        {
            string sonuc = IslemDepartman.DepartmanGuncelle(departman);
            return Json(sonuc);
        }

        /// <summary>
        /// Adminin şifresini değiştirme işlemini gerçekleştirir
        /// </summary>
        /// <param name="eskiParola"></param>
        /// <param name="yeniParola"></param>
        /// <returns></returns>
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