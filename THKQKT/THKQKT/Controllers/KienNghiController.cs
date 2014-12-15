using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using THKQKT.Models;

namespace THKQKT.Controllers
{
    public class KienNghiController : Controller
    {
        private THKQKTEntities db = new THKQKTEntities();
        // GET: KienNghi
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

        /// <summary>
        /// Hiển thị danh sách chỉ tiêu của cuộc kiểm toán
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ChiTieuKienNghi(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("Index", "Home");
            }

            getCuocByID_Result cuoc = db.getCuocByID(id).FirstOrDefault();
            Session["CuocID"] = cuoc.ID;
            Session["NgayThucHien"] = cuoc.NgayBatDauThucHien;

            return View(cuoc);
        }

        /// <summary>
        /// get danh sách các chỉ tiêu kiến nghị
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ActionResult getKienNghiList(jQueryDataTableParamModel param)
        {
            if (Session["CuocID"] == null || Session["NgayThucHien"] == null)
            {
                HttpContext.GetOwinContext().Authentication.SignOut();
                return RedirectToAction("Login", "Account");
            }

            IEnumerable<sp_TongHopKetQua_List_Result> allResult = db.sp_TongHopKetQua_List(int.Parse(Session["CuocID"].ToString()), DateTime.Parse(Session["NgayThucHien"].ToString())).ToList();
            var tmpCount = allResult.Count();

            if (!string.IsNullOrEmpty(param.sSearch))
            {
                //Optionally check whether the columns are searchable at all 
                var Searchable_0 = Convert.ToBoolean(Request["bSearchable_0"]);
                int tmp = int.TryParse(param.sSearch, out tmp) ? tmp : 0;

                allResult = allResult
                    .Where(c => Searchable_0 && c.TenChiTieuMoi.ToLower().Contains(param.sSearch.ToLower()));
            }
            //else filteredResult = allResult;

            //var displayed = filteredResult.Skip(param.iDisplayStart).Take(param.iDisplayLength);
            var result = allResult.Select(c => new
            {
                col0 = c.TenChiTieuMoi,
                col1 = c.SoTien,
                col2 = c.isCongZon,
                col3 = c.MaChiTieu
            });

            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = tmpCount,
                iTotalDisplayRecords = allResult.Count(),
                aaData = result
            }, JsonRequestBehavior.AllowGet);
        }

    }
}