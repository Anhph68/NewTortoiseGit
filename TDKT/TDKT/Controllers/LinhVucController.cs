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
        public async Task<ActionResult> Index()
        {
            return View(await db.TD_LVKT.ToListAsync());
        }

        // GET: LinhVuc/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TD_LVKT tD_LVKT = await db.TD_LVKT.FindAsync(id);
            if (tD_LVKT == null)
            {
                return HttpNotFound();
            }
            return View(tD_LVKT);
        }

        // GET: LinhVuc/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LinhVuc/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "MA,TEN")] TD_LVKT tD_LVKT)
        {
            if (ModelState.IsValid)
            {
                db.TD_LVKT.Add(tD_LVKT);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tD_LVKT);
        }

        // GET: LinhVuc/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TD_LVKT tD_LVKT = await db.TD_LVKT.FindAsync(id);
            if (tD_LVKT == null)
            {
                return HttpNotFound();
            }
            return View(tD_LVKT);
        }

        // POST: LinhVuc/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MA,TEN")] TD_LVKT tD_LVKT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tD_LVKT).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tD_LVKT);
        }

        // GET: LinhVuc/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TD_LVKT tD_LVKT = await db.TD_LVKT.FindAsync(id);
            if (tD_LVKT == null)
            {
                return HttpNotFound();
            }
            return View(tD_LVKT);
        }

        // POST: LinhVuc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            TD_LVKT tD_LVKT = await db.TD_LVKT.FindAsync(id);
            db.TD_LVKT.Remove(tD_LVKT);
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
