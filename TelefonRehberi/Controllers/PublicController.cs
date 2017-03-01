using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TelefonRehberi.Models;
using TelefonRehberi.ViewModel;

namespace TelefonRehberi.Controllers
{
    public class PublicController : Controller
    {
        private readonly TelefonRehberiDBEntities db = new TelefonRehberiDBEntities();
        private VM vm = new VM();

        // GET: Public
        public ActionResult Index()
        {
            return View();
        }
    }
}