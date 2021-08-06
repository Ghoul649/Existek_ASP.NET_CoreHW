using App.Interfaces;
using App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Services
{
    public class InMemoryStorageService : IStorageService
    {
        List<Article> articles = new List<Article>();
        public IEnumerable<Article> Select()
        {
            return articles.Select( 
                a => new Article() 
                { 
                    ID = a.ID,
                    Author = a.Author,
                    Content = a.Content,
                    Title = a.Title,
                    IsPublished = a.IsPublished
                });
        }
        public IEnumerable<Article> Select(Article selection)
        {
            return Filter(articles, selection).Select(
                a => new Article()
                {
                    ID = a.ID,
                    Author = a.Author,
                    Content = a.Content,
                    Title = a.Title,
                    IsPublished = a.IsPublished
                });
        }
        public int Insert(IEnumerable<Article> articles) 
        {
            return articles.Count(a => Insert(a));
        }
        public bool Insert(Article article)
        {
            if (articles.Any(a => a.ID == article.ID))
                return false;
            articles.Add(new Article() 
            { 
                ID = article.ID,
                Title = article.Title,
                Content = article.Content,
                Author = article.Author,
                IsPublished = article.IsPublished
            });
            return true;
        }
        public int Update(Article selection, Article value)
        {
            int count = 0;
            var res = Filter(articles, selection);
            foreach (var a in res) 
            {
                bool changed = false;
                if (value.Title != null && value.Title != a.Title)
                {
                    a.Title = value.Title;
                    changed = true;
                }
                if (value.Content != null && value.Content != a.Content) 
                {
                    a.Content = value.Content;
                    changed = true;
                }
                if (value.Author != null && value.Author != a.Author)
                {
                    a.Author = value.Author;
                    changed = true;
                }
                if (value.IsPublished != null && value.IsPublished != a.IsPublished)
                {
                    a.IsPublished = value.IsPublished;
                    changed = true;
                }
                if (changed)
                    count++;
            }
            return count;
        }
        public int Delete(Article selection)
        {
            int count = 0;
            var res = Filter(articles, selection);
            foreach (var a in res)
                if (articles.Remove(a))
                    count++;
            return count;
        }
        private static IEnumerable<Article> Filter(IEnumerable<Article> articles, Article article) 
        {
            return articles.Where(a => 
                (article.ID ??          a.ID)           == a.ID &&
                (article.Title ??       a.Title)        == a.Title &&
                (article.Content ??     a.Content)      == a.Content &&
                (article.Author ??      a.Author)       == a.Author &&
                (article.IsPublished ?? a.IsPublished)  == a.IsPublished
            );
        }
    }
}
