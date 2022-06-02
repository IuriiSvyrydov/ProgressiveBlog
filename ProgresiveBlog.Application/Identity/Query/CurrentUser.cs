using System.Security.Claims;
using MediatR;
using ProgresiveBlog.Application.Identity.Dtos;
using ProgresiveBlog.Application.Models;

namespace ProgresiveBlog.Application.Identity.Query;

public class CurrentUser: IRequest<OperationResult<IdentityUserProfileDto>>
{
    public Guid UserProfileId { get; set; }
    public ClaimsPrincipal ClaimsPrincipal { get; set; }
}