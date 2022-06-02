using ProgresiveBlog.Application.Exceptions.UserProfile;
using ProgresiveBlog.Domain.Validators.UserInfoValidartor;

namespace ProgresiveBlog.Domain.Aggregates.User
{
    public class BasicInfo
    {
        private BasicInfo()
        {

        }
        public string FirstName { get;  set; }
        public string LastName { get;  set; }
        public string EmailAddress { get; set; }
       public string PhoneNumber { get; set; } 
        public DateTime DateOfBirth { get;  set; }
        public string CurrentCity { get;  set; }

        public static BasicInfo CreateBasicInfo(string firstName, string lastName, string emailAddress,
            string phone, DateTime dateOfBirth, string currentCity)
        {
            var validator = new BasicInfoValidator();
            var objectToValidate =   new BasicInfo
            {
                FirstName = firstName,
                LastName = lastName,
                EmailAddress = emailAddress,
                PhoneNumber = phone,
                DateOfBirth = dateOfBirth,
                CurrentCity = currentCity
            };
            var validateResut = validator.Validate(objectToValidate);
            
            if (validateResut.IsValid) return objectToValidate;
            var exception = new UserProfileValidatorException(" The uswrProfile is not valid");
            validateResut.Errors.ForEach(vr => exception.Errors.Add(vr.ErrorMessage));
            throw exception;
    
        }
    }


}
