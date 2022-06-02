namespace ProgresiveBlog.Application.Enums
{
    public enum 
        ErrorCode
    {
        NotFound = 404,
        ServerError = 500,
     
        ValidationError = 111,
        //Infrastructure Error
        IdentityUserAlreadyExists = 201,
        IncorrectPassword = 202,
        IdentityUserDoesNotExist = 203,
        IdentityUserCreationFailed = 204,
        //Application Error 
        PostUpdateNotPossible = 300,
        DeletePostNotPossible = 301,
        RemoveInteractionNotAuthorize = 302,
        UnAuthorizedAccountRemoval = 304,


        UnknownError  = 999


    }
}
