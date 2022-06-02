namespace ProgresiveBlog.Application.Identity
{
    public class IdentityErrorMessage
    {
        public const string NonExistingUserManager = "UserName or Password wrong. Login is failed";
        public const string IncorrectPassword = "The Provided password incorrect";
        public const string IdentityUserAlreadyExist = 
            "Provided email address already exists.Cannot register new user";
        public const string UnauthorizeedRemovalAccount = "Cannot remove account.You are not owner";
    };
}

