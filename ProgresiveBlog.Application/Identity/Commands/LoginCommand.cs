using MediatR;
using ProgresiveBlog.Application.Models;

namespace ProgresiveBlog.Application.Identity.Commands
{
    public class LoginCommand: IRequest<OperationResult<string>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
