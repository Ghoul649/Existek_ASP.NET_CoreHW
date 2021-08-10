using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App4
{
    public class AppSpecialConfiguration : ICloneable
    {
        public string InnerKey1 { get; set; }
        public string InnerKey2 { get; set; }

        public object Clone()
        {
            return new AppSpecialConfiguration()
            {
                InnerKey1 = InnerKey1,
                InnerKey2 = InnerKey2
            };
        }
    }
}
