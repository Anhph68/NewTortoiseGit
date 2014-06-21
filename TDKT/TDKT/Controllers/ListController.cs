using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TDKT.Models;

namespace TDKT.Controllers
{
    [Authorize]
    public class ListController : Controller
    {

        private TDKTEntities db = new TDKTEntities();

        // GET: List
        public ActionResult Index()
        {
            ViewBag.Donvi = new SelectList(db.getDonViDo(), "MA", "TEN");
            return View();
        }

        public ActionResult AjaxHandler(jQueryDataTableParamModel param)
        {
            var allResult = db.getCuoc(param.Year, string.IsNullOrEmpty(param.Donvi) ? "" : param.Donvi).ToList();

            IEnumerable<getCuoc_Result> filteredResult;
            //Check whether the companies should be filtered by keyword
            if (!string.IsNullOrEmpty(param.sSearch))
            {
                //Optionally check whether the columns are searchable at all 
                var Searchable_0 = Convert.ToBoolean(Request["bSearchable_0"]);
                var Searchable_1 = Convert.ToBoolean(Request["bSearchable_1"]);
                var Searchable_2 = Convert.ToBoolean(Request["bSearchable_2"]);
                var Searchable_3 = Convert.ToBoolean(Request["bSearchable_3"]);
                int tmp = int.TryParse(param.sSearch, out tmp) ? tmp : 0;

                filteredResult = allResult
                   .Where(c => Searchable_1 && c.TenCuoc.ToLower().Contains(param.sSearch.ToLower())
                            || Searchable_2 && c.DonVi.ToLower().Equals(param.sSearch.ToLower())
                            || Searchable_3 && c.SoQuyetDinh.ToLower().Contains(param.sSearch.ToLower())
                            || Searchable_0 && c.STT.Equals(tmp)
                        );
            }
            else filteredResult = allResult;

            var Sortable_0 = Convert.ToBoolean(Request["bSortable_0"]);
            var Sortable_1 = Convert.ToBoolean(Request["bSortable_1"]);
            var Sortable_2 = Convert.ToBoolean(Request["bSortable_2"]);
            var Sortable_3 = Convert.ToBoolean(Request["bSortable_3"]);
            var sortColumnIndex = Convert.ToInt64(Request["iSortCol_0"]);
            Func<getCuoc_Result, string> orderingFunction = (c => sortColumnIndex == 1 && Sortable_1 ? c.TenCuoc :
                                                            sortColumnIndex == 2 && Sortable_2 ? c.DonVi :
                                                            sortColumnIndex == 3 && Sortable_3 ? c.SoQuyetDinh :
                                                            "");
            Func<getCuoc_Result, Int64> orderingFunction2 = (c => sortColumnIndex == 0 && Sortable_0 ? c.STT : 0);

            var sortDirection = Request["sSortDir_0"]; // asc or desc
            if (sortDirection == "asc")
                filteredResult = filteredResult.OrderBy(orderingFunction).ThenBy(orderingFunction2);
            else
                filteredResult = filteredResult.OrderByDescending(orderingFunction).ThenByDescending(orderingFunction2);

            var displayed = filteredResult.Skip(param.iDisplayStart).Take(param.iDisplayLength);
            var result = displayed.Select(c => new
                             {
                                 STT = c.STT,
                                 MaCuoc = c.MaCuoc,
                                 TenCuoc = c.TenCuoc,
                                 DonVi = c.DonVi,
                                 SoQuyetDinh = c.SoQuyetDinh,
                                 NgayKyQD = c.NgayKyQD
                             });
            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = allResult.Count(),
                iTotalDisplayRecords = filteredResult.Count(),
                aaData = result
            }, JsonRequestBehavior.AllowGet);

        }

        [Authorize()]
        public ActionResult Create()
        {
            ViewBag.Donvi = new SelectList(db.getDonVi(true, true), "MaDonVi", "TenDonVi");

            ViewBag.LinhVuc = new SelectList(db.TD_LVKT, "MA", "TEN");

            ViewBag.LoaiHinh = new SelectList(db.TD_LHKT, "MA", "TEN");

            return PartialView();
        }

        /// <summary>
        /// Confirm Create
        /// </summary>
        /// <param name="cuoc"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CUOC_KT cuoc)
        {
            if (Request.IsAjaxRequest())
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        cuoc.MA_CUOC = db.genCode(cuoc.NAM_KT, cuoc.MA_DVKT, cuoc.MA_LVKT, cuoc.MA_LHKT).SingleOrDefault().ToString();
                        db.CUOC_KT.Add(cuoc);
                        db.SaveChanges();

                        return Json("Cập nhật thành công!", JsonRequestBehavior.AllowGet);
                    }
                    catch (Exception ex)
                    {
                        return Json("Có lỗi" + ex, JsonRequestBehavior.AllowGet);
                    }

                }
            }

            return Json("Có lỗi", JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var filtered = db.CUOC_KT.SingleOrDefault(c => c.MA_CUOC == key);

            if (filtered == null)
            {
                return HttpNotFound();
            }

            ViewBag.DonVi = db.TD_DVKT.SingleOrDefault(d => d.MA == filtered.MA_DVKT).TEN;

            return PartialView(filtered);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmDelete(CUOC_KT cuoc)
        {
            if (Request.IsAjaxRequest())
            {
                var result = db.CUOC_KT.SingleOrDefault(c => c.MA_CUOC == cuoc.MA_CUOC);
                if (result != null)
                {
                    try
                    {
                        db.CUOC_KT.Remove(result);
                        db.SaveChanges();

                        return Json("Đã xóa!", JsonRequestBehavior.AllowGet);
                    }
                    catch (Exception ex)
                    {
                        return Json("Có lỗi" + ex, JsonRequestBehavior.AllowGet);
                    }
                }
            }

            return Json("Có lỗi", JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var filtered = db.CUOC_KT.SingleOrDefault(c => c.MA_CUOC == key);

            if (filtered == null)
            {
                return HttpNotFound();
            }

            ViewBag.Donvi = new SelectList(db.TD_DVKT.ToList(), "MA", "TEN");

            ViewBag.LinhVuc = new SelectList(db.TD_LVKT.ToList(), "MA", "TEN");

            ViewBag.LoaiHinh = new SelectList(db.TD_LHKT.ToList(), "MA", "TEN");

            return PartialView(filtered);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CUOC_KT cuoc)
        {
            if (Request.IsAjaxRequest())
            {
                if (ModelState.IsValid)
                {
                    db.Entry(cuoc).State = EntityState.Modified;
                    db.SaveChanges();

                    return Json("Đã sửa!", JsonRequestBehavior.AllowGet);
                }
            }

            return Json("Có lỗi!", JsonRequestBehavior.AllowGet);
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