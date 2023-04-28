using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Entities
{
    public class Client
    {
        public int Id { get; set; }

        public string LastName { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string MiddleName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string? Organization { get; set; } = null!;
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;

        [NotMapped]
        public string FullName { get => $"{LastName} {FirstName} {MiddleName}"; }
    }
}
