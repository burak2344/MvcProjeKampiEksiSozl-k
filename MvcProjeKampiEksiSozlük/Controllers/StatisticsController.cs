﻿using DataAccessLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampiEksiSozlük.Controllers
{
    public class StatisticsController : Controller
    {
        // GET: Statistics
        Context context = new Context();
        public ActionResult Index()
        {
            var result = context.Categories.Count().ToString();
            ViewBag.result = result;

            var result2 = context.Headings.Count(h => h.CategoryId == 13).ToString();
            ViewBag.result2 = result2;

            var result3 = context.Writers.Where(w => w.WriterName.Contains("a") || w.WriterName.Contains("A")).Count();
            ViewBag.result3 = result3;

            var result4 = context.Categories.Where(u => u.CategoryId == context.Headings.GroupBy(x => x.CategoryId).OrderByDescending(x => x.Count())
                .Select(x => x.Key).FirstOrDefault()).Select(x => x.CategoryName).FirstOrDefault();
            ViewBag.result4 = result4;

            var result5 = context.Categories.Where(c => c.CategoryStatus == true).Count() -
                          context.Categories.Where(c => c.CategoryStatus == false).Count();
            ViewBag.result5 = result5;


            return View();
        }
    }
}