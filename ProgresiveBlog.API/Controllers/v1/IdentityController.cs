using Microsoft.AspNetCore.Http.Features;
using ProgresiveBlog.Application.Identity.Dtos;
using ProgresiveBlog.Application.Identity.Query;

namespace ProgresiveBlog.API.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route(ApiRoutes.BaseRoute)]
    [ApiController]
    public class IdentityController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public IdentityController(IMediator mediator, 
            IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        [HttpPost]
        [Route(ApiRoutes.Identity.Register)]
        [ValidateModel]
        public async Task<IActionResult> Register(UserRegistration register)
        {
            var command = _mapper.Map<RegisterIdentity>(register);
            var result = await _mediator.Send(command);
            if (result.IsError) 
                return HandleErrorResponse(result.Errors);
            //var authenticationResult = new AuthenticationResult
            //{
            //    Token = result.Payload
            //};
            
            //return Ok(authenticationResult);
            return Ok(_mapper.Map<IdentityUserProfileDto>(result.Payload));
        }
        [HttpPost]
        [Route(ApiRoutes.Identity.Login)]
        public async Task<IActionResult> Login([FromBody] Login login)
        {
            var command = _mapper.Map<LoginCommand>(login);
            var result = await _mediator.Send(command);
            if (result.IsError) return HandleErrorResponse(result.Errors);
            //var authResult = new AuthenticationResult
            //{
            //    Token = result.Payload
            //};
            //return Ok(authResult);
            return Ok(_mapper.Map<IdentityUserProfileDto>(result.Payload));
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete]
        [Route(ApiRoutes.Identity.RemovalById)]
        [ValidateGuid("identityUserId")]
        public async Task<IActionResult> DeleteAccount(string identityUserId)
        {
            var identityUserIdGuid = Guid.Parse(identityUserId);
            var requesterGuid = HttpContext.GetUserProfileIdClaimValue();
            var command = new RemoveAccount
            {
                IdentityUserId = identityUserIdGuid,
                RequesterGuid = requesterGuid
            };
            var result = await _mediator.Send(command);
            if (result.IsError) return HandleErrorResponse(result.Errors);
            return NoContent();
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        [Route(ApiRoutes.Identity.CurrentUser)]
        public async Task<IActionResult> CurrentUser(CancellationToken cancellationToken)
        {
            var userProfileId = HttpContext.GetUserProfileIdClaimValue();
            var query = new CurrentUser
            {
                UserProfileId = userProfileId,
                ClaimsPrincipal =HttpContext.User

            };
            var result = await _mediator.Send(query,cancellationToken);
            if (result.IsError)   return HandleErrorResponse(result.Errors);

            return Ok(_mapper.Map<IdentityUserProfile>(result.Payload));
        }
    }
}

