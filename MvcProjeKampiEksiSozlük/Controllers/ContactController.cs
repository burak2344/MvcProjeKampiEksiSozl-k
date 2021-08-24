using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampiEksiSozlük.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        ContactManager contactManager = new ContactManager(new EfContactDal());
        ContactValidator contactValidator = new ContactValidator();
        MessageManager messageManager = new MessageManager(new EfMessageDal());
        public ActionResult Index()
        {
            var contactvalues = contactManager.GetList();
            return View(contactvalues);
        }


        public ActionResult GetContactDetails(int id)
		{
            var contactvalues = contactManager.GetByID(id);
            return View(contactvalues);
		}

        public PartialViewResult MessageListMenu()
		{
            string parameter = (string)Session["AdminUserName"];
            var contact = contactManager.GetList().Count();
            ViewBag.contact = contact;

            var result = messageManager.GetListSendbox(parameter).Count();
            ViewBag.result = result;

            var result2 = messageManager.GetListInbox(parameter).Count();
            ViewBag.result2 = result2;

            var draft = messageManager.GetAllDraft(parameter).Where(x => x.IsDraft == true).Count();
            ViewBag.draft = draft;

            var readMessage = messageManager.GetAllRead(parameter).Where(x => x.IsRead == true).Count();
            ViewBag.readMessage = readMessage;

            var unReadMessage = messageManager.GetAllUnRead(parameter).Count();
            ViewBag.unReadMessage = unReadMessage;


            return PartialView();
		}

    }
}