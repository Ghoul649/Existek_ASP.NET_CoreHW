using App.Interfaces;
using App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Services
{
    public class ToHTMLHandler : IArticleHandlerService
    {
        public void Handle(Article p)
        {
            if(string.IsNullOrWhiteSpace(p.Content))
                throw new ArgumentException("Content is empty");
            p.Content = $"<div class='article'>{string.Concat(p.Content.Split('\n').Select(str => $"<p>{str}</p>"))}</div>";
        }
    }
}
