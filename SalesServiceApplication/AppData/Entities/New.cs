using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Entities
{
    public class New
    {
        public int Id { get; set; }

        public DateTime PublicationDate { get; set; }
        public string Header { get; set; } = null!;
        public string Text { get; set; } = null!;

        public int EmployeeId { get; set; }

        public Employee Employee { get; set; } = null!;
    }
}
