using System;
using System.Collections.Generic;
using System.Text;

namespace IPStackExternalService.Exceptions
{
    public class IPDetailsUpdateRequestInternalError : Exception
    {
        public IPDetailsUpdateRequestInternalError()
        {
        }

        public IPDetailsUpdateRequestInternalError(string message)
        : base(message)
        {
        }

        public IPDetailsUpdateRequestInternalError(string message, Exception inner)
        : base(message, inner)
        {
        }
    }
}
