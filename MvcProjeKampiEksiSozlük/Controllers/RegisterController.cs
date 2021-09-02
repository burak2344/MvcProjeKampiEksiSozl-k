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
    [AllowAnonymous]

    public class RegisterController : Controller
    {
        // GET: Register
        WriterManager writerManager = new WriterManager(new EfWriterDal());
        AdminManager adminManager = new AdminManager(new EfAdminDal());
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Index(Admin admin)
        {
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            string password = admin.AdminPassword;
            string result = Convert.ToBase64String(sha1.ComputeHash(Encoding.UTF8.GetBytes(password)));
            admin.AdminPassword = result;
            admin.AdminRole = "B";
            adminManager.AdminAddBL(admin);
            return RedirectToAction("Index", "Login");
        }

        [HttpGet]
        public ActionResult WriterRegister()
        {
            return View();
        }

        [HttpPost]
        public ActionResult WriterRegister(Writer writer)
        {
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            string password = writer.WriterPassword;
            string result = Convert.ToBase64String(sha1.ComputeHash(Encoding.UTF8.GetBytes(password)));
            writer.WriterPassword = result;
            writer.WriterStatus = true;
            writer.WriterRole = "C";
            writerManager.WriterAdd(writer);
            return RedirectToAction("WriterLogin", "Login");
        }
    }
}