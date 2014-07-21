using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TDKT.Controllers
{
    [Authorize]
    public class ReportController : Controller
    {
        // GET: Report
        public ActionResult Danh_Muc()
        {
            Session["Url"] = Request.RawUrl;
            return View();
        }

        public ActionResult Phu_Bieu_1()
        {
            Session["Url"] = Request.RawUrl;
            return View();
        }

        public ActionResult Phu_Bieu_2()
        {
            Session["Url"] = Request.RawUrl;
            return View();
        }

        public ActionResult Phu_Bieu_3()
        {
            Session["Url"] = Request.RawUrl;
            return View();
        }
    }
}