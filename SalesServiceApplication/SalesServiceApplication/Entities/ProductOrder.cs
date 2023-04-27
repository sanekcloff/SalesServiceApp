using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesServiceApplication.Entities
{
    public class ProductOrder
    {
        public int Id { get; set; }

        public string LastName { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string MiddleName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public DateTime DateOfAdd { get; set; }
        public DateTime? DateOfComplete { get; set; }
        public bool IsCompleted { get; set; }

        public int ProductId { get; set; }
        public int? EmployeeId { get; set; }

        public Product Product { get; set; } = null!;
        public Employee Employee { get; set; } = null!;

        [NotMapped]
        public string FullName { get => $"{LastName} {FirstName} {MiddleName}"; }
        public string Status { get => IsCompleted ? "Завершён" : "Ожидает ответа"; }
    }
}
