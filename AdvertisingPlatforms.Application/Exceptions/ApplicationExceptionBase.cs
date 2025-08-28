using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisingPlatforms.Application.Exceptions
{
    public abstract class ApplicationExceptionBase : Exception
    {
        protected ApplicationExceptionBase(string message) : base(message) { }
    }
}
