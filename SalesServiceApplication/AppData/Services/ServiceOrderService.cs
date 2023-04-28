using AppData.DataBaseData;
using AppData.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Services
{
    public class ServiceOrderService
    {
        private ApplicationDbContext _ctx;
        public ServiceOrderService(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }
        public ICollection<ServiceOrder> GetServiceServices() 
            => _ctx.ServiceOrders
            .Include(so => so.Employee)
            .Include(so => so.Service)
            .ToList();
        public ServiceOrder? GetServiceService()
            => _ctx.ServiceOrders
            .Include(so => so.Employee)
            .Include(so => so.Service)
            .SingleOrDefault();
        public void Insert(ServiceOrder serviceOrder)
        {
            _ctx.ServiceOrders.Add(serviceOrder);
            _ctx.SaveChanges();
        }
        public void Update(ServiceOrder serviceOrder)
        {
            _ctx.ServiceOrders.Update(serviceOrder);
            _ctx.SaveChanges();
        }
        public void Delete(ServiceOrder serviceOrder)
        {
            _ctx.ServiceOrders.Remove(serviceOrder);
            _ctx.SaveChanges();
        }
    }
}
