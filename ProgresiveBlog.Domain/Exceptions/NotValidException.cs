

namespace ProgresiveBlog.Domain.Exceptions
{
    public class NotValidException: Exception
    {
        public List<string> Errors { get; }
        public NotValidException()
        {
            Errors = new List<string>();
        }
        public NotValidException(string message) : base(message)
        {
            Errors = new List<string>();
        }
        public NotValidException(string message, Exception innerException)
            : base(message, innerException)
        {
            Errors = new List<string>();
        }
    }
}
