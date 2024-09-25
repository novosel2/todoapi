using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Exceptions
{
    public class UpdatingFailedException : Exception
    {
        public UpdatingFailedException()
        {
        }

        public UpdatingFailedException(string? message) : base(message)
        {
        }

        public UpdatingFailedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
