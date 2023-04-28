using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Entities
{
    public class Department
    {
        public Department()
        {
            Employees = new HashSet<Employee>();
        }
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public ICollection<Employee> Employees { get; set; } = null!;
    }
}
