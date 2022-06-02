namespace ProgresiveBlog.API.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route(ApiRoutes.BaseRoute)]
    [ApiController]
    [Authorize]
    public class UserProfileController : BaseController
    {
        #region private fields and constructors
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public UserProfileController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator; 
            _mapper = mapper;
        }
        #endregion
        #region simple methods
        /// <summary>
        /// Create Methods CRUD 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllProfiles()
        {
            var query = new GetUAllUserProfile();
            var response = await _mediator.Send(query);
            var profiles = _mapper.Map<List<UserProfileResponse>>(response.Payload);
            return Ok(profiles);
        }
       
        [HttpGet]
        [Route(ApiRoutes.UserProfiles.IdRoute)]
        public async Task<IActionResult> GetUserProfileById(string id)
        {
            var query = new GetUserProfileById
            {
                UserProfileId = Guid.Parse(id)
            };
            var response = await _mediator.Send(query);
            if (response.IsError)
              return  HandleErrorResponse(response.Errors);
            var userProfile = _mapper.Map<UserProfileResponse>(response);
            return Ok(userProfile);
        }
        [HttpPatch]
        [Route(ApiRoutes.UserProfiles.IdRoute)]
        [ValidateModel]
        [ValidateGuid("id")]
        public async Task<IActionResult> UpdateUserProfile(string id ,
            UserProfileCreateUpdate updateProfile)
        {
            var command = _mapper.Map<UpdateUserProfile>(updateProfile);
            command.UserProfileId = Guid.Parse(id);
            var response = await _mediator.Send(command);
            return response.IsError ? HandleErrorResponse(response.Errors) : NoContent();
        }
        //[HttpDelete]
        //[Route(ApiRoutes.UserProfiles.IdRoute)]
        //public async Task<IActionResult> DeleteUserProfile(string id)
        //{
        //    var command = new DeleteUserProfile
        //    {
        //        UserProfileId = Guid.Parse(id)
        //    };
        //    var response =await _mediator.Send(command);
        //    return response.IsError ? HandleErrorResponse(response.Errors) : NotFound();
        //}
        #endregion
    }
}
