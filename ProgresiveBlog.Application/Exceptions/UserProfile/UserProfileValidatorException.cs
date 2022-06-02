using ProgresiveBlog.Application.Exceptions.UserProfile;

namespace ProgresiveBlog.Domain.Exceptions.UserProfile
{
    public class UserProfileValidatorException: BasicException
    {
        public UserProfileValidatorException()
        {

        }
        public UserProfileValidatorException(string message):base(message)
        {

        }
        public UserProfileValidatorException(string message,Exception innerException):base(message,innerException)
        {

        }
    }
}
