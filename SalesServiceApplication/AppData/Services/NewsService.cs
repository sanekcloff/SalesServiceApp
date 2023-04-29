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
    public class NewsService
    {
        private ApplicationDbContext _ctx;
        public NewsService(ApplicationDbContext ctx)
        {
            _ctx= ctx;
        }
        public ICollection<New> GetNews() 
            => _ctx.News.Include(n=>n.Employee).ToList();
        public New? GetNew(int id)
            => _ctx.News.Include(n => n.Employee).SingleOrDefault(n=>n.Id == id);
        public void Insert(New @new)
        {
            _ctx.News.Add(@new);
            _ctx.SaveChanges();
        }
        public void Update(New @new)
        {
            _ctx.News.Update(@new);
            _ctx.SaveChanges();
        }
        public void Delete(New @new)
        {
            _ctx.News.Remove(@new);
            _ctx.SaveChanges();
        }
    }
}
