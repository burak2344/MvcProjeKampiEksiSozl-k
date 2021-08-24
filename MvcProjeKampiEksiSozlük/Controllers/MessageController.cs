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
    public class MessageController : Controller
    {
        // GET: Message
        MessageManager messageManager = new MessageManager(new EfMessageDal());
        MessageValidator messageValidator = new MessageValidator();
        public ActionResult Inbox()
        {
            string parameter = (string)Session["AdminUserName"];
            var messagelist = messageManager.GetListInbox(parameter);
            return View(messagelist);
        }

        public ActionResult Sendbox()
        {
            string parameter = (string)Session["AdminUserName"];
            var messagelist = messageManager.GetListSendbox(parameter);
            return View(messagelist);
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

        [HttpPost]
        public ActionResult NewMessage(Message message, string parameter)
        {
            ValidationResult results = messageValidator.Validate(message);
            string adminValue = (string)Session["AdminUserName"];
            if (parameter == "send")
            {
                if (results.IsValid)
                {
                    message.SenderMail = adminValue;
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
                    message.SenderMail = adminValue;
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

        public ActionResult Draft()
        {
            string parameter = (string)Session["AdminUserName"];
            var result = messageManager.GetAllDraft(parameter).Where(x => x.IsDraft == true).ToList();
            return View(result);
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

        public ActionResult ReadMessage()
        {
            string parameter = (string)Session["AdminUserName"];
            var readMessage = messageManager.GetAllRead(parameter).Where(x => x.IsRead == true).ToList();
            return View(readMessage);
        }

        public ActionResult UnReadMessage()
        {
            string parameter = (string)Session["AdminUserName"];
            var unReadMessage = messageManager.GetAllUnRead(parameter);
            return View(unReadMessage);
        }
    }
}