using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App4
{
    public class AppConfiguration : ICloneable
    {
        public string TestKey1 { get; set; }
        public string TestKey2 { get; set; }
        public int Int { get; set; }
        public double Double { get; set; }
        public DateTime DateTime { get; set; }
        public AppSpecialConfiguration Section1 { get; set; }
        public AppSpecialConfiguration Section2 { get; set; }
        public object Clone()
        {
            return new AppConfiguration()
            {
                TestKey1 = TestKey1,
                TestKey2 = TestKey2,
                Int = Int,
                Double = Double,
                DateTime = DateTime,
                Section1 = (AppSpecialConfiguration)Section1.Clone(),
                Section2 = (AppSpecialConfiguration)Section2.Clone()
            };
        }
    }
}
