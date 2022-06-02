using MediatR;
using ProgresiveBlog.Application.Models;
using ProgresiveBlog.Domain.Aggregates.Post;

namespace ProgresiveBlog.Application.Posts.Commands
{
    public class AddInteraction: IRequest<OperationResult<PostInteraction>>
    {
        public Guid PostId { get; set; }
        public Guid UserProfileId { get; set; }
        public InteractionType Type { get; set; }
    }
}
