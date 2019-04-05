using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineBank.Models;

namespace OnlineBank.Controllers
{
    public class TransactionDatasController : Controller
    {
        private ATMEntities db = new ATMEntities();

        // GET: TransactionDatas
        public ActionResult Index()
        {
            var transactionDatas = db.TransactionDatas.Include(t => t.CardDetail);
            return View(transactionDatas.ToList());
        }

        // GET: TransactionDatas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransactionData transactionData = db.TransactionDatas.Find(id);
            if (transactionData == null)
            {
                return HttpNotFound();
            }
            return View(transactionData);
        }

        // GET: TransactionDatas/Create
        public ActionResult Create()
        {
            ViewBag.cardID = new SelectList(db.CardDetails, "cardID", "cardNum");
            return View();
        }

        // POST: TransactionDatas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "transactionID,datemade,cashIn,cashOut,cardID")] TransactionData transactionData)
        {
            if (ModelState.IsValid)
            {
                db.TransactionDatas.Add(transactionData);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.cardID = new SelectList(db.CardDetails, "cardID", "cardNum", transactionData.cardID);
            return View(transactionData);
        }

        // GET: TransactionDatas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransactionData transactionData = db.TransactionDatas.Find(id);
            if (transactionData == null)
            {
                return HttpNotFound();
            }
            ViewBag.cardID = new SelectList(db.CardDetails, "cardID", "cardNum", transactionData.cardID);
            return View(transactionData);
        }

        // POST: TransactionDatas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "transactionID,datemade,cashIn,cashOut,cardID")] TransactionData transactionData)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transactionData).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cardID = new SelectList(db.CardDetails, "cardID", "cardNum", transactionData.cardID);
            return View(transactionData);
        }

        // GET: TransactionDatas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransactionData transactionData = db.TransactionDatas.Find(id);
            if (transactionData == null)
            {
                return HttpNotFound();
            }
            return View(transactionData);
        }

        // POST: TransactionDatas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TransactionData transactionData = db.TransactionDatas.Find(id);
            db.TransactionDatas.Remove(transactionData);
            db.SaveChanges();
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
