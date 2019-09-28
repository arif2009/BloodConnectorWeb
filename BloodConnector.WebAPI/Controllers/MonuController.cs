using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BloodConnector.WebAPI.Controllers
{
    public class MonuController : Controller
    {
        private DateTime FirstDayOfLastMenstrualPeriod = new DateTime(2019, 6, 10);

        public class Edge
        {
            public DateTime FirstDayOfLastMenstrualPeriod { get; set; }
            public int Days { get; set; }
            public int Hours { get; set; }
            
            public string MonthsWeeksDays { get; set; }
            public string WeeksDays { get; set; }
        }
        // GET: Monu`
        public ActionResult Index()
        {
            var dateDiff = DateTime.Now - FirstDayOfLastMenstrualPeriod;

            #region Age in week
            var w_weeks = dateDiff.TotalDays / 7;
            var w_days = (w_weeks - (int)w_weeks) * 7;
            #endregion

            #region Age in months
            var m_months = dateDiff.TotalDays / 30;
            var m_weeks = ((m_months - (int)m_months) *30)/7;
            var m_days = (m_weeks - (int)m_weeks) * 7;
            #endregion

            var model = new Edge
            {
                FirstDayOfLastMenstrualPeriod = FirstDayOfLastMenstrualPeriod,
                Days = dateDiff.Days,
                Hours = dateDiff.Hours,

                WeeksDays = string.Format("{0} Weeks {1} days", (int)w_weeks, (int)w_days),
                MonthsWeeksDays = string.Format("{0} Months {1} Weeks {2} days", (int)m_months, (int)m_weeks, (int)m_days)
            };

            return View(model);
        }
    }
}