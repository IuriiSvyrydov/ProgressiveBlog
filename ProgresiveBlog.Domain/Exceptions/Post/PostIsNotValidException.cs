namespace ProgresiveBlog.Domain.Exceptions.Post
{
    public class PostIsNotValidException: NotValidException
    {
        public PostIsNotValidException()
        {

        }
        public PostIsNotValidException(string message) : base(message)
        {

        }
        public PostIsNotValidException(string message,Exception innerException)
        {

        }
    }
}
