using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App10.Models
{
    public class SomeEntityExt : SomeEntity
    {
        public override int Id { get => base.Id; set => base.Id = value; }
        private bool intPropertyChanged = false;
        public override int IntProperty { get => base.IntProperty; 
            set { intPropertyChanged = true; base.IntProperty = value; } }
        private bool stringProperty1Changed = false;
        public override string StringProperty1 { get => base.StringProperty1; 
            set  { stringProperty1Changed = true; base.StringProperty1 = value; } }
        private bool stringProperty2Changed = false;
        public override string StringProperty2 { get => base.StringProperty2; 
            set  { stringProperty2Changed = true; base.StringProperty2 = value; } }
        private bool doublePropertyChanged = false;
        public override double DoubleProperty { get => base.DoubleProperty;  
            set { doublePropertyChanged = true; base.DoubleProperty = value; } }
        public void Update(SomeEntity entity) 
        {
            if (intPropertyChanged)
                entity.IntProperty = IntProperty;
            if (stringProperty1Changed)
                entity.StringProperty1 = StringProperty1;
            if (stringProperty2Changed)
                entity.StringProperty2 = StringProperty2;
            if (doublePropertyChanged)
                entity.DoubleProperty = DoubleProperty;
        }
    }
}
