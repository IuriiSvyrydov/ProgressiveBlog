using ProgresiveBlog.Domain.Exceptions;

namespace ProgresiveBlog.Application.Exceptions.UserProfile
{
    public class UserProfileValidatorException : NotValidException
    {
        public UserProfileValidatorException()
        {

        }
        public UserProfileValidatorException(string message) : base(message)
        {
        }
        public UserProfileValidatorException(string message, Exception innerException) 
            : base(message,innerException)
        {
        }
    }
}
