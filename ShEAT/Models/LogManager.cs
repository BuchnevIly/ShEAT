using ShEAT.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShEAT.Models
{
    public class LogManager
    {
        private CaloriesContext db;

        public LogManager()
        {
            db = new CaloriesContext();
        }


        public void WriteLog(string method, string text)
        {
            db.Logs.Add(new Log
            {
                Method = method,
                Text = text
            });

            db.SaveChanges();
        }
    }
}