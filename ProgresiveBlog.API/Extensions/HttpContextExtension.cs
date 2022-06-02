using System.Security.Claims;
using System.Security.Principal;
using ProgresiveBlog.Application.Posts.CommandHandlers;

namespace ProgresiveBlog.API.Extensions
{
    public static class HttpContextExtension
    {
        public static Guid GetUserProfileIdClaimValue(this  HttpContext context)
        {
            return GetGuidClaimValue("UserProfileId",context);
        }

        public static Guid GetIdentityIdClaimValue(this HttpContext context)
        {
            return GetGuidClaimValue("IdentityId", context);
        }

        private static Guid GetGuidClaimValue(string key, HttpContext context)
        {
            var identity = context.User.Identity as ClaimsIdentity;
            return Guid.Parse(identity?.FindFirst(key)?.Value);
        }

    }
}