using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Entities
{
    public class Review
    {
        public int Id { get; set; }

        public string Text { get; set; } = null!;
        public int Grade { get; set; }
        public DateTime DateOfAdd { get; set; }

        public int ClientId { get; set; }

        public Client Client { get; set; } = null!;
    }
}
