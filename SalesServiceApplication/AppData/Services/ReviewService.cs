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
    public class ReviewService
    {
        private ApplicationDbContext _ctx;
        public ReviewService(ApplicationDbContext ctx)
        {
            _ctx=ctx;
        }
        public ICollection<Review> GetReviews()
            => _ctx.Reviews.Include(r => r.Client).ToList();
        public Review? GetReview(int id)
            => _ctx.Reviews.Include(r => r.Client).SingleOrDefault(r=>r.Id==id);
        public void Imsert(Review review)
        {
            _ctx.Reviews.Add(review);
            _ctx.SaveChanges();
        }
        public void Update(Review review)
        {
            _ctx.Reviews.Update(review);
            _ctx.SaveChanges();
        }
        public void Delete(Review review)
        {
            _ctx.Reviews.Remove(review);
            _ctx.SaveChanges();
        }

    }
}
