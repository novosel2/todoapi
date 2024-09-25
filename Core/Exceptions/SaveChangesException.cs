using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Exceptions
{
    public class SaveChangesException : Exception
    {
        public SaveChangesException()
        {
        }

        public SaveChangesException(string? message) : base(message)
        {
        }

        public SaveChangesException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
