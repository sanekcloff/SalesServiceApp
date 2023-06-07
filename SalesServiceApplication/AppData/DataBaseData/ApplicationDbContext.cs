using AppData.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.DataBaseData
{
    public class ApplicationDbContext:DbContext
    {

        private const string connectionString = @"Server=DESKTOP-I8L1GP6; Database=SalesServiceAppDB; Trusted_Connection=true; TrustServerCertificate=true;";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Client> Clients { get; set; } = null!;
        public DbSet<Department> Departments { get; set; } = null!;
        public DbSet<Employee> Employees { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<ProductCategory> ProductCategories { get; set; } = null!;
        public DbSet<ProductOrder> ProductOrder { get; set; } = null!;
        public DbSet<Service> Services { get; set; } = null!;
        public DbSet<ServiceOrder> ServiceOrders { get; set; } = null!;
        public DbSet<Review> Reviews { get; set; } = null!;
        public DbSet<New> News { get; set; } = null!;
        public DbSet<Question> Questions { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
