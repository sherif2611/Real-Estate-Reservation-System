namespace RealEstate.Models
{
    public class Imgs
    {
        public int Id { get; set; }
        public byte[] Img { get; set; }
        public int RealEstateId { get; set; }
        public virtual RealEstate RealEstate { get; set; }
    }
}
