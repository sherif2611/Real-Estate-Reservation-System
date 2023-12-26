namespace RealEstate.Models
{
    public class User
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public virtual Person Person { get; set; }
        public virtual List<RealEstate> RealEstates { get; set; }
    }
}