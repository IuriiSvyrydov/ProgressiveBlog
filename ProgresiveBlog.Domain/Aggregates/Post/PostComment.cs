
using ProgresiveBlog.Application.Exceptions.Post;
using ProgresiveBlog.Domain.Validators.PostValidators;

namespace ProgresiveBlog.Domain.Aggregates.Post
{
    public class PostComment
    {
        private PostComment()
        {
            
        }
        public Guid CommentId { get;  set; }
        public Guid PostId { get;  set; }
        public string Text { get;  set; }
        public Guid UserProfileId { get;  set; }
        public DateTime DateCreated { get;  set; }
        public DateTime LastModified { get;  set; }
        public static PostComment CreatePostComment(Guid postId,string text, Guid userProfileId)
        {
            //TODO Validation
            var validator = new PostCommemtValidator();
            var objectTovalidate =  new PostComment
            {
                PostId = postId,
                Text = text,
                UserProfileId = userProfileId,
                DateCreated= DateTime.Now,
                LastModified = DateTime.Now
            };
            var validatorResult = validator.Validate(objectTovalidate);
            if (validatorResult.IsValid) return objectTovalidate;
            var exception = new PostIsNotValidException("Post comment is not valid");
            validatorResult.Errors.ForEach(vr => exception.Errors.Add(vr.ErrorMessage));
            throw exception;
        }

        public void UpdatePostComment(string newComment)
        {
            Text = newComment;
            LastModified = DateTime.UtcNow;
        }
    }
}
