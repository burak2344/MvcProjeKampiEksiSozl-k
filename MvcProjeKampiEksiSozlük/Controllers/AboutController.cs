using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampiEksiSozlük.Controllers
{
    public class AboutController : Controller
    {
        // GET: About

        AboutManager aboutManager = new AboutManager(new EfAboutDal());
        public ActionResult Index()
        {
            var aboutvalues = aboutManager.GetList();
            return View(aboutvalues);
        }
        [HttpGet]
        public ActionResult AddAbout()
		{
            return View();
		}

        [HttpPost]
        public ActionResult AddAbout(About p)
        {
            aboutManager.AboutAddBL(p);
            return RedirectToAction("Index"); 
        }

        [HttpGet]
        public ActionResult UpdateAbout(int id)
        {
            var aboutValues = aboutManager.GetByID(id);
            return View(aboutValues);
        }

        [HttpPost]
        public ActionResult UpdateAbout(About about)
        {
            aboutManager.AboutUpdate(about);
            return RedirectToAction("Index");
        }

        public PartialViewResult AboutPartial()
		{
            return PartialView();
		}


        public ActionResult StatusActiveAndPassive(int id)
        {
            var aboutValue = aboutManager.GetByID(id);

            if (aboutValue.AboutStatus == true)
            {
                aboutValue.AboutStatus = false;
            }
            else
            {
                aboutValue.AboutStatus = true;
            }
            aboutManager.AboutUpdate(aboutValue);
            return RedirectToAction("Index");
        }
    }
}