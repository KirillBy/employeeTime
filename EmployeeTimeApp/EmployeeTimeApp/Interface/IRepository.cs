using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeTimeApp.Interface
{
    interface IRepository<T> : IDisposable
            where T : class
    {
        IEnumerable<T> GetBookList(); 
    }
}
