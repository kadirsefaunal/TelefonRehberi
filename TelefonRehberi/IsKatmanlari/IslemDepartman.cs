using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TelefonRehberi.Models;

namespace TelefonRehberi.IsKatmanlari
{
    public static class IslemDepartman
    {
        public static string DepartmanEkle (Departmanlar departman)
        {
            try
            {
                using (TelefonRehberiDBEntities db = new TelefonRehberiDBEntities())
                {
                    departman.DepartmanAdi = departman.DepartmanAdi.ToUpper();
                    var departmanKontrol = (from d in db.Departmanlar
                                            where d.DepartmanAdi == departman.DepartmanAdi
                                            select d).SingleOrDefault();

                    if (departmanKontrol == null)
                    {
                        db.Departmanlar.Add(departman);
                        db.SaveChanges();
                        return "Departman ekleme işlemi başarılı.";
                    }
                    else
                    {
                        return "Eklemeye çalıştığınız departman zaten mevcut!";
                    }
                }
            }
            catch (Exception ex)
            {
                return "Departman ekleme başarısız! Hata: " + ex.Message;
            }
        }

        public static string DepartmanSil (int departmanID)
        {
            try
            {
                using (TelefonRehberiDBEntities db = new TelefonRehberiDBEntities())
                {
                    var departman = (from d in db.Departmanlar
                                     where d.ID == departmanID
                                     select d).SingleOrDefault();

                    if (departman.Calisanlar.Count() == 0)
                    {
                        db.Departmanlar.Remove(departman);
                        db.SaveChanges();

                        return "Departman silme başarılı.";
                    }
                    else
                    {
                        return "Bu departmanı silemezsiniz! Bu departmanda çalışanlar var";
                    }
                }
            }
            catch (Exception ex)
            {
                return "Departman silme işlemi hatalı! Hata: " + ex.Message;
            }
        }

        public static string DepartmanGuncelle (Departmanlar departman)
        {
            try
            {
                using (TelefonRehberiDBEntities db = new TelefonRehberiDBEntities())
                {
                    var guncellenecekDepartman = (from d in db.Departmanlar
                                                  where d.ID == departman.ID
                                                  select d).SingleOrDefault();

                    guncellenecekDepartman.DepartmanAdi = departman.DepartmanAdi.ToUpper();
                    guncellenecekDepartman.Aciklama = departman.Aciklama;
                    db.SaveChanges();

                    return "Departman güncelleme başarılı.";
                }
            }
            catch (Exception ex)
            {
                return "Departman güncelleme başarısız! Hata: " + ex.Message;
            }
        }
    }
}