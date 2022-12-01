using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Application.Exceptions
{
    public class InvalidExternalAuthentication : Exception
    {
        public InvalidExternalAuthentication() : base("Invalid External Authentication")
        {
        }

        public InvalidExternalAuthentication(string? message) : base(message)
        {
        }

        public InvalidExternalAuthentication(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
