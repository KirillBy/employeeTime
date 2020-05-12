using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeTimeApp.Class;

namespace EmployeeTimeApp
{
    class Program
    {
        static void Main()
        {
            var DayOfWeekGroup = (from tr in GetTimeReports()
                                 group tr by new
                                 {
                                     Name = tr.EmployeeName , WeekDay = tr.WorkDay.DayOfWeek, 
                                 }  into grp
                                 select new
                                 {
                                     grp.Key.WeekDay,
                                     grp.Key.Name,
                                     Hours = grp.Sum(t => t.WorkHours)
                                 }).GroupBy(x => x.WeekDay)
       .SelectMany(g => g.OrderByDescending(e => e.Hours).TopWithTies(2, x => x.Hours))
       .ToList(); ;
            foreach (var timeReport in DayOfWeekGroup)
            {
                    Console.WriteLine("{0} works {1} hours at {2}", timeReport.Name, timeReport.Hours, timeReport.WeekDay );
         
            }
            

            Console.ReadLine();
        }

      


        private static IEnumerable<FullTimeReport> GetTimeReports()
        {
            var context = new EmployeeDbContext();
            IEnumerable<FullTimeReport> timeReports = from tr in context.TimeReports
                              join e in context.Employee on tr.employee_id equals e.id
                              select new FullTimeReport()
                              {
                                  EmployeeName = e.name,
                                  WorkHours = (float)tr.hours,
                                  WorkDay = tr.date
                              };
            return timeReports;
        }
    }
}
