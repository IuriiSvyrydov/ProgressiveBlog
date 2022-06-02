using MediatR;
using Microsoft.EntityFrameworkCore;
using ProgresiveBlog.Application.Enums;
using ProgresiveBlog.Application.Exceptions.UserProfile;
using ProgresiveBlog.Application.Models;
using ProgresiveBlog.Application.UserProfiles.Commands;
using ProgresiveBlog.Dal;
using ProgresiveBlog.Domain.Aggregates.User;
using ProgresiveBlog.Domain.Exceptions.UserProfile;

namespace ProgresiveBlog.Application.UserProfiles.CommandHandlers
{
    public class UpdateUserProfileBasicHandler: IRequestHandler<UpdateUserProfile, OperationResult<UserProfile>>
    {
        private readonly PostDbContext _context;

        public UpdateUserProfileBasicHandler(PostDbContext context)
        {
            _context = context;
        }

        public async Task<OperationResult<UserProfile>> Handle(UpdateUserProfile request
            , CancellationToken cancellationToken)
        {
            var result = new OperationResult<UserProfile>();
            try
            {
                var userProfile = await _context.UsersProfiles.FirstOrDefaultAsync(up => up.UserProfileId
                                                                              == request.UserProfileId);
                if (userProfile == null)
                {
                    result.IsError = true;
                    var error = new Error { Code = ErrorCode.NotFound, Message = $"No user Profile with ID{request.UserProfileId}found" };
                    result.Errors.Add(error);
                    return result;
                }
                var basicInfo = BasicInfo.CreateBasicInfo(request.FirstName, request.LastName, request.EmailAddress,
                    request.PhoneNumber, request.DateOfBirth, request.CurrentCity);
                userProfile.UpdateInfo(basicInfo);
                _context.UsersProfiles.Update(userProfile);
                await _context.SaveChangesAsync();
                result.Payload = userProfile;
                return result;
            }
            catch (Exceptions.UserProfile.UserProfileValidatorException ex)
            {
                result.IsError = true;
                ex.Errors.ForEach(e =>
                {
                    var error = new Error
                    {
                        Code = ErrorCode.ValidationError,
                        Message = $"{ex.Message}"
                    };
                    result.Errors.Add(error);
                });
                return result;
            }
            catch(Exception ex)
            {
                var error = new Error { Code = ErrorCode.ServerError,Message = ex.Message };
                result.IsError = true;
                result.Errors.Add(error);
            }
           
            return result;
        }
    }
}
