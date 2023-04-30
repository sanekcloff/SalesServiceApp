using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Entities
{
    public class Question
    {
        public int Id { get; set; }

        public string Topic { get; set; } = null!;
        public string Text { get; set; } = null!;
        public string? Answer { get; set; } = null!;
        public DateTime DateOfAdd { get; set; }

        public int ClientId { get; set; }
        public int? EmployeeId { get; set; }

        public Client Client { get; set; } = null!;
        public Employee Employee { get; set; } = null!;

        [NotMapped]
        public bool IsAnswered { get => (Answer!=null && Employee!=null); }
    }
}
