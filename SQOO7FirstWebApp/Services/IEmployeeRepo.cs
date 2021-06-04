using SQOO7FirstWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SQOO7FirstWebApp.Services
{
    public interface IEmployeeRepo
    {
        public List<Employee> GetEmployees();
    }
}
