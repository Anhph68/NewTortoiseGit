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
            return View();
        }

        public ActionResult AjaxHandler(jQueryDataTableParamModel param)
        {

            var allResult = db.getCuoc(param.Year).ToList();

            IEnumerable<getCuoc_Result> filteredResult;
            //Check whether the companies should be filtered by keyword
            if (!string.IsNullOrEmpty(param.sSearch))
            {
                //Optionally check whether the columns are searchable at all 
                var isSTTSearchable = Convert.ToBoolean(Request["bSearchable_0"]);
                var isTenCuocSearchable = Convert.ToBoolean(Request["bSearchable_1"]);
                var isDonViSearchable = Convert.ToBoolean(Request["bSearchable_2"]);
                var isSoQDSearchable = Convert.ToBoolean(Request["bSearchable_3"]);
                int tmp = int.TryParse(param.sSearch, out tmp) ? tmp : 0;

                filteredResult = allResult
                   .Where(c => isTenCuocSearchable && c.TenCuoc.ToLower().Contains(param.sSearch.ToLower())
                            || isDonViSearchable && c.DonVi.ToLower().Equals(param.sSearch.ToLower())
                            || isSoQDSearchable && c.SoQuyetDinh.ToLower().Contains(param.sSearch.ToLower())
                            || isSTTSearchable && c.STT.Equals(tmp)
                        );
            }
            else
            {
                filteredResult = allResult;
            }

            var isSTTSortable = Convert.ToBoolean(Request["bSortable_0"]);
            var isTenCuocSortable = Convert.ToBoolean(Request["bSortable_1"]);
            var isDonviSortable = Convert.ToBoolean(Request["bSortable_2"]);
            var isSoQDSortable = Convert.ToBoolean(Request["bSortable_3"]);
            var sortColumnIndex = Convert.ToInt64(Request["iSortCol_0"]);
            Func<getCuoc_Result, string> orderingFunction = (c => sortColumnIndex == 1 && isTenCuocSortable ? c.TenCuoc :
                                                            sortColumnIndex == 2 && isDonviSortable ? c.DonVi :
                                                            sortColumnIndex == 3 && isSoQDSortable ? c.SoQuyetDinh :
                                                            "");
            Func<getCuoc_Result, Int64> orderingFunction2 = (c => sortColumnIndex == 0 && isSTTSortable ? c.STT : 0);

            var sortDirection = Request["sSortDir_0"]; // asc or desc
            if (sortDirection == "asc")
                filteredResult = filteredResult.OrderBy(orderingFunction).ThenBy(orderingFunction2);
            else
                filteredResult = filteredResult.OrderByDescending(orderingFunction).ThenByDescending(orderingFunction2);

            var displayedCuoc = filteredResult.Skip(param.iDisplayStart).Take(param.iDisplayLength);
            var result = from c in displayedCuoc
                         select new
                         {
                             STT = c.STT,
                             MaCuoc = c.MaCuoc,
                             TenCuoc = c.TenCuoc,
                             DonVi = c.DonVi,
                             SoQuyetDinh = c.SoQuyetDinh,
                             NgayKyQD = c.NgayKyQD
                         };
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
            ViewBag.Donvi = new SelectList(db.TD_DVKT.ToList(), "MA", "TEN");

            ViewBag.LinhVuc = new SelectList(db.TD_LVKT.ToList(), "MA", "TEN");

            ViewBag.LoaiHinh = new SelectList(db.TD_LHKT.ToList(), "MA", "TEN");

            return PartialView();
        }

        /// <summary>
        /// Confirm Create
        /// </summary>
        /// <param name="cuoc"></param>
        /// <returns></returns>
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public string Create(CUOC_KT cuoc)
        //{
        //    if (Request.IsAjaxRequest())
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            try
        //            {
        //                cuoc.MA_CUOC = db.genCode(cuoc.NAM_KT).SingleOrDefault().ToString();
        //                db.CUOC_KT.Add(cuoc);
        //                db.SaveChanges();

        //                return "Cập nhật thành công!";
        //            }
        //            catch (Exception ex)
        //            {
        //                return "Có lỗi" + ex;
        //            }

        //        }
        //    }

        //    return "Có lỗi!";
        //}

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
        public string ConfirmDelete(CUOC_KT cuoc)
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

                        return "Xóa thành công";
                    }
                    catch (Exception ex)
                    {
                        return "Có lỗi" + ex;
                    }
                }
            }

            return "Có lỗi";
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
        public string Edit(CUOC_KT cuoc)
        {
            if (Request.IsAjaxRequest())
            {
                if (ModelState.IsValid)
                {
                    db.Entry(cuoc).State = EntityState.Modified;
                    db.SaveChanges();

                    return "Cập nhật thành công!";
                }
            }

            return "Có lỗi!";
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