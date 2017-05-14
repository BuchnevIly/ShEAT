using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShEAT.Models.DTO
{
    public class CaloriesResponse
    {
        public int ID { get; set; }

        public string ImageURL { get; set; }

        public string NameImageObject { get; set; }

        public double Caloricity { get; set; }
    }
}