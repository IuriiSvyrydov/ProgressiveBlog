using System.ComponentModel.DataAnnotations;

namespace ProgresiveBlog.API.Contracts.UserProfiles.Requests;

public class UserProfileCreateUpdate
{
    [Required]
    [MinLength(3)]
    [MaxLength(50)]
    public string FirstName { get; set; }

    [Required]
    [MinLength(3)]
    [MaxLength(50)]
    public string LastName { get; set; }

    [EmailAddress]
    [Required]
    public string EmailAddress { get; set; }

    public string PhoneNumber { get; set; }

    [Required]
    public DateTime DateOfBirth { get; set; }
    public string CurrentCity { get; set; }
}