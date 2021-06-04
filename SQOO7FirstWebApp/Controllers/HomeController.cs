using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SQOO7FirstWebApp.Models;
using SQOO7FirstWebApp.Services;
using SQOO7FirstWebApp.ViewModels;

namespace SQOO7FirstWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmployeeRepo _employeeRepo;

        public HomeController(ILogger<HomeController> logger, IEmployeeRepo employeeRepo)
        {
            _logger = logger;
            _employeeRepo = employeeRepo;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            //throw new Exception("This page must not run!");

            var employees = _employeeRepo.GetEmployees();

            var employeesToRetun = new List<EmployeeViewModel>();

            foreach(var employee in employees)
            {
                var employeeSingle = new EmployeeViewModel
                {
                    Id = employee.Id,
                    Name = employee.FirstName,
                    Photo = employee.Photo
                };

                employeesToRetun.Add(employeeSingle);
            }

            return View(employeesToRetun);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}
