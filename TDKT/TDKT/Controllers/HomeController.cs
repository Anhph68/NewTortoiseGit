using DotNet.Highcharts;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDKT.Models;

namespace TDKT.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        TDKTEntities db = new TDKTEntities();

        public ActionResult Index()
        {
            Session["Url"] = Request.RawUrl;
            return View();
        }

        public ActionResult Chart()
        {
            return PartialView();
        }

        [HttpGet]
        public ActionResult chooseYear()
        {
            return PartialView(db.getYears());
        }

        [HttpGet]
        public ActionResult AjaxHandler(string key)
        {
            var allResult = db.getYears().ToList();

            IEnumerable<getYears_Result> filteredResult;
            //Check whether the companies should be filtered by keyword
            if (!string.IsNullOrEmpty(key))
            {
                filteredResult = allResult
                    .Where(c => c.namkt.ToLower().Contains(key.ToLower()));
            }
            else filteredResult = allResult;

            return Json(filteredResult, JsonRequestBehavior.AllowGet);
        }

        [HttpPost, ActionName("Year")]
        [ValidateAntiForgeryToken]
        public ActionResult chooseYear(string year)
        {
            Session["year"] = year;

            return Redirect(Session["Url"].ToString());
        }

        [HttpPost]
        public ActionResult chooseDate(string key, string code)
        {

            if (code == HttpContext.Session.SessionID)
            {
                Session["date"] = key;
                return null;
            }

            return HttpNotFound();
        }
    }
}