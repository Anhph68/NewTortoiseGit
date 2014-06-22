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
    public class LinhVucController : Controller
    {
        private TDKTEntities db = new TDKTEntities();

        // GET: LinhVuc
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AjaxHandler(jQueryDataTableParamModel param)
        {
            var allResult = db.getLinhVuc().ToList();

            IEnumerable<getLinhVuc_Result> filteredResult;
            //Check whether the companies should be filtered by keyword
            if (!string.IsNullOrEmpty(param.sSearch))
            {
                //Optionally check whether the columns are searchable at all 
                var Searchable_0 = Convert.ToBoolean(Request["bSearchable_0"]);
                var Searchable_1 = Convert.ToBoolean(Request["bSearchable_1"]);
                var Searchable_2 = Convert.ToBoolean(Request["bSearchable_2"]);
                int tmp = int.TryParse(param.sSearch, out tmp) ? tmp : 0;

                filteredResult = allResult
                   .Where(c => Searchable_1 && c.MA.ToLower().Contains(param.sSearch.ToLower())
                            || Searchable_2 && c.TEN.ToLower().Contains(param.sSearch.ToLower())
                            || Searchable_0 && c.STT.Equals(tmp)
                        );
            }
            else filteredResult = allResult;

            var Sortable_0 = Convert.ToBoolean(Request["bSortable_0"]);
            var Sortable_1 = Convert.ToBoolean(Request["bSortable_1"]);
            var Sortable_2 = Convert.ToBoolean(Request["bSortable_2"]);
            var sortColumnIndex = Convert.ToInt64(Request["iSortCol_0"]);
            Func<getLinhVuc_Result, string> orderingFunction = (c => sortColumnIndex == 1 && Sortable_1 ? c.MA :
                                                            sortColumnIndex == 2 && Sortable_2 ? c.TEN : "");
            Func<getLinhVuc_Result, Int64> orderingFunction2 = (c => sortColumnIndex == 0 && Sortable_0 ? c.STT : 0);

            var sortDirection = Request["sSortDir_0"]; // asc or desc
            if (sortDirection == "asc")
                filteredResult = filteredResult.OrderBy(orderingFunction).ThenBy(orderingFunction2);
            else
                filteredResult = filteredResult.OrderByDescending(orderingFunction).ThenByDescending(orderingFunction2);

            var displayed = filteredResult.Skip(param.iDisplayStart).Take(param.iDisplayLength);
            var result = displayed.Select(c => new
            {
                col0 = c.STT,
                col1 = c.MA,
                col2 = c.TEN
            });

            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = allResult.Count(),
                iTotalDisplayRecords = filteredResult.Count(),
                aaData = result
            }, JsonRequestBehavior.AllowGet);

        }

        // GET: LinhVuc/Create
        public ActionResult Create()
        {
            return PartialView();
        }

        // POST: LinhVuc/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "MA,TEN")] TD_LVKT l)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.TD_LVKT.Add(l);
                    await db.SaveChangesAsync();
                    TempData["Msg"] = "Đã thêm lĩnh vực kiểm toán mới";
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    TempData["Msg"] = "Có lỗi";
                }
            }

            return View(l);
        }

        // GET: LinhVuc/Edit/5
        public async Task<ActionResult> Edit(string key)
        {
            if (key == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TD_LVKT l = await db.TD_LVKT.FindAsync(key);
            if (l == null)
            {
                return HttpNotFound();
            }
            return PartialView(l);
        }

        // POST: LinhVuc/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MA,TEN")] TD_LVKT l)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(l).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    TempData["Msg"] = "Đã sửa";
                }
                catch (Exception)
                {
                    TempData["Msg"] = "Có lỗi";
                }

                return RedirectToAction("Index");
            }
            return PartialView(l);
        }

        // GET: LinhVuc/Delete/5
        public async Task<ActionResult> Delete(string key)
        {
            if (key == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TD_LVKT l = await db.TD_LVKT.FindAsync(key);
            if (l == null)
            {
                return HttpNotFound();
            }
            return PartialView(l);
        }

        // POST: LinhVuc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string MA)
        {
            try
            {
                TD_LVKT l = await db.TD_LVKT.FindAsync(MA);
                db.TD_LVKT.Remove(l);
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
