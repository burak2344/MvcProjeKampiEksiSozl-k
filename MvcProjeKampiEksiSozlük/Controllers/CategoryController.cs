using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace MvcProjeKampiEksiSozlük.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetCategoryList()
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
            //categoryManager.CategoryAddBL(p);
            CategoryValidator categoryValidator = new CategoryValidator();
            ValidationResult results = categoryValidator.Validate(p);
			if (results.IsValid)
			{
                categoryManager.CategoryAddBL(p);
                return RedirectToAction("GetCategoryList");
            }
			else
			{
				foreach (var item in results.Errors)
				{
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
				}
			}
            return View();
		}
    }
}