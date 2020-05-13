using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeTimeApp.Class;
using EmployeeTimeApp.Interface;

namespace EmployeeTimeApp
{
    class Program
    {
        static void Main()
        {
            IRepository<FullTimeReport> db;

            db = new SQLTimeReportRepository();

            TimeReportGenerator reportGenerator = new TimeReportGenerator(db.GetBookList());

            reportGenerator.DayOfWeekChampionsGenerate();

            Console.ReadLine();
        }

    }
}
