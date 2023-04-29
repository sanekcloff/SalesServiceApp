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
    public class ClientService
    {
        private ApplicationDbContext _ctx;
        public ClientService(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }
        public ICollection<Client> GetClients() 
            => _ctx.Clients.ToList();
        public Client? GetClient(int id)
            => _ctx.Clients.SingleOrDefault(c => c.Id == id);
        public void Insert(Client client)
        {
            _ctx.Clients.Add(client);
            _ctx.SaveChanges();
        }
        public void Update(Client client)
        {
            _ctx.Clients.Update(client);
            _ctx.SaveChanges();
        }
        public void Delete(Client client)
        {
            _ctx.Clients.Remove(client);
            _ctx.SaveChanges();
        }

    }
}
