
using MediatR;
using ProgresiveBlog.Application.Identity.Dtos;
using ProgresiveBlog.Application.Models;

namespace ProgresiveBlog.Application.Identity.Commands
{
    public class RegisterIdentity: IRequest<OperationResult<IdentityUserProfileDto>>
    {
       
        public string UserName { get; set; }
       

        public string Password { get; set; }
       
        public string FirstName { get; set; }
       
        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Phone { get; set; }
        public string City { get; set; }
    }
}
