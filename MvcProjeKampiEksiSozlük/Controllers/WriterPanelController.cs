using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampiEksiSozlük.Controllers
{
    public class WriterPanelController : Controller
    {
        HeadingManager headingManager = new HeadingManager(new EfHeadingDal());
        CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());
        WriterManager writerManager = new WriterManager(new EfWriterDal());
        WriterValidator writerValidator = new WriterValidator();
        Context context = new Context();
        // GET: WriterPanel
        [HttpGet]
        public ActionResult WriterProfile(int id = 0)
        {
            string parameter = (string)Session["WriterMail"];
            //var writerValue = context.Writers.FirstOrDefault(x => x.WriterMail == parameter);

            id = context.Writers.Where(x => x.WriterMail == parameter).Select(z => z.WriterId).FirstOrDefault();
            var writerValue = writerManager.GetByID(id);

            var writerName = context.Writers.Where(x => x.WriterMail == parameter).Select(z => z.WriterName + " " + z.WriterSurnam).FirstOrDefault();
            ViewBag.writerName = writerName;

            var writerImage = context.Writers.Where(x => x.WriterMail == parameter).Select(z => z.WriterImage).FirstOrDefault();
            ViewBag.writerImage = writerImage;

            var writerMail = context.Writers.Where(x => x.WriterMail == parameter).Select(z => z.WriterMail).FirstOrDefault();
            ViewBag.writerMail = writerMail;

            var writerAbout = context.Writers.Where(x => x.WriterMail == parameter).Select(z => z.WriterAbout).FirstOrDefault();
            ViewBag.writerAbout = writerAbout;

            var writerTitle = context.Writers.Where(x => x.WriterMail == parameter).Select(z => z.WriterTitle).FirstOrDefault();
            ViewBag.writerTitle = writerTitle;

            var writerId = context.Writers.Where(x => x.WriterMail == parameter).Select(z => z.WriterId).FirstOrDefault();
            var writerTitles = context.Contents.Where(x => x.WriterId == writerId).Count();
            ViewBag.writerTitles = writerTitles;

            var writerMessage = context.Messages.Where(x => x.ReceiverMail == parameter).Count();
            ViewBag.writerMessage = writerMessage;

            return View(writerValue);
        }

        [HttpPost]
        public ActionResult WriterProfile(Writer writer)
        {
            ValidationResult results = writerValidator.Validate(writer);
            if (results.IsValid)
            {
                writerManager.UpdateWriterPanel(writer);
                return RedirectToAction("WriterProfile");
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

        [HttpGet]
        public ActionResult WriterProfilePassword(int id = 0)
        {
            string parameter = (string)Session["WriterMail"];
            id = context.Writers.Where(x => x.WriterMail == parameter).Select(z => z.WriterId).FirstOrDefault();
            var writerValue = writerManager.GetByID(id);
            return View(writerValue);
        }

        [HttpPost]
        public ActionResult WriterProfilePassword(Writer writer)
        {
            writerManager.UpdatePasswordWriterPanel(writer);
            return RedirectToAction("WriterProfile");
        }

        public ActionResult MyHeading(string parameter)
        {
            parameter = (string)Session["WriterMail"];
            var writerIdInfo = context.Writers.Where(x => x.WriterMail == parameter).Select(z => z.WriterId).FirstOrDefault();

            var values = headingManager.GetAllByWriter(writerIdInfo);
            return View(values);
        }
        [HttpGet]
        public ActionResult NewHeading()
		{
            List<SelectListItem> valuecategory = (from x in categoryManager.GetList()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.CategoryName,
                                                      Value = x.CategoryId.ToString()
                                                  }).ToList();
            ViewBag.vlc = valuecategory;
            return View();
		}

        [HttpPost]
        public ActionResult NewHeading(Heading heading)
        {
            string result = (string)Session["WriterMail"];
            var writerIdInfo = context.Writers.Where(x => x.WriterMail == result).Select(z => z.WriterId).FirstOrDefault();
            heading.HeadingDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            heading.HeadingStatus = true;
            heading.IsWriterHeading = true;
            heading.WriterId = writerIdInfo;
            headingManager.HeadingAddBL(heading);
            Thread.Sleep(1500);
            return RedirectToAction("MyHeading");
        }


        [HttpGet]
        public ActionResult UpdateHeading(int id)
        {
            List<SelectListItem> valueCategory = (from category in categoryManager.GetList()
                                                  select new SelectListItem
                                                  {
                                                      Text = category.CategoryName,
                                                      Value = category.CategoryId.ToString()
                                                  }).ToList();
            ViewBag.category = valueCategory;

            var headingValues = headingManager.GetByID(id);
            return View(headingValues);
        }

        [HttpPost]
        public ActionResult UpdateHeading(Heading heading)
        {
            headingManager.HeadingUpdate(heading);
            return RedirectToAction("MyHeading");
        }

        public ActionResult DeleteHeading(int id)
        {
            var headingValue = headingManager.GetByID(id);

            if (headingValue.IsWriterHeading == true)
            {
                headingValue.IsWriterHeading = false;
            }
            else
            {
                headingValue.IsWriterHeading = true;
            }

            headingManager.HeadingDelete(headingValue);
            return RedirectToAction("MyHeading");
        }

        public ActionResult AllHeading(int page = 1)
        {
            var headings = headingManager.GetList().ToPagedList(page, 5);
            return View(headings);
        }
    }
}