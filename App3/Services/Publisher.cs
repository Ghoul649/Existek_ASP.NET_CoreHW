using App3.Interfaces;
using App3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App3.Services
{
    public class Publisher : IPublishingService
    {
        private List<Article> _published = new List<Article>();
        public IEnumerable<Article> Published => _published.Select(a => new Article()
        {
            ID = a.ID,
            Content = a.Content,
            Title = a.Title,
            Author = a.Author
        });

        public void Publish(Article a)
        {
            Article toPublish = new Article()
            {
                ID = (_published.Max(a => a.ID) ?? 0) + 1,
                Content = a.Content,
                Title = a.Title,
                Author = a.Author
            };
            a.ID = toPublish.ID;
            _published.Add(toPublish);
        }
    }
}
