using System.ComponentModel.DataAnnotations;

namespace HamburgerMVC.Models.DTOs
{
    public class UserDTO
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Email is not valid")]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
