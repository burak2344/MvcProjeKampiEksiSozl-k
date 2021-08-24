using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampiEksiSozlük.Controllers
{
    public class AdminCategoryController : Controller
    {
        CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());
        CategoryValidator categoryValidator = new CategoryValidator();

        //[Authorize(Roles="A,B")]
        public ActionResult Index()
        {
            var categoryvalues = categoryManager.GetList();
            return View(categoryvalues);
        }

        [HttpGet]
        public ActionResult AddCategory()
		{
            return View();
		}

        [HttpPost]
        public ActionResult AddCategory(Category p)
        {
           
            ValidationResult result = categoryValidator.Validate(p);
			if (result.IsValid)
			{
                categoryManager.CategoryAddBL(p);
                Thread.Sleep(1500);
                return RedirectToAction("Index");
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

        public ActionResult DeleteCategory(int id)
		{
            var categoryvalue = categoryManager.GetByID(id);
            categoryManager.CategoryDelete(categoryvalue);
            return RedirectToAction("Index");
		}

        [HttpGet]
        public ActionResult EditCategory(int id)
		{
            var categoryvalue = categoryManager.GetByID(id);
            return View(categoryvalue);
        }
        [HttpPost]
        public ActionResult EditCategory(Category p)
        {
            ValidationResult result = categoryValidator.Validate(p);
            if (result.IsValid)
            {
                categoryManager.CategoryUpdate(p);
                return RedirectToAction("Index");
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