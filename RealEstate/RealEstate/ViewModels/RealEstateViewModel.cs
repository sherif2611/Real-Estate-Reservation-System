using System.ComponentModel.DataAnnotations;

namespace RealEstate.ViewModels
{
    public class RealEstateViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Desc { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public IFormFile Credentail { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public IFormFile MainImg { get; set; }
        [Required]
        public decimal Area { get; set; }
        [Required]
        public int countOfImgs { get; set; }
    }
}