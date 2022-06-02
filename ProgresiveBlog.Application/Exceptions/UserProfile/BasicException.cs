using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgresiveBlog.Application.Exceptions.UserProfile
{
    public class BasicException: Exception
    {
        public List<string> ValidatorErrors { get; }
        public BasicException()
        {
            ValidatorErrors = new List<string>();
        }
        public BasicException(string message) : base(message)
        {
            ValidatorErrors = new List<string>();
        }
        public BasicException(string message, Exception innnerException) : base(message, innnerException)
        {
            ValidatorErrors = new List<string>();

        }
    }
}
