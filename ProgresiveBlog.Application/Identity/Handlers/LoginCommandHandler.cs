using System.Security.Claims;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProgresiveBlog.Application.Enums;
using ProgresiveBlog.Application.Identity.Commands;
using ProgresiveBlog.Application.Identity.Dtos;
using ProgresiveBlog.Application.Identity.Services;
using ProgresiveBlog.Application.Models;
using ProgresiveBlog.Dal;
using ProgresiveBlog.Domain.Aggregates.User;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace ProgresiveBlog.Application.Identity.Handlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, OperationResult<IdentityUserProfileDto>>
    {
        private readonly PostDbContext _dbContext;
        private readonly UserManager<IdentityUser> _manager;
        private readonly IMapper _mapper;
        private readonly IdentityService _identityService;
        private OperationResult<IdentityUserProfileDto> _result = new();

        public LoginCommandHandler(PostDbContext dbContext,UserManager<IdentityUser>manager,
              IdentityService identityService, IMapper mapper)
        {
            _dbContext = dbContext;
            _manager = manager;
            _identityService = identityService;
            _mapper = mapper;
        }

        public async Task<OperationResult<IdentityUserProfileDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var identityUser = await ValidateAndGetIdentityAsync(request);
                var userProfile = await _dbContext.UsersProfiles
                    .FirstOrDefaultAsync(up => up.IdentityId == identityUser.Id);
                _result.Payload = _mapper.Map<IdentityUserProfileDto>(userProfile);
                _result.Payload.UserName = identityUser.UserName;

                _result.Payload.Token = GetJwtString(identityUser, userProfile);
               
                return _result;
            }
            catch (Exception ex)
            {
                _result.IsError = true;

                var error = new Error
                {
                    Code = ErrorCode.NotFound,
                    Message = $"{ex.Message}"
                };
                _result.Errors.Add(error);
            }

            return _result;
        }

        private async Task<IdentityUser> ValidateAndGetIdentityAsync( LoginCommand request)
        {
            var identityUser = await _manager.FindByEmailAsync(request.UserName);
            if (identityUser is null)
                _result.AddError(ErrorCode.IdentityUserDoesNotExist, IdentityErrorMessage.NonExistingUserIdentity);
                
            
            var validPassword =
                await _manager.CheckPasswordAsync(identityUser, request.Password);
            if (!validPassword)
                _result.AddError(ErrorCode.IncorrectPassword,IdentityErrorMessage.IncorrectPassword);
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