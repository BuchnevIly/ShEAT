using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShEAT.Models.Entity
{
    public class History
    {
        public int ID { get; set; }

        public int UserID { get; set; }

        public DateTime UpdateTime { get; set; }

        public string ImageURL { get; set; }

        public string Name { get; set; }

        public double Calories { get; set; }

        public bool IsVisible { get; set; }
    }
}