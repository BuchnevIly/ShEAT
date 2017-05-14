using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShEAT.Models.Entity
{
    public class Log
    {
        public int ID { get; set; }

        public string Method { get; set; }

        public string Text { get; set; }
    }
}