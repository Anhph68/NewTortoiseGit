using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using THKQKT.Models;

namespace THKQKT.Controllers
{
    [Authorize]
    public class SoLieuController : Controller
    {

        private THKQKTEntities db = new THKQKTEntities();

        // GET: List
        public ActionResult Index()
        {
            Session["Url"] = Request.RawUrl;
            if (Session["year"] == null)
            {
                HttpContext.GetOwinContext().Authentication.SignOut();
                return RedirectToAction("Login", "Account");
            }
            ViewBag.Donvi = new SelectList(db.getDonViDo(Session["year"].ToString()), "MA", "TEN");
            ViewBag.LinhVuc = new SelectList(db.getLinhVuc(), "TEN", "TEN");
            return View();
        }

        public ActionResult AjaxHandler(jQueryDataTableParamModel param)
        {
            IEnumerable<getCuocStatus_thkqkt_Result> allResult = db.getCuocStatus_thkqkt(param.Year, DateTime.Parse(Session["date"].ToString()), string.IsNullOrEmpty(param.Status) ? "" : param.Status, (Session["donvi"] != null) ? Session["donvi"].ToString() : "");
            var filterDonVi = string.IsNullOrEmpty(param.Donvi) ? "" : param.Donvi;
            var filterLinhVuc = string.IsNullOrEmpty(param.LinhVuc) ? "" : param.LinhVuc;
            allResult = (filterDonVi == "") ? allResult.ToList() : allResult.Where(r => r.MaDonVi == filterDonVi).ToList();
            allResult = (filterLinhVuc == "") ? allResult.ToList() : allResult.Where(r => r.linhvuc == filterLinhVuc).ToList();
            IEnumerable<getCuocStatus_thkqkt_Result> filteredResult;
            //Check whether the companies should be filtered by keyword
            if (!string.IsNullOrEmpty(param.sSearch))
            {
                //Optionally check whether the columns are searchable at all 
                var Searchable_0 = Convert.ToBoolean(Request["bSearchable_0"]);
                var Searchable_2 = Convert.ToBoolean(Request["bSearchable_2"]);
                var Searchable_3 = Convert.ToBoolean(Request["bSearchable_3"]);
                var Searchable_4 = Convert.ToBoolean(Request["bSearchable_4"]);
                var Searchable_5 = Convert.ToBoolean(Request["bSearchable_5"]);
                int tmp = int.TryParse(param.sSearch, out tmp) ? tmp : 0;

                filteredResult = allResult
                    .Where(c => Searchable_2 && c.TenCuoc.ToLower().Contains(param.sSearch.ToLower())
                             || Searchable_3 && c.DonVi.ToLower().Contains(param.sSearch.ToLower())
                             || Searchable_4 && c.SoQuyetDinh.ToLower().Contains(param.sSearch.ToLower())
                             || Searchable_5 && c.linhvuc.ToLower().Contains(param.sSearch.ToLower())
                             || Searchable_0 && c.STT.Equals(tmp)
                     );
            }
            else filteredResult = allResult;

            var Sortable_0 = Convert.ToBoolean(Request["bSortable_0"]);
            var Sortable_2 = Convert.ToBoolean(Request["bSortable_2"]);
            var Sortable_3 = Convert.ToBoolean(Request["bSortable_3"]);
            var Sortable_4 = Convert.ToBoolean(Request["bSortable_4"]);
            var Sortable_5 = Convert.ToBoolean(Request["bSortable_5"]);
            var sortColumnIndex = Convert.ToInt64(Request["iSortCol_0"]);
            Func<getCuocStatus_thkqkt_Result, string> orderingFunction = (c => sortColumnIndex == 2 && Sortable_2 ? c.TenCuoc :
                                                            sortColumnIndex == 3 && Sortable_3 ? c.DonVi :
                                                            sortColumnIndex == 4 && Sortable_4 ? c.SoQuyetDinh :
                                                            sortColumnIndex == 5 && Sortable_5 ? c.linhvuc :
                                                            "");
            Func<getCuocStatus_thkqkt_Result, Int64> orderingFunction2 = (c => sortColumnIndex == 0 && Sortable_0 ? c.STT : 0);

            var sortDirection = Request["sSortDir_0"]; // asc or desc
            if (sortDirection == "asc")
                filteredResult = filteredResult.OrderBy(orderingFunction).ThenBy(orderingFunction2);
            else
                filteredResult = filteredResult.OrderByDescending(orderingFunction).ThenByDescending(orderingFunction2);

            var displayed = filteredResult.Skip(param.iDisplayStart).Take(param.iDisplayLength);
            var result = displayed.Select(c => new
                             {
                                 col0 = c.STT,
                                 col1 = c.ID,
                                 col2 = c.TenCuoc,
                                 col3 = c.DonVi,
                                 col4 = c.SoQuyetDinh,
                                 col5 = c.linhvuc
                             });
            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = allResult.Count(),
                iTotalDisplayRecords = filteredResult.Count(),
                aaData = result
            }, JsonRequestBehavior.AllowGet);

        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}