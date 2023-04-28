using AppData.DataBaseData;
using AppData.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Services
{
    public class ProductService
    {
        private ApplicationDbContext _ctx;
        public ProductService(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }
        public ICollection<Product> GetProducts() 
            => _ctx.Products
            .Include(p=>p.ProductCategories)
                .ThenInclude(pc=>pc.Category)
            .ToList();  
        public Product? GetProduct(int id)
            => _ctx.Products
            .Include(p => p.ProductCategories)
                .ThenInclude(pc => pc.Category)
            .SingleOrDefault(p=>p.Id==id);
        public void Insert(Product product)
        {
            _ctx.Products.Add(product);
            _ctx.SaveChanges();
        }
        public void Update(Product product)
        {
            _ctx.Products.Update(product);
            _ctx.SaveChanges();
        }
        public void Delete(Product product)
        {
            _ctx.Products.Remove(product);
            _ctx.SaveChanges();
        }
    }
}
