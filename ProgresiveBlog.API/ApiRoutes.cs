namespace ProgresiveBlog.API
{
    public static class ApiRoutes
    {
        public const string BaseRoute = "api/v{version:apiVersion}/[controller]";
        public static class UserProfiles
        {
            public const string IdRoute = "{id}";
        } 
        public static class Posts
        {
            public const string IdRoute = "{id}";
            public const string PostComments = "{postId}/comments";
            public const string CommentById = "{postId}/comments/{commentId}";
            public const string AddPostInteraction = "{postId}/postInteractions";
            public const string PostInteractionById = "{postId}/postInteractions/{interactionId}";
            public const string PostInteractions = "{postId}/postInteractions";
        }

        public static class Identity
        {
            public const string Login = "login";
            public const string Register = "registration";
            public const string RemovalById = "IdentityUserId";
        }
    }

   
}
