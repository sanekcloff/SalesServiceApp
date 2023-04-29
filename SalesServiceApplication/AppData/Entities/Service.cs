using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Entities
{
    public class Service
    {
        public Service()
        {
            ServiceOrders = new HashSet<ServiceOrder>();
        }

        public int Id { get; set; }

        public string Title { get; set; } = null!;
        public string? Description { get; set; } = null!;
        public decimal CostPerHour { get; set; }
        public DateTime DateOfAdd { get; set; }
        public string? Picture { get; set; } = null!; 

        public ICollection<ServiceOrder> ServiceOrders { get; set; } = null!;

        [NotMapped]
        public bool IsNegotiable { get => CostPerHour > 0 ? false : true; }
        public string CorrectPicturePath
        {
            get => (Picture == string.Empty || Picture == null)
                ? @"\Storage\Pictures\NonPicture.png" : @$"\Storage\Pictures\{Picture}";
        }
    }
}
