using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesServiceApplication.Entities
{
    public class Employee
    {
        public int Id { get; set; }

        public string LastName { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string MiddleName { get; set; } = null!;
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
        public bool IsAdmin { get; set; }

        [NotMapped]
        public string FullName { get => $"{LastName} {FirstName} {MiddleName}"; }
        public string Role { get => IsAdmin ? "Администратор" : "Сотрудник"; }
    }
}
