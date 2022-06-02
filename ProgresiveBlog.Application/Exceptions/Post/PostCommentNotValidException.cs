using ProgresiveBlog.Application.Exceptions.UserProfile;

namespace ProgresiveBlog.Application.Exceptions.Post
{
    public class PostCommentNotValidException: BasicException
    {
        public PostCommentNotValidException()
        {

        }
        public PostCommentNotValidException(string message):base(message)
        {

        }
        public PostCommentNotValidException(string message, Exception innerException)
            :base(message, innerException)
        {

        }
    }
}
