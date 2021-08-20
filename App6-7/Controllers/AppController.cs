using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App6_7.Controllers
{
    public class AppController : Controller
    {
        //  /alpha/asdf
        [Route("alpha/{value:alpha}")]
        public string AlphaConstraint(string value)
        {
            return $"AlphaConstraint: {value}";
        }

        // /bool/true
        [Route("bool/{value:bool}")]
        public string BoolConstraint(bool value)
        {
            return $"BoolConstraint: {value}";
        }

        // /datetime/08.20.2021%2015:49
        [Route("datetime/{value:datetime}")]
        public string DateTimeConstraint(DateTime value)
        {
            return $"DateTimeConstraint: {value}";
        }

        // /double/123.123
        [Route("double/{value:double}")]
        public string DoubleConstraint(decimal value)
        {
            return $"DoubleConstraint: {value}";
        }

        // /guid/D12BEB59-6259-4FA1-A733-ADCD523D72DC
        [Route("guid/{value:guid}")]
        public string GuidConstraint(Guid value)
        {
            return $"GuidConstraint: {value}";
        }

        // /int/123
        [Route("int/{value:int}")]
        public string IntConstraint(int value)
        {
            return $"IntConstraint: {value}";
        }
        // /length/123asdfasd
        [Route("length/{value:length(10)}")]
        public string LengthConstraint(string value)
        {
            return $"LengthConstraint: {value}";
        }

        // /regex/roma.gulchak@gmail.com
        [Route(@"regex/{value:regex(^[[\w-\.]]+@([[\w-]]+\.)+[[\w-]]{{2,4}}$)}")]
        public string RegexEmailConstraint(string value)
        {
            return $"RegexEmailConstraint: {value}";
        }

        [Route("customexception")]
        public IActionResult ThrowCustomException()
        {
            throw new NotImplementedException();
        }
        [Route("exception")]
        public IActionResult ThrowException()
        {
            throw new Exception();
        }

        [MathValidationFilter]
        [Route("secured")]
        public string SecuredAction() 
        {
            return "Action";
        }
    }
}
