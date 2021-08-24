using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampiEksiSozlük.Controllers
{
    public class TalentCardController : Controller
    {
        // GET: TalentCard
        TalentCardManager talentCardManager = new TalentCardManager(new EfTalentCardDal());
        public ActionResult Index()
        {
            var cardValues = talentCardManager.GetList();
            return View(cardValues);
        }

        [HttpGet]
        public ActionResult AddCard()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCard(TalentCard talentCard)
        {
            talentCardManager.TalentCardAddBL(talentCard);
            Thread.Sleep(2500);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult UpdateCard(int id)
        {
            var cardValues = talentCardManager.GetByID(id);
            return View(cardValues);
        }

        [HttpPost]
        public ActionResult UpdateCard(TalentCard talentCard)
        {
            talentCardManager.TalentCardUpdate(talentCard);
            return RedirectToAction("Index");
        }
    }
}