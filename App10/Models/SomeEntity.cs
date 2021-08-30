using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App10.Models
{
    public class SomeEntity
    {
        public virtual int Id { get; set; }
        public virtual int IntProperty { get; set; }
        public virtual string StringProperty1 { get; set; }
        public virtual string StringProperty2 { get; set; }
        public virtual double DoubleProperty { get; set; }

    }
}
