using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampiEksiSozlük.Controllers
{
    public class ContentController : Controller
    {
        // GET: Content

  //      IContentService _contentService;

		//public ContentController(IContentService contentService)
		//{
		//	_contentService = contentService;
		//}

		ContentManager contentManager = new ContentManager(new EfContentDal());
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ContentByHeading(int id)
        {
            var contentvalues = contentManager.GetListByHeadingID(id);
            return View(contentvalues);
        }

        public ActionResult GetAllContent(string parameter)
        {
            var values = contentManager.GetAll(parameter);
            return View(values);
        }
    }
}