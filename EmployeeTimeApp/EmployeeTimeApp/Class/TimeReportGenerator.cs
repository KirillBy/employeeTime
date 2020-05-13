using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeTimeApp.Class
{
    internal class TimeReportGenerator
    {
        IEnumerable<FullTimeReport> timeReport;
        public TimeReportGenerator(IEnumerable<FullTimeReport> timeReport)
        {
            this.timeReport = timeReport;
        }

        public void DayOfWeekChampionsGenerate()
        {
            //Group by Day of week and Name, with summ of hours for each worker to each day of week,
            //leaving only top 3 worker for each day of week. If on the third place 2 or more person
            //thay will both be in raiting(TopWithTies extension)
            var DayOfWeekGroup = (from tr in timeReport
                                  group tr by new
                                  {
                                      Name = tr.EmployeeName,
                                      WeekDay = tr.WorkDay.DayOfWeek,
                                  } into grp
                                  select new
                                  {
                                      grp.Key.WeekDay,
                                      grp.Key.Name,
                                      Hours = grp.Sum(t => t.WorkHours)
                                  }).GroupBy(x => x.WeekDay).SelectMany(g => g.OrderByDescending(e => e.Hours)
                                  .TopWithTies(2, x => x.Hours)).ToList();

            //Grouping each day worker into one Day of week group
            var dayGroups = from day in DayOfWeekGroup
                            group day by day.WeekDay into g
                            select new
                            {
                                WeekDay = g.Key,
                                Hour = from h in g select h,
                                Name = from n in g select n
                            };

            //Correct display with table view
            foreach (var group in dayGroups)
            {

                StringBuilder sb = new StringBuilder();

                sb.Append($"{group.WeekDay,-9} | ");

                foreach (var item in group.Hour)
                    sb.Append($"{item.Name} ({item.Hours} hours), ");

                sb.Remove(sb.Length - 2, 1);

                Console.WriteLine("{0, -90} |", sb.Shorten(90));
            }
        }
    }
}
