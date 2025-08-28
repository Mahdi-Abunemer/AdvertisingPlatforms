using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisingPlatforms.Application.Exceptions
{
    public class FileParseException : ApplicationExceptionBase
    {
        public FileParseException(string message) : base(message) { }
    }
}
