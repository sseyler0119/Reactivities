using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string DisplayName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        // RegularExpression states that password must contain a number, a lc letter, uc letter and must be between 4-8 characters
        [RegularExpression("(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{4,8}$", ErrorMessage ="Password must contain at least 1 number, 1 lowercase letter, 1 uppercase letter and be betwwen 4-8 characters in length. ")] 
        public string Password { get; set; }
        [Required]
        public string Username { get; set; }
    }
}