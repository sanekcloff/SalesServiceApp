using AppData.DataBaseData;
using AppData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Services
{
    public class DepartmentService
    {
        private ApplicationDbContext _ctx;
        public DepartmentService(ApplicationDbContext ctx)
        {
            _ctx=ctx;
        }
        public ICollection<Department> GetDepartments()
            => _ctx.Departments.ToList();
        public Department? GetDepartment(int id)
            => _ctx.Departments.SingleOrDefault(d=>d.Id==id);
        public void Insert(Department department)
        {
            _ctx.Departments.Add(department);
            _ctx.SaveChanges();
        }
        public void Update(Department department)
        {
            _ctx.Departments.Update(department);
            _ctx.SaveChanges();
        }
        public void Delete(Department department)
        {
            _ctx.Departments.Remove(department);
            _ctx.SaveChanges();
        }
    }
}
