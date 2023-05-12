using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Entities
{
    public class Client
    {
        public Client()
        {
            ProductOrders = new HashSet<ProductOrder>();
            ServiceOrders = new HashSet<ServiceOrder>();
            Reviews = new HashSet<Review>();
            Questions = new HashSet<Question>();
        }

        public int Id { get; set; }

        public string LastName { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string MiddleName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Organization { get; set; } = null!;
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;

        public ICollection<ProductOrder> ProductOrders { get; set; } = null!;
        public ICollection<ServiceOrder> ServiceOrders { get; set; } = null!;
        public ICollection<Review> Reviews { get; set; } = null!;
        public ICollection<Question> Questions { get; set; } = null!;

        [NotMapped]
        public string FullName { get => $"{LastName} {FirstName} {MiddleName}"; }
        public bool IsOrganization { get => Organization!=null; }
    }
}
