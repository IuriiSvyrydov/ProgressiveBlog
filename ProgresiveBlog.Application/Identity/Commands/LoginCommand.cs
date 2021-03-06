using MediatR;
using ProgresiveBlog.Application.Identity.Dtos;
using ProgresiveBlog.Application.Models;

namespace ProgresiveBlog.Application.Identity.Commands
{
    public class LoginCommand: IRequest<OperationResult<IdentityUserProfileDto>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
