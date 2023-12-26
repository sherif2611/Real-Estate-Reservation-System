using System.ComponentModel.DataAnnotations;

namespace RealEstate.Models
{
    public class Support
    {
        public int Id { get; set; }
        [Phone]
        public string Phone { get; set; }
        public int PersonId { get; set; }
        public virtual Person Person { get; set; }
    }
}
