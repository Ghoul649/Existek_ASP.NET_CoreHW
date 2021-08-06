using App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Interfaces
{
    public interface IStorageService
    {
        IEnumerable<Article> Select();
        IEnumerable<Article> Select(Article selection);
        int Update(Article selection, Article value);
        int Delete(Article selection);
        int Insert(IEnumerable<Article> articles);
        bool Insert(Article article);
    }
}
