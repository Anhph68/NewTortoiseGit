using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TDKT.Models;

namespace TDKT.Controllers
{
    [Authorize]
    public class DanhSachController : Controller
    {

        private TDKTEntities db = new TDKTEntities();

        // GET: List
        public ActionResult Index()
        {
            Session["Url"] = Request.RawUrl;
            ViewBag.Donvi = new SelectList(db.getDonViDo(Session["year"].ToString()), "MA", "TEN");
            ViewBag.LinhVuc = new SelectList(db.getLinhVuc(), "TEN", "TEN");
            return View();
        }

        public ActionResult cuocPlus(string key)
        {
            if (string.IsNullOrEmpty(key))
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var d = db.getCuocPlus(key, DateTime.Parse(Session["date"].ToString())).FirstOrDefault();

            if (d == null)
            {
                return HttpNotFound();
            }
            return PartialView(d);
        }

        public ActionResult AjaxHandler(tblCuocKiemtoanParamModel param)
        {
            // Xử lý chỗ này

            IEnumerable<getCuocStatus_Result> allResult = db.getCuocStatus(param.Year, Session["date"] != null ? DateTime.Parse(Session["date"].ToString()) : DateTime.Today, string.IsNullOrEmpty(param.Status) ? "" : param.Status, (Session["donvi"] != null) ? Session["donvi"].ToString() : "");
            var filterDonVi = string.IsNullOrEmpty(param.Donvi) ? "" : param.Donvi;
            var filterLinhVuc = string.IsNullOrEmpty(param.LinhVuc) ? "" : param.LinhVuc;
            allResult = (filterDonVi == "") ? allResult.ToList() : allResult.Where(r => r.MaDonVi == filterDonVi).ToList();
            allResult = (filterLinhVuc == "") ? allResult.ToList() : allResult.Where(r => r.linhvuc == filterLinhVuc).ToList();
            IEnumerable<getCuocStatus_Result> filteredResult;
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
            Func<getCuocStatus_Result, string> orderingFunction = (c => sortColumnIndex == 2 && Sortable_2 ? c.TenCuoc :
                                                            sortColumnIndex == 3 && Sortable_3 ? c.DonVi :
                                                            sortColumnIndex == 4 && Sortable_4 ? c.SoQuyetDinh :
                                                            sortColumnIndex == 5 && Sortable_5 ? c.linhvuc :
                                                            "");
            Func<getCuocStatus_Result, Int64> orderingFunction2 = (c => sortColumnIndex == 0 && Sortable_0 ? c.STT : 0);

            var sortDirection = Request["sSortDir_0"]; // asc or desc
            if (sortDirection == "asc")
                filteredResult = filteredResult.OrderBy(orderingFunction).ThenBy(orderingFunction2);
            else
                filteredResult = filteredResult.OrderByDescending(orderingFunction).ThenByDescending(orderingFunction2);

            var displayed = filteredResult.Skip(param.iDisplayStart).Take(param.iDisplayLength);
            var result = displayed.Select(c => new
                             {
                                 col0 = c.STT,
                                 col1 = c.MaCuoc,
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

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Donvi = new SelectList(db.getDonVi(true, Session["year"].ToString()), "MaDonVi", "TenDonVi");

            ViewBag.LinhVuc = new SelectList(db.TD_LVKT, "MA", "TEN");

            ViewBag.LoaiHinh = new SelectList(db.TD_LHKT, "MA", "TEN");

            return PartialView();
        }

        /// <summary>
        /// Confirm Create
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CUOC_KT c)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    c.MA_CUOC = db.genCode(c.NAM_KT, c.MA_DVKT, c.MA_LVKT, c.MA_LHKT).FirstOrDefault().ToString();
                    c.SO_QD = (string.IsNullOrEmpty(c.SO_QD)) ? "" : c.SO_QD;
                    db.CUOC_KT.Add(c);
                    db.SaveChanges();

                    TempData["Msg"] = "Đã thêm 1 cuộc kiểm toán mới";

                }
                catch (Exception)
                {
                    TempData["Msg"] = "Có lỗi";
                }

                return RedirectToAction("Index");
            }

            return PartialView(c);
        }

        [HttpGet]
        public ActionResult Edit(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var d = db.CUOC_KT.FirstOrDefault(c => c.MA_CUOC == key);

            Session["EditId"] = d.ID;
            Session["EditMa"] = d.MA_CUOC;
            if (d == null)
            {
                return HttpNotFound();
            }

            ViewBag.Donvi = new SelectList(db.TD_DVKT.ToList(), "MA", "TEN");

            ViewBag.LinhVuc = new SelectList(db.TD_LVKT.ToList(), "MA", "TEN");

            ViewBag.LoaiHinh = new SelectList(db.TD_LHKT.ToList(), "MA", "TEN");

            return PartialView(d);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CUOC_KT c)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    c.ID = int.Parse(Session["EditId"].ToString());
                    c.MA_CUOC = Session["EditMa"].ToString();
                    db.Entry(c).State = EntityState.Modified;

                    await db.SaveChangesAsync();
                    TempData["Msg"] = "Đã sửa";
                }
                catch (Exception)
                {
                    TempData["Msg"] = "Có lỗi";
                }

                return RedirectToAction("Index");
            }
            return PartialView(c);
        }

        [HttpGet]
        public ActionResult Delete(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var filtered = db.CUOC_KT.FirstOrDefault(c => c.MA_CUOC == key);
            Session["DelMa"] = filtered.MA_CUOC;
            if (filtered == null)
            {
                return HttpNotFound();
            }

            ViewBag.DonVi = db.TD_DVKT.FirstOrDefault(d => d.MA == filtered.MA_DVKT).TEN;

            return PartialView(filtered);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ConfirmDelete(string MA_CUOC)
        {
            try
            {
                if (MA_CUOC != Session["DelMa"].ToString()) TempData["Msg"] = "Có lỗi";
                else
                {
                    CUOC_KT t = db.CUOC_KT.FirstOrDefault(c => c.MA_CUOC == MA_CUOC);
                    db.CUOC_KT.Remove(t);
                    await db.SaveChangesAsync();
                    TempData["Msg"] = "Đã xóa";
                }
            }
            catch (Exception ex)
            {
                TempData["Msg"] = "Có lỗi" + ex;
            }

            return RedirectToAction("Index");
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