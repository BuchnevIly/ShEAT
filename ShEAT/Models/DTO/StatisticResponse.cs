using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShEAT.Models.DTO
{
    public class StatisticResponse
    {
        public List<DateTime> FullStatistic { get; set; }

        public List<double> FullListCalories { get; set; }
    }
}