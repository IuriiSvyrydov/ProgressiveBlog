using ProgresiveBlog.Domain.Exceptions;

namespace ProgresiveBlog.Application.Exceptions.Post
{
    internal class PostIsNotValidException : NotValidException
    {
        public PostIsNotValidException()
        {

        }
        public PostIsNotValidException(string message) : base(message)
        {
        }
        public PostIsNotValidException(string message,Exception innerException) : base(message,innerException)
        {
        }
    }
}
