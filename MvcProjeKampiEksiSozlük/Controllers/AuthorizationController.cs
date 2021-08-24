using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampiEksiSozlük.Controllers
{
    public class AuthorizationController : Controller
    {
        // GET: Authorization

        AdminManager adminManager = new AdminManager(new EfAdminDal());
        public ActionResult Index()
        {
            var adminValues = adminManager.GetList();
            return View(adminValues);
        }


        [HttpGet]
        public ActionResult AdminAdd()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AdminAdd(Admin admin)
        {
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            string password = admin.AdminPassword;
            string result = Convert.ToBase64String(sha1.ComputeHash(Encoding.UTF8.GetBytes(password)));
            admin.AdminPassword = result;
            adminManager.AdminAddBL(admin);
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult AdminUpdate(int id)
        {
            var adminValue = adminManager.GetByID(id);
            return View(adminValue);
        }

        [HttpPost]
        public ActionResult AdminUpdate(Admin admin)
        {
            var adminValue = adminManager.GetByID(admin.AdminId);
            admin.AdminPassword = adminValue.AdminPassword;
            adminManager.AdminUpdate(admin);
            return RedirectToAction("Index");
        }

        public ActionResult AdminDelete(int id)
        {
            var adminValue = adminManager.GetByID(id);
            adminManager.AdminDelete(adminValue);
            return RedirectToAction("Index");
        }

        public PartialViewResult AdminPartial()
        {
            return PartialView();
        }

    }
}