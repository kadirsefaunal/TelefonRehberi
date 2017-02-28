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
    }
}