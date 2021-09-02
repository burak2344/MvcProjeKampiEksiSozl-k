using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;



namespace MvcProjeKampiEksiSozlük.Controllers
{
    [AllowAnonymous]

    public class DefaultController : Controller
    {
        // GET: Default
        ContentManager contentManager = new ContentManager(new EfContentDal());
        HeadingManager headingManager = new HeadingManager(new EfHeadingDal());
        InternManager internManager = new InternManager(new EfInternDal());
        InternValidator internValidator = new InternValidator();
        public ActionResult Index(int id = 1)
        {
            var contentList = contentManager.GetListByHeadingID(id);
            return PartialView(contentList);
        }

        public ActionResult Headings()
        {
            var headingList = headingManager.GetList();
            return View(headingList);
        }

        [HttpGet]
        public ActionResult Intern(int number = 1)
        {
            var result = internManager.GetAll().OrderByDescending(x => x.Id).ToPagedList(number, 1);
            return View(result);
        }

        [HttpPost]
        public ActionResult Intern(Intern ıntern)
        {
            ValidationResult result = internValidator.Validate(ıntern);
            if (result.IsValid)
            {
                internManager.Add(ıntern);
                return RedirectToAction("Intern");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }
    }
}