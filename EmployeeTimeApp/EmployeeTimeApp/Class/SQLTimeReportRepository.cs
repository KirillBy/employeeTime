using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeTimeApp.Interface;

namespace EmployeeTimeApp.Class
{
    //Realisation of Repository Interface
    internal class SQLTimeReportRepository : IRepository<FullTimeReport>
    {
        public EmployeeDbContext db;

        public SQLTimeReportRepository()
        {
            this.db = new EmployeeDbContext();
        }

        //Getting joining information from employee.db and time_report.db into 
        //new combined class FullTimeReport, which have 3 properties:
        //EmployeeName, WorkHours, WorkDay
        public IEnumerable<FullTimeReport> GetBookList()
        {
            IEnumerable<FullTimeReport> timeReports = from tr in db.TimeReports
                                                      join e in db.Employee on tr.employee_id equals e.id
                                                      select new FullTimeReport()
                                                      {
                                                          EmployeeName = e.name,
                                                          WorkHours = (float)tr.hours,
                                                          WorkDay = tr.date
                                                      };
            return timeReports;
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
