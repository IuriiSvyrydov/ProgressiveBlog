using ProgresiveBlog.Application.Exceptions.Post;
using ProgresiveBlog.Domain.Aggregates.User;
using ProgresiveBlog.Domain.Validators.PostValidators;

namespace ProgresiveBlog.Domain.Aggregates.Post
{
    public class Post
    {
        private readonly List<PostComment> _postComments;
        private readonly List<PostInteraction> _postInteractions;
        public Post()
        {
            _postComments = new List<PostComment>();
            _postInteractions = new List<PostInteraction>();
        }
        public Guid PostId { get; set; }
        public Guid UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }
        public string TextContent { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastModified { get; set; }
        public IEnumerable<PostComment> PostComments => _postComments;
        public IEnumerable<PostInteraction> PostInteractions => _postInteractions;


        #region Simple CRUD

        public static Post CreatePost(Guid userProfileId, string text)
        {
            var validator = new PostValidator();

            var objectToValidate  =  new Post
            { 
                UserProfileId = userProfileId,
                TextContent = text,
                DateCreated = DateTime.UtcNow,
                LastModified = DateTime.UtcNow
            };
            var resultToValidate = validator.Validate(objectToValidate);
            if (resultToValidate.IsValid) return objectToValidate;
            var exception = new PostIsNotValidException(" The Post is not valid");
            foreach (var error in resultToValidate.Errors)
            {
                exception.Errors.Add(error.ErrorMessage);
            }
            throw exception;

        }
        public void UpdateText(string newText)
        {
            if(string.IsNullOrWhiteSpace(newText))
            {
                var exception = new PostIsNotValidException("Can not update text");
                exception.Errors.Add("The provided text is either null or either only white space");
                throw exception;
            }
            TextContent = newText;
            LastModified = DateTime.Now;
        }

        public void AddPostComment(PostComment newComment)
        {
            _postComments.Add(newComment);
        }
        public  void UpdatePostComment(Guid postCommentId, string updatedComment)
        {
            var comment = _postComments.FirstOrDefault(c => c.CommentId == postCommentId);
            if (comment != null && !string.IsNullOrWhiteSpace(updatedComment))
                comment.UpdatePostComment(updatedComment);
        }
        public void RemovePostComment(PostComment removeComment)
        {
            _postComments.Remove(removeComment);
        }

        public void AddPostInteraction(PostInteraction newInteraction)
        {
            _postInteractions.Add(newInteraction);
        }
        public void RemovePostInteraction(PostInteraction removeInteraction)
        {
            _postInteractions.Remove(removeInteraction);
        }

        #endregion
    }
}