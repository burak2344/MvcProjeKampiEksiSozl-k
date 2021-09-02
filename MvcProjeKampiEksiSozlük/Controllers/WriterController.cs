﻿using BusinessLayer.Concrete;
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
    public class WriterController : Controller
    {
        WriterManager writerManager = new WriterManager(new EfWriterDal());
        WriterValidator writerValidator = new WriterValidator();
        public ActionResult Index()
        {
            var WriterValues = writerManager.GetList();
            return View(WriterValues);
        }
        [HttpGet]
        public ActionResult AddWriter()
		{
            return View();
		}

        [HttpPost]
        public ActionResult AddWriter(Writer p)
        {
            ValidationResult result = writerValidator.Validate(p);
			if (result.IsValid)
			{
                p.WriterRole = "C";
                writerManager.WriterAdd(p);
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
        [HttpGet]
        public ActionResult EditWriter(int id)
		{
            var writervalue = writerManager.GetByID(id);
            return View(writervalue);
		}
        [HttpPost]
        public ActionResult EditWriter(Writer p)
        {
            ValidationResult result = writerValidator.Validate(p);
            if (result.IsValid)
            {
                p.WriterRole = "C";
                writerManager.WriterUpdate(p);
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