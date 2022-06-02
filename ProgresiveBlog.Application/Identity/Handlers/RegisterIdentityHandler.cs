using System.Security.Claims;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage;
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
    public class RegisterIdentityHandler: IRequestHandler<RegisterIdentity,OperationResult<IdentityUserProfileDto>>
    {
        private readonly PostDbContext _context;
        private  readonly UserManager<IdentityUser> _manager;
        private readonly IdentityService _identityService;
        private OperationResult<IdentityUserProfileDto> _result = new();
        private readonly IMapper _mapper;

        public RegisterIdentityHandler(PostDbContext context,UserManager<IdentityUser>manager, IdentityService identityService, IMapper mapper)
        {
            _context = context;
            _manager = manager;
            _identityService = identityService;
            _mapper = mapper;
        }

        public async Task<OperationResult<IdentityUserProfileDto>> Handle(RegisterIdentity request, CancellationToken cancellationToken)
        {
            
            try
            {
                var creationValidated = await ValidateIdentityDoesNotExist( request);
                if (!creationValidated) return _result;
                await using var transaction = _context.Database.BeginTransaction();

                var identity = await CreateIdentityAsync( request, transaction);
                if (identity is null) return _result;

                var profile = await CreateUserProfileAsync( request, transaction,identity);
                await transaction.CommitAsync();
                _result.Payload = _mapper.Map<IdentityUserProfileDto>(profile);
                _result.Payload.UserName = identity.UserName;

                _result.Payload.Token = GetJwtString(identity, profile);
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
        private  async Task<bool> ValidateIdentityDoesNotExist(RegisterIdentity request)
        {
            var existingIdentity = await _manager.FindByEmailAsync(request.UserName);
            if (existingIdentity != null)
               _result.AddError(ErrorCode.IdentityUserAlreadyExists,IdentityErrorMessage.IdentityUserAlreadyExist);
            return true;

        }
        private async Task<IdentityUser> CreateIdentityAsync( RegisterIdentity request,
            IDbContextTransaction transaction )
        {
            var identity = new IdentityUser {Email = request.UserName, UserName = request.UserName};
            var createIdentity = await _manager.CreateAsync(identity, request.Password);
            if (!createIdentity.Succeeded)
            {
                await transaction.RollbackAsync();
                _result.IsError = true;
                foreach (var identityError in createIdentity.Errors)
                {
                    
                    _result.AddError(ErrorCode.IdentityUserCreationFailed,identityError.Description);
                }

            }

            return identity;
        }
        private async Task<UserProfile> CreateUserProfileAsync( RegisterIdentity request,
            IDbContextTransaction transaction,IdentityUser identity)
        {
           
            try
            {
                var profileInfo = BasicInfo.CreateBasicInfo(request.FirstName, request.LastName, request.UserName,
                    request.Phone, request.DateOfBirth, request.City);
                var profile = UserProfile.CreateUserProfile(identity.Id, profileInfo);
                _context.UsersProfiles.Add(profile);
                await _context.SaveChangesAsync();
                return profile;
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        private string GetJwtString(IdentityUser identity,UserProfile profile)
        {
            var claimsIdentity = new ClaimsIdentity(new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, identity.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, identity.Email),
                new Claim("identityId", identity.Id),
                new Claim("UserProfileId", profile.UserProfileId.ToString())
            });
            var token = _identityService.CreateSecurityToken(claimsIdentity);

            return _identityService.WriteToken(token);
        }
    }
}
