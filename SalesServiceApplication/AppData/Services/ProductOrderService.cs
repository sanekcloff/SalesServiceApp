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
    public class ProductOrderService
    {
        private ApplicationDbContext _ctx;
        public ProductOrderService(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }
        public ICollection<ProductOrder> GetProductOrders()
            => _ctx.ProductOrder
            .Include(po => po.Employee)
            .Include(po => po.Product)
                .ThenInclude(p=>p.ProductCategories)
                    .ThenInclude(pc=>pc.Category)
            .ToList();
        public ProductOrder? GetProductOrder(int id)
            => _ctx.ProductOrder
            .Include(po => po.Employee)
            .Include(po => po.Product)
                .ThenInclude(p => p.ProductCategories)
                    .ThenInclude(pc => pc.Category)
            .SingleOrDefault(po=>po.Id==id);
        public void Insert(ProductOrder productOrder)
        {
            _ctx.ProductOrder.Add(productOrder);
            _ctx.SaveChanges();
        }
        public void Update(ProductOrder productOrder)
        {
            _ctx.ProductOrder.Update(productOrder);
            _ctx.SaveChanges();
        }
        public void Delete(ProductOrder productOrder)
        {
            _ctx.ProductOrder.Remove(productOrder);
            _ctx.SaveChanges();
        }

    }
}
