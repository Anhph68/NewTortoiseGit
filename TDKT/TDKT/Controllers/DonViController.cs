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
    public class DonViController : Controller
    {
        private TDKTEntities db = new TDKTEntities();

        // GET: DonVi
        public async Task<ActionResult> Index()
        {
            ViewBag.Donvi = new SelectList(db.getDonVi(true, null), "MaDonVi", "TenDonVi");
            return View(await db.TD_DVKT.ToListAsync());
        }

        public ActionResult AjaxHandler(jQueryDataTableParamModel param)
        {
            var allResult = db.getUsers(string.IsNullOrEmpty(param.Donvi) ? "" : param.Donvi).ToList();

            IEnumerable<getUsers_Result> filteredResult;
            //Check whether the companies should be filtered by keyword
            if (!string.IsNullOrEmpty(param.sSearch))
            {
                //Optionally check whether the columns are searchable at all 
                var Searchable_0 = Convert.ToBoolean(Request["bSearchable_0"]);
                var Searchable_1 = Convert.ToBoolean(Request["bSearchable_1"]);
                var Searchable_2 = Convert.ToBoolean(Request["bSearchable_2"]);
                int tmp = int.TryParse(param.sSearch, out tmp) ? tmp : 0;

                filteredResult = allResult
                   .Where(c => Searchable_1 && c.HoTen.ToLower().Contains(param.sSearch.ToLower())
                            || Searchable_2 && c.TenDangNhap.ToLower().Contains(param.sSearch.ToLower())
                            || Searchable_0 && c.STT.Equals(tmp)
                        );
            }
            else filteredResult = allResult;

            var Sortable_0 = Convert.ToBoolean(Request["bSortable_0"]);
            var Sortable_1 = Convert.ToBoolean(Request["bSortable_1"]);
            var Sortable_2 = Convert.ToBoolean(Request["bSortable_2"]);
            var sortColumnIndex = Convert.ToInt64(Request["iSortCol_0"]);
            Func<getUsers_Result, string> orderingFunction = (c => sortColumnIndex == 1 && Sortable_1 ? c.HoTen :
                                                            sortColumnIndex == 2 && Sortable_2 ? c.TenDangNhap : "");
            Func<getUsers_Result, Int64> orderingFunction2 = (c => sortColumnIndex == 0 && Sortable_0 ? c.STT : 0);

            var sortDirection = Request["sSortDir_0"]; // asc or desc
            if (sortDirection == "asc")
                filteredResult = filteredResult.OrderBy(orderingFunction).ThenBy(orderingFunction2);
            else
                filteredResult = filteredResult.OrderByDescending(orderingFunction).ThenByDescending(orderingFunction2);

            var displayed = filteredResult.Skip(param.iDisplayStart).Take(param.iDisplayLength);
            var result = displayed.Select(c => new
            {
                STT = c.STT,
                ID = c.ID,
                HoTen = c.HoTen,
                TenDangNhap = c.TenDangNhap,
                DonVi = c.DonVi
            });

            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = allResult.Count(),
                iTotalDisplayRecords = filteredResult.Count(),
                aaData = result
            }, JsonRequestBehavior.AllowGet);

        }

        // GET: DonVi/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TD_DVKT tD_DVKT = await db.TD_DVKT.FindAsync(id);
            if (tD_DVKT == null)
            {
                return HttpNotFound();
            }
            return View(tD_DVKT);
        }

        // GET: DonVi/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DonVi/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "MA,TEN")] TD_DVKT tD_DVKT)
        {
            if (ModelState.IsValid)
            {
                db.TD_DVKT.Add(tD_DVKT);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tD_DVKT);
        }

        // GET: DonVi/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TD_DVKT tD_DVKT = await db.TD_DVKT.FindAsync(id);
            if (tD_DVKT == null)
            {
                return HttpNotFound();
            }
            return View(tD_DVKT);
        }

        // POST: DonVi/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MA,TEN")] TD_DVKT tD_DVKT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tD_DVKT).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tD_DVKT);
        }

        // GET: DonVi/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TD_DVKT tD_DVKT = await db.TD_DVKT.FindAsync(id);
            if (tD_DVKT == null)
            {
                return HttpNotFound();
            }
            return View(tD_DVKT);
        }

        // POST: DonVi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            TD_DVKT tD_DVKT = await db.TD_DVKT.FindAsync(id);
            db.TD_DVKT.Remove(tD_DVKT);
            await db.SaveChangesAsync();
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
