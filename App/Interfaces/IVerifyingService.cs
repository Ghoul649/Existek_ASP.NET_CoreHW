using App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Interfaces
{
    public interface IVerifyingService
    {
        bool Verify(Article article);
    }
}
