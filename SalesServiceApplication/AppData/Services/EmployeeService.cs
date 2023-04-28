using AppData.DataBaseData;
using AppData.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Services
{
    public class EmployeeService
    {
        private ApplicationDbContext _ctx;
        public EmployeeService(ApplicationDbContext ctx)
        {
            _ctx=ctx;
        }
        public ICollection<Employee> GetEmployees()
            => _ctx.Employees
            .Include(e=>e.Department)
            .Include(e=>e.ProductOrders)
            .Include(e=>e.ServiceOrders)
            .ToList();
        public Employee? GetEmployee(int id)
            => _ctx.Employees
            .Include(e => e.Department)
            .Include(e => e.ProductOrders)
            .Include(e => e.ServiceOrders)
            .SingleOrDefault(e=>e.Id==id);
        public void Insert(Employee employee)
        {
            _ctx.Employees.Add(employee);
            _ctx.SaveChanges();
        }
        public void Update(Employee employee)
        {
            _ctx.Employees.Update(employee);
            _ctx.SaveChanges();
        }
        public void Delete(Employee employee)
        {
            _ctx.Employees.Remove(employee);
            _ctx.SaveChanges();
        }
    }
}
