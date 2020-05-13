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
                                      Hours = Math.Round(grp.Average(t => t.WorkHours), 2)
                                  }).GroupBy(x => x.WeekDay).SelectMany(g => g.OrderByDescending(e => e.Hours)
                                  .TopWithTies(3, x => x.Hours)).ToList();

            //Grouping each day worker into one Day of week group
            var dayGroups = from day in DayOfWeekGroup
                            group day by day.WeekDay into g
                            select new
                            {
                                WeekDay = g.Key,
                                Hour = from h in g select h,
                                Name = from n in g select n
                            };
            //Show result on console
            DayOfWeekChampionsDisplay(dayGroups);
        }

        //Correct display with table view
        private void DayOfWeekChampionsDisplay(IEnumerable<dynamic> dayGroups)
        {
            foreach (var group in dayGroups)
            {

                StringBuilder sb = new StringBuilder();
                sb.Append($"{group.WeekDay,-9} | ");
                foreach (var item in group.Hour)
                    sb.Append($"{item.Name} ({item.Hours} hours), ");
                sb.Remove(sb.Length - 2, 1);
                sb.Append(' ', MaxStringBuilderLenght(dayGroups) - sb.Length);
                Console.WriteLine(sb + " | ");
            }
        }

        //Finding max Lenght of string for making correct table
        private int MaxStringBuilderLenght(IEnumerable<dynamic> dayGroups)
        {
            int maxStrLenght = 0;
            foreach (dynamic group in dayGroups)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"{group.WeekDay,-9} | ");
                foreach (var item in group.Hour)
                    sb.Append($"{item.Name} ({item.Hours} hours), ");
                sb.Remove(sb.Length - 2, 1);
                int currentStrLenght = sb.Length;
                if (currentStrLenght > maxStrLenght)
                    maxStrLenght = currentStrLenght;
            }
            return maxStrLenght;
        }
    }
}
