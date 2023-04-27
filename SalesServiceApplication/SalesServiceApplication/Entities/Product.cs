using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesServiceApplication.Entities
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
        public string? Description { get; set; } = null!;
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
                ? @"\Content\Pictures\NonPicture.png" : @$"\Content\Pictures\{Picture}";
        }

        public bool IsDiscount { get => Discount != 0 ? true : false; }
        public decimal CorrectCost { get => IsDiscount ? Cost * (Discount/100) : Cost; }
        public int CorrectDiscount { get => (int)(100 - Discount); }
    }
}
