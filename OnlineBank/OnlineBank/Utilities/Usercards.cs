using OnlineBank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineBank.Utilities
{
    public static class Usercards
    {
        public static dynamic GetCards(ATMEntities db, int accountID)
        {
            var subQuery = db.CardDetails.Where(h => h.accountID == accountID);

            var result = subQuery.ToList(); // this is where your query and subquery are evaluated and sent to the database
            return result;
            //db.Dispose();
            //db = null;
        }
    }
}