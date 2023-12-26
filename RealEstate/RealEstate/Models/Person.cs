using System.ComponentModel.DataAnnotations;

namespace RealEstate.Models
{
    public class Person
    {
        public int Id { get; set; }
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
        public string Role { get; set; }
        public virtual User User { get; set; }
        public virtual Admin Admin { get; set; }
        public virtual Support Support { get; set; }
    }
}