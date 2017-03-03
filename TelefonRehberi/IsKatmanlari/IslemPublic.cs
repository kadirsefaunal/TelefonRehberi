using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TelefonRehberi.ViewModel;
using TelefonRehberi.Models;

namespace TelefonRehberi.IsKatmanlari
{
    public static class IslemPublic 
    {
        private static TelefonRehberiDBEntities db = new TelefonRehberiDBEntities();

        public static Calisan CalisanDetay(int calisanID)
        {
            var calisan = (from c in db.Calisanlar
                           where c.ID == calisanID
                           select c).SingleOrDefault();

            Calisan mapCalisan = new Calisan()
            {
                ID = calisan.ID,
                CalisanAd = calisan.CalisanAd,
                CalisanSoyad = calisan.CalisanSoyad,
                Telefon = calisan.Telefon,
                Departman = (calisan.Departmanlar != null) ? calisan.Departmanlar.DepartmanAdi : "Belirtilmemiş",
                Yonetici = (calisan.Calisanlar2 != null) ? calisan.Calisanlar2.CalisanAd + " " + calisan.Calisanlar2.CalisanSoyad : "Belirtilmemiş"
            };

            return mapCalisan;
        }
    }
}