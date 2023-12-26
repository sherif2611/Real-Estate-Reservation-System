namespace RealEstate.Models
{
    public class RealEstate
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Area { get; set; }
        public byte[] MainImg { get; set; }
        public string Desc { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public byte[] Credentail { get; set; }
        public bool Available { get; set; }
        public bool Confirmed { get; set; }
        public string Address { get; set; }
        public DateTime PublishingDate { get; set; }
        public virtual List<Imgs> Imgs { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int AdminId { get; set; }
        public virtual Admin Admin { get; set; }
    }
}