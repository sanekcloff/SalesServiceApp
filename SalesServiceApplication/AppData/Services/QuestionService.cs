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
    public class QuestionService
    {
        private ApplicationDbContext _ctx;
        public QuestionService(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }
        public ICollection<Question> GetQuestions()
            => _ctx.Questions.Include(q => q.Client).Include(q => q.Employee).ToList();
        public Question? GetQuestion(int id)
            => _ctx.Questions.Include(q => q.Client).Include(q => q.Employee).SingleOrDefault(q=>q.Id==id);
        public void Insert(Question question)
        {
            _ctx.Questions.Add(question);
            _ctx.SaveChanges();
        }
        public void Update(Question question)
        {
            _ctx.Questions.Update(question);
            _ctx.SaveChanges();
        }
        public void Delete(Question question)
        {
            _ctx.Questions.Remove(question);
            _ctx.SaveChanges();
        }
    }
}
