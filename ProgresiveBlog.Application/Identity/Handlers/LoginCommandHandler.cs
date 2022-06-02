using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProgresiveBlog.Application.Enums;
using ProgresiveBlog.Application.Identity.Commands;
using ProgresiveBlog.Application.Identity.Services;
using ProgresiveBlog.Application.Models;
using ProgresiveBlog.Dal;
using ProgresiveBlog.Domain.Aggregates.User;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace ProgresiveBlog.Application.Identity.Handlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, OperationResult<string>>
    {
        private readonly PostDbContext _dbContext;
        private readonly UserManager<IdentityUser> _manager;
 
        private readonly IdentityService _identityService;

        public LoginCommandHandler(PostDbContext dbContext,UserManager<IdentityUser>manager,
              IdentityService identityService)
        {
            _dbContext = dbContext;
            _manager = manager;
            _identityService = identityService;
       
        }

        public async Task<OperationResult<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<string>();
            try
            {
                var identityUser = await ValidateAndGetIdentityAsync(request, result);
                var userProfile = await _dbContext.UsersProfiles
                    .FirstOrDefaultAsync(up => up.IdentityId == identityUser.Id);
            
                result.Payload = GetJwtString(identityUser, userProfile);
               
                return result;
            }
            catch (Exception ex)
            {
                result.IsError = true;

                var error = new Error
                {
                    Code = ErrorCode.NotFound,
                    Message = $"{ex.Message}"
                };
                result.Errors.Add(error);
            }

            return result;
        }

        private async Task<IdentityUser> ValidateAndGetIdentityAsync( LoginCommand request, OperationResult<string> result)
        {
            var identityUser = await _manager.FindByEmailAsync(request.UserName);
            if (identityUser is null)
                result.AddError(ErrorCode.IdentityUserDoesNotExist, IdentityErrorMessage.NonExistingUserManager);
                
            
            var validPassword =
                await _manager.CheckPasswordAsync(identityUser, request.Password);
            if (!validPassword)
                result.AddError(ErrorCode.IncorrectPassword,IdentityErrorMessage.IncorrectPassword);
            return identityUser;
        }

        private string GetJwtString(IdentityUser identity, UserProfile profile)
        {
            var claimsIdentity = new ClaimsIdentity(new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, identity.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, identity.Email),
                new Claim("IdentityId", identity.Id),
                new Claim("UserProfileId", profile.UserProfileId.ToString())
            });
            var token = _identityService.CreateSecurityToken(claimsIdentity);

            return _identityService.WriteToken(token);
        }
    }
}