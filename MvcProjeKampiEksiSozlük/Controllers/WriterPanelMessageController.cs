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
    public class WriterPanelMessageController : Controller
    {
        // GET: WriterPanelMessage
        MessageManager messageManager = new MessageManager(new EfMessageDal());
        MessageValidator messageValidator = new MessageValidator();
        ContactManager contactManager = new ContactManager(new EfContactDal());
        public ActionResult Inbox()
        {
            string parameter = (string)Session["WriterMail"];
            var messagelist = messageManager.GetListInbox(parameter);
            return View(messagelist);
        }

        public ActionResult Sendbox()
        {
            string parameter = (string)Session["WriterMail"];
            var messageList = messageManager.GetListSendbox(parameter);
            return View(messageList);
        }
        public PartialViewResult MessagePartial()
        {
            string parameter = (string)Session["WriterMail"];
            var contacts = contactManager.GetList().Count();
            ViewBag.contact = contacts;

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

        public ActionResult GetInBoxMessageDetails(int id)
        {
            var values = messageManager.GetByID(id);
            return View(values);
        }

        public ActionResult GetSendBoxMessageDetails(int id)
        {
            var values = messageManager.GetByID(id);
            return View(values);
        }

        [HttpGet]
        public ActionResult NewMessage()
        {
            return View();
        }

        [HttpPost] //Burası refaktor edilecek şuanlık bu şekide..
        public ActionResult NewMessage(Message message, string parameter)
        {
            string sender = (string)Session["WriterMail"];
            ValidationResult results = messageValidator.Validate(message);
            if (parameter == "send")
            {
                if (results.IsValid)
                {
                    message.SenderMail = sender;
                    message.IsDraft = false;
                    message.MessageDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                    messageManager.MessageAddBL(message);
                    Thread.Sleep(1500);
                    return RedirectToAction("Sendbox");
                }
                else
                {
                    foreach (var item in results.Errors)
                    {
                        ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                    }
                }
            }

            else if (parameter == "draft")
            {
                if (results.IsValid)
                {
                    message.SenderMail = sender;
                    message.IsDraft = true;
                    message.MessageDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                    messageManager.MessageAddBL(message);
                    Thread.Sleep(1500);
                    return RedirectToAction("Draft");
                }
                else
                {
                    foreach (var item in results.Errors)
                    {
                        ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                    }
                }
            }

            return View();
        }

        public ActionResult UnReadMessagePanel()
        {
            string parameter = (string)Session["WriterMail"];
            var unReadMessage = messageManager.GetAllUnRead(parameter);
            return View(unReadMessage);
        }

        public ActionResult IsRead(int id)
        {
            var result = messageManager.GetByID(id);
            if (result.IsRead == false)
            {
                result.IsRead = true;
            }
            messageManager.MessageUpdate(result);
            return RedirectToAction("ReadMessage");
        }

        public ActionResult ReadMessagePanel()
        {
            string parameter = (string)Session["WriterMail"];
            var readMessage = messageManager.GetAllRead(parameter).Where(x => x.IsRead == true).ToList();
            return View(readMessage);
        }

        public ActionResult Draft()
        {
            string parameter = (string)Session["WriterMail"];
            var result = messageManager.GetAllDraft(parameter).Where(x => x.IsDraft == true).ToList();
            return View(result);
        }

    }
}