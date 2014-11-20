using DotNet.Highcharts;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDKT.Models;
using System.Drawing;
using DotNet.Highcharts.Enums;
using System.Net;
using Microsoft.Owin.Security;

namespace TDKT.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        TDKTEntities td = new TDKTEntities();

        public ActionResult Index()
        {
            Session["Url"] = Request.RawUrl;
            if (Session["year"] == null)
            {
                HttpContext.GetOwinContext().Authentication.SignOut();
                return RedirectToAction("Login", "Account");
            }

            ViewBag.TrienKhai = td.getTrienKhai(Session["year"].ToString(), DateTime.Parse(Session["date"].ToString()), Session["donvi"] == null ? "" : Session["donvi"].ToString()).FirstOrDefault();

            string[] categories = new[] { "Đã kết thúc", "Đã trình duyệt BCKT", "Đã xét duyệt BCKT", "Đơn vị đã trình PHBCKT", "Vụ TH đã trình PHBCKT", "Đã phát hành BCKT" };

            ChartsModel model = new ChartsModel();
            model.Charts = new List<Highcharts>();
            var tmp2 = td.getPhatHanh(Session["year"].ToString(), DateTime.Parse(Session["date"].ToString()), Session["donvi"] == null ? "" : Session["donvi"].ToString()).FirstOrDefault();
            Series[] colData = new[] {
                    new Series { Name = "", Data = new Data(new object[] { tmp2.dakt, tmp2.datrinhbc, tmp2.daduyetbc, tmp2.dvtrinhph, tmp2.thtrinhph, tmp2.ktnnph }) }
                };
            ChartsController c = new ChartsController();
            model.Charts.Add(c.ColChart1("chart", categories, colData).SetOptions(new GlobalOptions
            {
                Colors = new System.Drawing.Color[] { Color.FromArgb(92, 184, 92) }
            }));

            return View(model);
        }

        public ActionResult Chart()
        {
            return PartialView();
        }

        [HttpGet]
        public ActionResult chooseYear()
        {
            return PartialView(td.getYears());
        }

        [HttpGet]
        public ActionResult AjaxHandler(string key)
        {
            var allResult = td.getYears().ToList();

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
            if (!String.IsNullOrEmpty(year))
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