using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProgresiveBlog.Application.Enums;
using ProgresiveBlog.Application.Identity.Dtos;
using ProgresiveBlog.Application.Identity.Query;
using ProgresiveBlog.Application.Identity.Services;
using ProgresiveBlog.Application.Models;
using ProgresiveBlog.Dal;

namespace ProgresiveBlog.Application.Identity.QueryHandlers
{
    public class GetCurrentUserHandler: IRequestHandler<CurrentUser,OperationResult<IdentityUserProfileDto>>
    {
        private readonly PostDbContext _dbContext;
        private readonly UserManager<IdentityUser> _manager;
        private readonly IMapper _mapper;
        private OperationResult<IdentityUserProfileDto> _result = new();

        public GetCurrentUserHandler(PostDbContext dbContext, UserManager<IdentityUser> manager,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _manager = manager;
            _mapper = mapper;
            
        }

        public async Task<OperationResult<IdentityUserProfileDto>> Handle(CurrentUser request, CancellationToken cancellationToken)
        {
            var identity = await _manager.GetUserAsync(request.ClaimsPrincipal);
        

            var profile =
                await _dbContext.UsersProfiles.FirstOrDefaultAsync(up => up.UserProfileId == request.UserProfileId);
            _result.Payload = _mapper.Map<IdentityUserProfileDto>(profile);
            _result.Payload.UserName = identity.UserName;
            return _result;
        }
    }
}
