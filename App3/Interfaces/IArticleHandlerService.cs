using App3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App3.Interfaces
{
    public interface IArticleHandlerService
    {
        void Handle(Article p);
    }
}
