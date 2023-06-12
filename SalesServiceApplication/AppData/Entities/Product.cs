using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Entities
{
    public class Product
    {
        public Product()
        {
            ProductCategories = new HashSet<ProductCategory>();
            ProductOrders = new HashSet<ProductOrder>();
        }

        public int Id { get; set; }

        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string? Picture { get; set; } = null!;
        public decimal Cost { get; set; }
        public decimal Discount { get; set; }
        public DateTime DateOfAdd { get; set; }

        public ICollection<ProductCategory> ProductCategories { get; set; } = null!;
        public ICollection<ProductOrder> ProductOrders { get; set; } = null!;

        [NotMapped]
        public string CorrectPicturePath
        {
            get => (Picture == string.Empty || Picture == null)
                ? @"\Resources\Pictures\NonPicture.png" : @$"\Resources\Pictures\{Picture}";
        }

        public bool IsDiscount { get => Discount != 0; }
        public bool IsNew { get => (DateTime.Now.Date-DateOfAdd.Date).TotalDays < 20; }
        public decimal CorrectCost { get => IsDiscount ? Cost-(Cost * (Discount/100)) : Cost; }
        public int CorrectDiscount { get => (int)(Discount); }
    }
}
