using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampiEksiSozlük.Controllers
{
    public class HeadingController : Controller
    {
        // GET: Heading
        HeadingManager headingManager = new HeadingManager(new EfHeadingDal());
        CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());
        WriterManager writerManager = new WriterManager(new EfWriterDal());
        public ActionResult Index(int page = 1)
        {
            var headingValues = headingManager.GetList().ToPagedList(page, 5);
            return View(headingValues);
        }
        public ActionResult HeadingReport()
        {
            var headingValues = headingManager.GetList();
            return View(headingValues);
        }
        [HttpGet]
        public ActionResult AddHeading()
		{
            List<SelectListItem> valuecategory = (from x in categoryManager.GetList()
                                                  select new SelectListItem
                                                  {
                                                    Text=x.CategoryName,
                                                    Value=x.CategoryId.ToString()
                                                  }).ToList();

            List<SelectListItem> valuewriter = (from x in writerManager.GetList()
                                                select new SelectListItem
                                                {
                                                    Text = x.WriterName + " "+x.WriterSurnam,
                                                    Value = x.WriterId.ToString()
                                                }).ToList();
            ViewBag.vlw = valuewriter;
            ViewBag.vlc = valuecategory;
            return View();
		}
        [HttpPost]
        public ActionResult AddHeading(Heading p)
		{
            p.HeadingDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            headingManager.HeadingAddBL(p);
            return RedirectToAction("Index");
		}
        [HttpGet]
        public ActionResult EditHeading(int id)
		{

            List<SelectListItem> valuecategory = (from x in categoryManager.GetList()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.CategoryName,
                                                      Value = x.CategoryId.ToString()
                                                  }).ToList();

            ViewBag.vlc = valuecategory;
            var HeadingValue = headingManager.GetByID(id);
            return View(HeadingValue);
		}
        [HttpPost]
        public ActionResult EditHeading(Heading p)
		{
            headingManager.HeadingUpdate(p);
            return RedirectToAction("Index");
		}
        public ActionResult DeleteHeading(int id)
		{
            var headingvalue = headingManager.GetByID(id);
            headingvalue.HeadingStatus = headingvalue.HeadingStatus == true ? false : true;
            headingManager.HeadingDelete(headingvalue); 
            return RedirectToAction("Index");
		}
    }
}