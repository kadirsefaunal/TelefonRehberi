using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TelefonRehberi.Models;

namespace TelefonRehberi.ViewModel
{
    public class Calisan
    {
        public int ID { get; set; }
        public string CalisanAd { get; set; }
        public string CalisanSoyad { get; set; }
        public string Telefon { get; set; }
        public string Departman { get; set; }
        public string Yonetici { get; set; }
    }
}