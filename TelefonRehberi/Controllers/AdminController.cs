using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TelefonRehberi.Models;

namespace TelefonRehberi.Controllers
{
    public class AdminController : Controller
    {
        TelefonRehberiDBEntities db = new TelefonRehberiDBEntities();
        // GET: Admin
        public ActionResult Index()
        {
            
            return View();
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