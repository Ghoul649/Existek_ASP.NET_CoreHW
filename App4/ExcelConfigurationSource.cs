using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App4
{
    public class ExcelConfigurationSource : IConfigurationSource
    {
        private readonly string _filePath;
        public ExcelConfigurationSource(string filePath) 
        {
            _filePath = filePath;
        }
        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new ExcelConfigurationProvider(_filePath);
        }
    }
}
