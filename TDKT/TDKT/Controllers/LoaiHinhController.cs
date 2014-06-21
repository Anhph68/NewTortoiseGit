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
    public class LoaiHinhController : Controller
    {
        private TDKTEntities db = new TDKTEntities();

        // GET: LoaiHinh
        public async Task<ActionResult> Index()
        {
            return View(await db.TD_LHKT.ToListAsync());
        }

        // GET: LoaiHinh/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TD_LHKT tD_LHKT = await db.TD_LHKT.FindAsync(id);
            if (tD_LHKT == null)
            {
                return HttpNotFound();
            }
            return View(tD_LHKT);
        }

        // GET: LoaiHinh/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LoaiHinh/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "MA,TEN")] TD_LHKT tD_LHKT)
        {
            if (ModelState.IsValid)
            {
                db.TD_LHKT.Add(tD_LHKT);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tD_LHKT);
        }

        // GET: LoaiHinh/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TD_LHKT tD_LHKT = await db.TD_LHKT.FindAsync(id);
            if (tD_LHKT == null)
            {
                return HttpNotFound();
            }
            return View(tD_LHKT);
        }

        // POST: LoaiHinh/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MA,TEN")] TD_LHKT tD_LHKT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tD_LHKT).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tD_LHKT);
        }

        // GET: LoaiHinh/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TD_LHKT tD_LHKT = await db.TD_LHKT.FindAsync(id);
            if (tD_LHKT == null)
            {
                return HttpNotFound();
            }
            return View(tD_LHKT);
        }

        // POST: LoaiHinh/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            TD_LHKT tD_LHKT = await db.TD_LHKT.FindAsync(id);
            db.TD_LHKT.Remove(tD_LHKT);
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
