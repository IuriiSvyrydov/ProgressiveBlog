namespace ProgresiveBlog.API.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route(ApiRoutes.BaseRoute)]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PostController: BaseController
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public PostController(IMapper mapper,IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllPosts()
        {
            var query = new GetAllPosts();
            var response = await _mediator.Send(query);

            var profiles = _mapper.Map<List<PostResponse>>(response.Payload);
            return Ok(profiles);
            
        }

        [HttpGet]
        [Route(ApiRoutes.Posts.IdRoute)]
        [ValidateGuid("id")]
        public async Task< IActionResult> GetPostById(string id)
        {
            var postId = Guid.Parse(id);
            var query = new GetPostById {PostId = postId};
            var result =await _mediator.Send(query);
            var mapped = _mapper.Map<PostResponse>(result.Payload);
            return result.IsError ? HandleErrorResponse(result.Errors) : Ok(mapped);
        }
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult>CreatePost([FromBody]PostCreate newPost, string id)
        {
            var userProfileId = Guid.Parse(id);
            var command = new CreatePost()
            {
                UserProfileId  = userProfileId,
                Content = newPost.Content
            };
            var result = await _mediator.Send(command);
            var mapped = _mapper.Map<PostResponse>(result.Payload);
            return result.IsError ? HandleErrorResponse(result.Errors) :
                CreatedAtAction(nameof(GetPostById), 
                    new {id = result.Payload.UserProfileId},mapped );
        }

        [HttpPatch]
        [ValidateModel]
        [Route(ApiRoutes.Posts.IdRoute)]
        [ValidateGuid("id")]
        public async Task<IActionResult> UpdatePostText([FromBody]PostUpdate newPost,string id)
        {
            var userProfileId = HttpContext.GetUserProfileIdClaimValue();
            var command = new UpdatePostText
            {
                NewText = newPost.Text,
                PostId = Guid.Parse(id),
                UserProfileId = userProfileId
            };
            var result = await _mediator.Send(command);
            return result.IsError ? HandleErrorResponse(result.Errors) : NoContent();
        }
        [HttpDelete]
        [Route(ApiRoutes.Posts.IdRoute)]
        [ValidateGuid("id")]
        [ValidateModel]
        public async Task<IActionResult> DeletePost(string id)
        {
            var userProfileId = HttpContext.GetUserProfileIdClaimValue();
            var command = new PostDelete()
            {
                PostId = Guid.Parse(id),
                UserProfileId = userProfileId 
            };
            var response = await _mediator.Send(command);
            return response.IsError ? HandleErrorResponse(response.Errors) : NoContent();
        }
        [HttpGet]
        [Route(ApiRoutes.Posts.PostComments)]
        [ValidateModel]
        [ValidateGuid("postId")]
        public async Task<IActionResult> GetListPostComment(string postId)
        {
            var comment = new GetPostComments
            {
                PostId = Guid.Parse(postId)
            };
            var response = await _mediator.Send(comment);
            if (response.IsError)
                HandleErrorResponse(response.Errors);
           
            var profiles = _mapper.Map<List<PostCommentResponse>>(response.Payload);
            return Ok(profiles);
        }
        [HttpPost]
        [Route(ApiRoutes.Posts.PostComments)]
        [ValidateGuid("postId")]
        [ValidateModel]
        public async Task<IActionResult> AddPostComment(string postId,[FromBody]PostCommentCreate comment,CancellationToken cancellationToken)
        {
            var userProfileId =  HttpContext.GetUserProfileIdClaimValue();
           

            var command = new AddPostComment
            {
                PostId = Guid.Parse(postId),
                UserProfileId = userProfileId,
                Text = comment.Text
            };
            var result = await _mediator.Send(command,cancellationToken);
            if (result.IsError) return HandleErrorResponse(result.Errors);
            var mapped = _mapper.Map<PostCommentResponse>(result.Payload);
            return Ok(mapped);
        }
        [HttpGet]
        [Route(ApiRoutes.Posts.PostInteractions)]
            
        [ValidateGuid("postId")]
        public async Task<IActionResult> GetPostInteractions(string postId)
        {
            var postGuid = Guid.Parse(postId);
            var query = new GetPostInteraction
            {
                PostId = postGuid
            };
            var result = await _mediator.Send(query);
            if (result.IsError) HandleErrorResponse(result.Errors);
            var mapped = _mapper.Map<List<PostInteraction>>(result.Payload);
            return Ok(mapped);
        }

        [HttpPost]
        [Route(ApiRoutes.Posts.PostInteractions)]
        [ValidateGuid("postId")]
        [ValidateModel]
        public async Task<IActionResult> AddInteraction(string postId,PostInteractionCreate interaction,
            string userProfileId ,CancellationToken token)
        {
            var postGuid = Guid.Parse(postId);
            var profileId = Guid.Parse(userProfileId);
            var command = new AddInteraction
            {
                PostId = postGuid,
                UserProfileId = profileId,
                Type = interaction.Type
            };
            var result = await _mediator.Send(command,token);
            if (result.IsError) HandleErrorResponse(result.Errors);
            var mapped = _mapper.Map<PostInteraction>(result.Payload);
            return Ok(mapped);
        }
        [HttpDelete]
        [Route(ApiRoutes.Posts.PostInteractionById)]
        [ValidateGuid("postId","interactionId")]
        public async Task<IActionResult> RemovePostInteraction(string postId, string interactionId,
            string userProfileId)
        {
            var postGuid = Guid.Parse(postId);
            var interactionGuid = Guid.Parse(interactionId);
            var userProfileGuid = Guid.Parse(userProfileId);
            var command = new DeletePostInteractions
                {PostId = postGuid, InteractionId = interactionGuid, UserProfileId = userProfileGuid};
            var result = await _mediator.Send(command);
            if (result.IsError) 
                return HandleErrorResponse(result.Errors);
            var mapped = _mapper.Map<PostInteraction>(result.Payload);
            return Ok(mapped);
        }
    } 
}
