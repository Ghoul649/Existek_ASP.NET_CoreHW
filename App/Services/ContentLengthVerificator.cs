using App.Interfaces;
using App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Services
{
    public class ContentLengthVerificator : IVerifyingService
    {
        int _min;
        int _max;
        public ContentLengthVerificator(int minLength = 30, int maxLength = 3000) 
        {
            _min = minLength;
            _max = maxLength;
        }
        public bool Verify(Article a)
        {
            return (!string.IsNullOrWhiteSpace(a.Content)) && a.Content.Length >= _min && a.Content.Length <= _max;
        }
    }
}
