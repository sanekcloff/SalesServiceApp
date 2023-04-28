using AppData.DataBaseData;
using AppData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Services
{
    public class CategoryService
    {
        private ApplicationDbContext _ctx;
        public CategoryService(ApplicationDbContext ctx)
        {
            _ctx=ctx;
        }
        public ICollection<Category> GetCategories()
            => _ctx.Categories.ToList();
        public Category? GetCategory(int id) 
            => _ctx.Categories.SingleOrDefault(c => c.Id == id);
        public void Insert(Category category)
        {
            _ctx.Categories.Add(category);
            _ctx.SaveChanges();
        }
        public void Update(Category category)
        {
            _ctx.Categories.Update(category);
            _ctx.SaveChanges();
        }
        public void Delete(Category category)
        {
            _ctx.Categories.Remove(category);
            _ctx.SaveChanges();
        }
    }
}
