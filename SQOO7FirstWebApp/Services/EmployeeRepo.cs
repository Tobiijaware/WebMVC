using SQOO7FirstWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SQOO7FirstWebApp.Services
{
    public class EmployeeRepo : IEmployeeRepo
    {
        public List<Employee> Employees { get; set; } = new List<Employee>
        {
            new Employee{ Id="1", FirstName="Solomon", Photo = "images/user1.jpg"},
            new Employee{ Id="2", FirstName="Diana", Photo = "images/user2.jpg"},
            new Employee{ Id="3", FirstName="Clement", Photo = "images/user1.jpg"},
        };

        public List<Employee> GetEmployees()
        {
            return Employees;
        }
    }
}
