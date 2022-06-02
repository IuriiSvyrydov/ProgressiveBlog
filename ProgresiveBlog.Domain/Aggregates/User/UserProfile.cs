
namespace ProgresiveBlog.Domain.Aggregates.User
{
    public class UserProfile
    {
        private UserProfile()
        {

        }
        public Guid UserProfileId { get;  set; }
        public string IdentityId { get;  set; }
        public BasicInfo BasicInfo { get; set; } 
        public DateTime DateCreated { get;  set; }
        public DateTime LastModified { get;  set; }

        // Factory method
        public static UserProfile CreateUserProfile(string identityId, BasicInfo basicInfo)
        {
            return new UserProfile
            {
                IdentityId = identityId,
                BasicInfo = basicInfo,
                DateCreated = DateTime.UtcNow,
                LastModified = DateTime.UtcNow
            };
        }
        public void UpdateInfo(BasicInfo newInfo)
        {
            BasicInfo = newInfo;
            LastModified = DateTime.UtcNow;
        }
    }
}
