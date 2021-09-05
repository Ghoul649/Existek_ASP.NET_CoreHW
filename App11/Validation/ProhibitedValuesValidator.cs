using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App11.Validation
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ProhibitedValuesValidator : ValidationAttribute
    {
        private static Dictionary<string, IEnumerable<string>> ProhibitedValues;
        public IEnumerable<string> Values { get; set; }
        public static void Load(IConfiguration configuration) 
        {
            ProhibitedValues = new Dictionary<string, IEnumerable<string>>();
            foreach (var child in configuration.GetChildren()) 
            {
                string[] values = child.GetChildren().Select(ent => ent.Value).ToArray();
                if (values.Length == 0)
                    continue;
                ProhibitedValues.Add(child.Key, values);
            }
        }
        public string Key { get; set; }
        public StringComparison StringComparison { get; set; }
        public ProhibitedValuesValidator() 
        {
        }
        public override bool IsValid(object value)
        {
            if (value == null)
                return true;
            if (Values == null)
                if (!ProhibitedValues.TryGetValue(Key, out var values))
                    return true;
                else
                    Values = values;
            
            var str = value.ToString();
            foreach (var item in Values) 
            {
                if (string.Equals(item, str, StringComparison))
                    return false;
            }
            return true;
        }
        public override string FormatErrorMessage(string name)
        {

            return $"Specified value of field \"{name}\" is prohibited.";
        }
    }
}
