using System.ComponentModel.DataAnnotations;

namespace RealEstate.ViewModels
{
    public class UserViewModel
    {
        [MaxLength(10)]
        [Required]
        [MinLength(2)]
        public string FName { get; set; }
        [MaxLength(10)]
        [Required]
        [MinLength(2)]
        public string LName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
