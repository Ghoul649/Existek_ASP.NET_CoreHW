using ExcelDataReader;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App4
{
    public class ExcelConfigurationProvider : ConfigurationProvider
    {
        private readonly string _path;
        public ExcelConfigurationProvider(string filePath) 
        {
            _path = filePath;
        }
        public override void Load()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var values = new Dictionary<string, string>();
            using (var reader = ExcelReaderFactory.CreateOpenXmlReader(File.OpenRead(_path),new ExcelReaderConfiguration() {}))
            {
                while (reader.Read())
                {
                    var key = reader.GetString(0)?.Replace('/', ':');
                    var value = reader.GetValue(1)?.ToString();
                    if(key != null && value != null)
                        values[key] = value;
                }
            }
            Data = values;
        }
    }
}
