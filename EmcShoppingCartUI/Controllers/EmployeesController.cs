using Azure.Core;
using EmcShoppingCartUI.Data;
using EmcShoppingCartUI.Models;
using EmcShoppingCartUI.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmcShoppingCartUI.Controllers //The controller is responsible for handling requests related to employees.
{ 
    public class EmployeesController : Controller
    {
        //Connection to data base like calling connnection string func
        private readonly MVCdemoDbContext mvcDemoDbContext;

        public EmployeesController(MVCdemoDbContext mvcDemoDbContext)
        {
            this.mvcDemoDbContext = mvcDemoDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employees = await mvcDemoDbContext.Employees.ToListAsync(); // this line retrieves a list of employees from the Employees property of the mvcDemoDbContext.
            return View(employees);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeViewModel request)
        {
            var employee = new Employee()
            {
                Id =  Guid.NewGuid(),
                Name = request.Name,
                Email = request.Email,
                Department = request.Department,
                Salary = request.Salary,

            };
            await mvcDemoDbContext.Employees.AddAsync(employee);
            await mvcDemoDbContext.SaveChangesAsync();
            return RedirectToAction("Index");

        }
        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
           var employee = await mvcDemoDbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if (employee != null)
            {
                var viewModel = new UpdateEmployeeModel()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Email = employee.Email,
                    Department = employee.Department,
                    Salary = employee.Salary,
                };
                return View(viewModel);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateEmployeeModel updatedata)
        {
            var employee = await mvcDemoDbContext.Employees.FindAsync(updatedata.Id);
            if (employee != null)
            {
                employee.Name = updatedata.Name;
                employee.Email = updatedata.Email;
                employee.Department = updatedata.Department;
                employee.Salary = updatedata.Salary;

                await mvcDemoDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
