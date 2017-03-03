using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TelefonRehberi.Models;
using TelefonRehberi.ViewModel;

namespace TelefonRehberi.IsKatmanlari
{
    public static class IslemCalisan
    {
        private static VM vm = new VM();

        public static string CalisanEkle(Calisanlar calisan)
        {
            try
            {
                using (TelefonRehberiDBEntities db = new TelefonRehberiDBEntities())
                {
                    db.Calisanlar.Add(calisan);
                    db.SaveChanges();
                }
                return "Çalışan ekleme başarılı.";
            }
            catch (Exception ex)
            {
                return "Çalışan ekleme başarısız! Hata: " + ex.Message; 
            }
        }

        public static Calisan CalisanGetir(int calisanID)
        {
            try
            {
                using (TelefonRehberiDBEntities db = new TelefonRehberiDBEntities())
                {
                    var calisan = (from c in db.Calisanlar
                                   where c.ID == calisanID
                                   select c).SingleOrDefault();

                    if (calisan != null)
                    {
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
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static string CalisanSil(int calisanID)
        {
            try
            {
                using (TelefonRehberiDBEntities db = new TelefonRehberiDBEntities())
                {
                    var calisan = (from c in db.Calisanlar
                                   where c.ID == calisanID
                                   select c).SingleOrDefault();

                    if (calisan.Calisanlar1.Count() == 0)
                    {
                        db.Calisanlar.Remove(calisan);
                        db.SaveChanges();
                        return "Başarılı.";
                    }
                    else
                    {
                        return "Bu çalışan yönetici konumunda!";
                    }
                }
            }
            catch (Exception ex)
            {
                return "Çalışan Silme başarısız! Hata: " + ex.Message;
            }
        }

        public static VM CalisanGetirDuzenleme(int calisanID)
        {
            try
            {
                using (TelefonRehberiDBEntities db = new TelefonRehberiDBEntities())
                {
                    vm.calisan = (from c in db.Calisanlar
                                  where c.ID == calisanID
                                  select c).SingleOrDefault();

                    if (vm.calisan != null)
                    {
                        vm.calisanlar = db.Calisanlar.ToList();
                        vm.departmanlar = db.Departmanlar.ToList();

                        vm.mapCalisan = new Calisan()
                        {
                            ID = vm.calisan.ID,
                            CalisanAd = vm.calisan.CalisanAd,
                            CalisanSoyad = vm.calisan.CalisanSoyad,
                            Telefon = vm.calisan.Telefon,
                            Departman = (vm.calisan.Departmanlar != null) ? vm.calisan.Departmanlar.DepartmanAdi : "Belirtilmemiş",
                            Yonetici = (vm.calisan.Calisanlar2 != null) ? vm.calisan.Calisanlar2.CalisanAd + " " + vm.calisan.Calisanlar2.CalisanSoyad : "Belirtilmemiş"
                        };
                        return vm;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static string CalisanDuzenle(Calisanlar calisan)
        {
            try
            {
                using (TelefonRehberiDBEntities db = new TelefonRehberiDBEntities())
                {
                    var calisanEski = (from c in db.Calisanlar
                                       where c.ID == calisan.ID
                                       select c).SingleOrDefault();

                    calisanEski.CalisanAd = calisan.CalisanAd;
                    calisanEski.CalisanSoyad = calisan.CalisanSoyad;
                    calisanEski.Telefon = calisan.Telefon;
                    calisanEski.DepartmanID = calisan.DepartmanID;
                    calisanEski.YoneticiID = calisan.YoneticiID;

                    db.SaveChanges();

                    return "Başarılı.";
                }
            }
            catch (Exception ex)
            {
                return "Çalışan güncelleme başarısız! Hata: " + ex.Message;
            }
        }
    }
}