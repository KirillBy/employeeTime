using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeTimeApp
{
    class Program
    {
        static void Main()
        {
            var timeReports = GetTimeReports();

            foreach (var timeReport in timeReports)
            {
                Console.WriteLine("{0} works {1} hours at {2}  {3}", timeReport.EmployeeName, timeReport.WorkHours,
                    timeReport.WorkDay.ToString("M/d/yyyy"), timeReport.WorkDay.DayOfWeek);
            }
            Console.ReadLine();
        }

        private static IQueryable<FullTimeReport> GetTimeReports()
        {
            var context = new EmployeeDbContext();
            var timeReports = from tr in context.TimeReports
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
