using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Entities
{
    public class ServiceOrder
    {
        public int Id { get; set; }

        public DateTime DateOfAdd { get; set; }
        public DateTime? DateOfComplete { get; set; }
        public string Status { get; set; } = null!;

        public int ServiceId { get; set; }
        public int ClientId { get; set; }
        public int? EmployeeId { get; set; }

        public Service Service { get; set; } = null!;
        public Client Client { get; set; } = null!;
        public Employee Employee { get; set; } = null!;
    }
}
