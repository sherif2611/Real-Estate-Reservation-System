using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstate.Models
{
    public class RealEstateReservations
    {
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        [Key]
        public int RealEstateId { get; set; }
        [ForeignKey("RealEstateId")]
        public virtual RealEstate RealEstate { get; set; }
        public DateTime StartReservationDate { get; set; }
        public DateTime EndReservationDate { get; set;}
    }
}