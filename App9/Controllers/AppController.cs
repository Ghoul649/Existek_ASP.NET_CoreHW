using App9.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App9.Controllers
{
    public class AppController : Controller
    {
        public AppDBContext DBContext { get; set; }

        public AppController(AppDBContext dbContext)
        {
            DBContext = dbContext;
        }

        [Route("adddata")]
        public void AddData() 
        {
            var user1 = new User() { UserName = "User1" };
            var user2 = new User() { UserName = "User2" };
            var user3 = new User() { UserName = "User3" };
            var user4 = new User() { UserName = "User4" };
            DBContext.Users.Add(user1);
            DBContext.Users.Add(user2);
            DBContext.Users.Add(user3);
            DBContext.Users.Add(user4);

            var useri1 = new UserInfo() { User = user1, FirstName = "James", LastName = "Johnson" };
            var useri2 = new UserInfo() { User = user2, FirstName = "Erika", LastName = "Johnson" };
            var useri3 = new UserInfo() { User = user3, FirstName = "Catherine", LastName = "Garcia" };
            var useri4 = new UserInfo() { User = user4, FirstName = "Cameron", LastName = "Brown" };

            DBContext.UserInfos.Add(useri1);
            DBContext.UserInfos.Add(useri2);
            DBContext.UserInfos.Add(useri3);
            DBContext.UserInfos.Add(useri4);

            var article1 = new Article() { Title = "Article1", Content = "content1", Author = user1 };
            var article2 = new Article() { Title = "Article2", Content = "content2", Author = user1 };
            var article3 = new Article() { Title = "Article3", Content = "content3", Author = user2 };
            var article4 = new Article() { Title = "Article4", Content = "content4", Author = user3 };

            article4.AddRelatedArticle(article1);
            article4.AddRelatedArticle(article3);
            article2.AddRelatedArticle(article1);

            DBContext.Articles.Add(article1);
            DBContext.Articles.Add(article2);
            DBContext.Articles.Add(article3);
            DBContext.Articles.Add(article4);

            DBContext.SaveChanges();
        }

        [Route("articles")]
        public object GetArticles()
        {
            return DBContext.Articles
                .Include(a => a.RelatedArticlesTo)
                .Include(a => a.Author)
                .Include(a => a.Author.UserInfo)
                .ToList()
                .Select(
                a =>
                    new
                    {
                        Id = a.Id,
                        Title = a.Title,
                        Content = a.Content,
                        Author = new 
                        { 
                            UserName = a.Author.UserName, 
                            FirstName = a.Author.UserInfo.FirstName,
                            LastName = a.Author.UserInfo.LastName
                        },
                        RelatedArticles = a.RelatedArticles.Select(i => i.Id)
                    }
            );
        }
    }
}
