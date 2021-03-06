﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TelefonRehberi.Models;

namespace TelefonRehberi.ViewModel
{
    public class VM
    {
        public Admin admin { get; set; }

        public List<Calisanlar> calisanlar { get; set; }

        public Calisanlar calisan { get; set; }

        public Calisan mapCalisan { get; set; }

        public List<Departmanlar> departmanlar { get; set; }
    }
}