using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineBank.Models;
using OnlineBank.Utilities;

namespace OnlineBank.Controllers
{
    public class AccountHoldersController : Controller
    {
        private ATMEntities db = new ATMEntities();

        // GET: AccountHolders/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: AccountHolders/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "accountID,email,hashPassword,salt,firstName,lastName,balance")] AccountHolder accountHolder)
        {
            
            if (ModelState.IsValid)
            {
                AccountHolder ah = db.AccountHolders.SingleOrDefault(a => a.email == accountHolder.email);
                IQueryable<CardDetail> cd = db.CardDetails.Where(c => c.accountID == ah.accountID);
                int i = 0;
                foreach (var item in cd)
                {
                    double c[i] = 
                }

                if (ah == null || PasswordSecurity.GenerateHash(accountHolder.password, ah.salt) != ah.hashPassword)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    Session["accountID"] = ah.accountID;
                    Session["firstName"] = ah.firstName;
                    Session["lastName"] = ah.lastName;
                    ah.balance = 
                    return RedirectToAction("Details/"+ ah.accountID);
                }
            }

            return View(accountHolder);
        }

        // GET: AccountHolders
        public ActionResult Index()
        {
            return View(db.AccountHolders.ToList());
        }

        // GET: AccountHolders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountHolder accountHolder = db.AccountHolders.Find(id);
            if (accountHolder == null)
            {
                return HttpNotFound();
            }
            return View(accountHolder);
        }

        // GET: AccountHolders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AccountHolders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "accountID,email,hashPassword,salt,firstName,lastName,balance")] AccountHolder accountHolder)
        {
            if (ModelState.IsValid)
            {
                accountHolder.salt = PasswordSecurity.GenerateSalt(3);
                accountHolder.hashPassword = PasswordSecurity.GenerateHash(accountHolder.password, accountHolder.salt);
                
                db.AccountHolders.Add(accountHolder);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(accountHolder);
        }

        // GET: AccountHolders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountHolder accountHolder = db.AccountHolders.Find(id);
            if (accountHolder == null)
            {
                return HttpNotFound();
            }
            return View(accountHolder);
        }

        // POST: AccountHolders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "accountID,email,hashPassword,salt,firstName,lastName,balance")] AccountHolder accountHolder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(accountHolder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(accountHolder);
        }

        // GET: AccountHolders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountHolder accountHolder = db.AccountHolders.Find(id);
            if (accountHolder == null)
            {
                return HttpNotFound();
            }
            return View(accountHolder);
        }

        // POST: AccountHolders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AccountHolder accountHolder = db.AccountHolders.Find(id);
            db.AccountHolders.Remove(accountHolder);
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
