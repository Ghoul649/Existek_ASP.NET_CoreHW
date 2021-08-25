using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace App9.Models
{
    public class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int AuthorId { get; set; }
        public User Author { get; set; }

        public List<Article> RelatedArticlesTo { get; set; } = new List<Article>();
        public List<Article> RelatedArticlesFrom { get; set; } = new List<Article>();
        [NotMapped]
        public IEnumerable<Article> RelatedArticles 
        {
            get 
            {
                return RelatedArticlesFrom.Concat(RelatedArticlesTo);
            }
        }
        public void RemoveRelatedArticle(Article article)
        {
            RelatedArticlesFrom.Remove(article);
            RelatedArticlesTo.Remove(article);
        }
        public void AddRelatedArticle(Article article) 
        {
            RelatedArticlesTo.Add(article);
        }
    }
}
