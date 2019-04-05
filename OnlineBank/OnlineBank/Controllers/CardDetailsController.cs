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
    public class CardDetailsController : Controller
    {
        private ATMEntities db = new ATMEntities();

        // GET: CardDetails
        public ActionResult Index()
        {
            var cardDetails = db.CardDetails.Include(c => c.AccountHolder);
            return View(cardDetails.ToList());
        }

        // GET: CardDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CardDetail cardDetail = db.CardDetails.Find(id);
            if (cardDetail == null)
            {
                return HttpNotFound();
            }
            return View(cardDetail);
        }

        // GET: CardDetails/Create
        public ActionResult Create()
        {
            ViewBag.accountID = new SelectList(db.AccountHolders, "accountID", "email");
            return View();
        }

        // POST: CardDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cardID,cardNum,cardPin,cbalance,accountID")] CardDetail cardDetail)
        {
            if (ModelState.IsValid)
            {
                db.CardDetails.Add(cardDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.accountID = new SelectList(db.AccountHolders, "accountID", "email", cardDetail.accountID);
            return View(cardDetail);
        }

        // GET: CardDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CardDetail cardDetail = db.CardDetails.Find(id);
            if (cardDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.accountID = new SelectList(db.AccountHolders, "accountID", "email", cardDetail.accountID);
            return View(cardDetail);
        }

        // POST: CardDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "cardID,cardNum,cardPin,cbalance,accountID")] CardDetail cardDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cardDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.accountID = new SelectList(db.AccountHolders, "accountID", "email", cardDetail.accountID);
            return View(cardDetail);
        }

        // GET: CardDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CardDetail cardDetail = db.CardDetails.Find(id);
            if (cardDetail == null)
            {
                return HttpNotFound();
            }
            return View(cardDetail);
        }

        // POST: CardDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CardDetail cardDetail = db.CardDetails.Find(id);
            db.CardDetails.Remove(cardDetail);
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
