using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TDKT.Models;

namespace TDKT.Controllers
{
    [Authorize(Roles = "Quản trị")]
    public class DonViController : Controller
    {
        private TDKTEntities db = new TDKTEntities();

        // GET: DonVi
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AjaxHandler(jQueryDataTableParamModel param)
        {
            var allResult = db.getDonVi(null, "").ToList();

            IEnumerable<getDonVi_Result> filteredResult;
            //Check whether the companies should be filtered by keyword
            if (!string.IsNullOrEmpty(param.sSearch))
            {
                //Optionally check whether the columns are searchable at all 
                var Searchable_0 = Convert.ToBoolean(Request["bSearchable_0"]);
                var Searchable_1 = Convert.ToBoolean(Request["bSearchable_1"]);
                var Searchable_2 = Convert.ToBoolean(Request["bSearchable_2"]);
                int tmp = int.TryParse(param.sSearch, out tmp) ? tmp : 0;

                filteredResult = allResult
                   .Where(c => Searchable_1 && c.MaDonVi.ToLower().Contains(param.sSearch.ToLower())
                            || Searchable_2 && c.TenDonVi.ToLower().Contains(param.sSearch.ToLower())
                            || Searchable_0 && c.STT.Equals(tmp)
                        );
            }
            else filteredResult = allResult;

            var Sortable_0 = Convert.ToBoolean(Request["bSortable_0"]);
            var Sortable_1 = Convert.ToBoolean(Request["bSortable_1"]);
            var Sortable_2 = Convert.ToBoolean(Request["bSortable_2"]);
            var sortColumnIndex = Convert.ToInt64(Request["iSortCol_0"]);
            Func<getDonVi_Result, string> orderingFunction = (c => sortColumnIndex == 1 && Sortable_1 ? c.MaDonVi :
                                                            sortColumnIndex == 2 && Sortable_2 ? c.TenDonVi : "");
            Func<getDonVi_Result, Int64> orderingFunction2 = (c => sortColumnIndex == 0 && Sortable_0 ? c.STT : 0);

            var sortDirection = Request["sSortDir_0"]; // asc or desc
            if (sortDirection == "asc")
                filteredResult = filteredResult.OrderBy(orderingFunction).ThenBy(orderingFunction2);
            else
                filteredResult = filteredResult.OrderByDescending(orderingFunction).ThenByDescending(orderingFunction2);

            var displayed = filteredResult.Skip(param.iDisplayStart).Take(param.iDisplayLength);
            var result = displayed.Select(c => new
            {
                col0 = c.STT,
                col1 = c.MaDonVi,
                col2 = c.TenDonVi,
                col3 = c.CanAudit ? "<span class='btn btn-md btn-success glyphicon glyphicon-ok-circle'></span>" : "<span class='btn btn-md btn-warning glyphicon glyphicon-remove-circle'></span>",
                col4 = (c.BeginDate != null) ? String.Format("{0: dd/MM/yyyy}", c.BeginDate) : "",
                col5 = (c.EndDate != null) ? String.Format("{0: dd/MM/yyyy}", c.EndDate) : "Đang hoạt động",
            });

            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = allResult.Count(),
                iTotalDisplayRecords = filteredResult.Count(),
                aaData = result
            }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Create()
        {
            return PartialView();
        }

        // POST: DonVi/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TD_DVKT d)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.TD_DVKT.Add(d);
                    await db.SaveChangesAsync();
                    TempData["Msg"] = "Đã thêm 1 đơn vị mới";

                }
                catch (Exception)
                {
                    TempData["Msg"] = "Có lỗi";
                }

                return RedirectToAction("Index");
            }

            return PartialView(d);
        }

        // GET: DonVi/Edit/5
        public async Task<ActionResult> Edit(string key)
        {
            if (key == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TD_DVKT d = await db.TD_DVKT.FindAsync(key);
            if (d == null)
            {
                return HttpNotFound();
            }
            return PartialView(d);
        }

        // POST: DonVi/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(TD_DVKT d)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(d).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    TempData["Msg"] = "Đã sửa";
                }
                catch (Exception)
                {
                    TempData["Msg"] = "Có lỗi";
                }

                return RedirectToAction("Index");
            }
            return PartialView(d);
        }

        // GET: DonVi/Delete/5
        public async Task<ActionResult> Delete(string key)
        {
            if (key == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TD_DVKT d = await db.TD_DVKT.FindAsync(key);
            if (d == null)
            {
                return HttpNotFound();
            }
            return PartialView(d);
        }

        // POST: DonVi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string MA)
        {
            try
            {
                TD_DVKT d = await db.TD_DVKT.FindAsync(MA);
                db.TD_DVKT.Remove(d);
                await db.SaveChangesAsync();
                TempData["Msg"] = "Đã xóa";
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
